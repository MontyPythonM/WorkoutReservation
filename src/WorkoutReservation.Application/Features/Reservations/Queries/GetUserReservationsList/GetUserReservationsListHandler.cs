using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

public class GetUserReservationsListHandler : IRequestHandler<GetUserReservationsListQuery,
                                                              PagedResultDto<UserReservationsListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICurrentUserService _userService;

    public GetUserReservationsListHandler(IReservationRepository reservationRepository, 
                                          ICurrentUserService userService)
    {
        _reservationRepository = reservationRepository;
        _userService = userService;
    }

    public async Task<PagedResultDto<UserReservationsListDto>> Handle(GetUserReservationsListQuery request, 
                                                                      CancellationToken cancellationToken)
    {
        var validator = new GetUserReservationsListValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var userGuid = Guid.Parse(_userService.UserId);

        var reservationsQuery = _reservationRepository.GetUserReservationsByGuidQuery(userGuid);

        var query = reservationsQuery
            .Where(x => request.SearchPhrase == null ||
                   x.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));
        
        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>
            {
                { nameof(Reservation.ReservationStatus), u => u.ReservationStatus},
                { "WorkoutDate", u => u.RealWorkout.Date}
            };

            var sortByExpression = columnsSelector[request.SortBy];

            query = request.SortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        var result = query
        .Skip(request.PageSize * (request.PageNumber - 1))
        .Take(request.PageSize)
        .Select(x => new UserReservationsListDto
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            LastModificationDate = x.LastModificationDate,
            ReservationStatus = x.ReservationStatus,
            RealWorkout = new RealWorkoutForUserReservationsListDto
            { 
                Id = x.RealWorkout.Id,
                StartTime = x.RealWorkout.StartTime,
                EndTime = x.RealWorkout.EndTime,
                Date = x.RealWorkout.Date,
                CurrentParticipianNumber = x.RealWorkout.CurrentParticipianNumber,
                MaxParticipianNumber = x.RealWorkout.MaxParticipianNumber,

                Instructor = new InstructorForUserReservationsListDto
                { 
                    Id = x.RealWorkout.Instructor.Id,
                    FirstName = x.RealWorkout.Instructor.FirstName,
                    LastName = x.RealWorkout.Instructor.LastName
                },

                WorkoutType = new WorkoutTypeForUserReservationsListDto 
                { 
                    Id = x.RealWorkout.WorkoutType.Id,
                    Name = x.RealWorkout.WorkoutType.Name,
                    Intensity = x.RealWorkout.WorkoutType.Intensity
                }
            } 

        })
        .ToList();

        var pagedWorkoutTypes = new PagedResultDto<UserReservationsListDto>(result,
                                                                            totalCount,
                                                                            request.PageSize,
                                                                            request.PageNumber);
        return pagedWorkoutTypes;
    }
}
