using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;

public record GetReservationsQuery : IRequest<PagedResultDto<ReservationListDto>>, IPagedQuery
{
    public string SearchPhrase { get; set; }
    public string SortBy { get; set; }
    public bool SortByDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetReservationsHandler : IRequestHandler<GetReservationsQuery, 
    PagedResultDto<ReservationListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public GetReservationsHandler(IReservationRepository reservationRepository, 
        IDateTimeProvider dateTimeProvider)
    {
        _reservationRepository = reservationRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<PagedResultDto<ReservationListDto>> Handle(GetReservationsQuery request, 
        CancellationToken token)
    {
        var validator = new GetReservationsValidator();
        await validator.ValidateAndThrowAsync(request, token);

        var pagedResult = await _reservationRepository.GetPagedAsync(request, token);
        
        var dto = pagedResult.reservations.Select(r => new ReservationListDto
        {
            Id = r.Id,
            ReservationStatus = r.ReservationStatus,
            Note = r.Note,
            OwnerId = r.UserId,
            OwnerFullName = string.Join(" ", r.User.FirstName, r.User.LastName),
            RealWorkoutId = r.RealWorkoutId,
            StartTime = r.RealWorkout.StartTime,
            EndTime = r.RealWorkout.EndTime,
            Date = r.RealWorkout.Date,
            MaxParticipantNumber = r.RealWorkout.MaxParticipantNumber,
            CurrentParticipantNumber = r.RealWorkout.CurrentParticipantNumber,
            WorkoutTypeId = r.RealWorkout.WorkoutType.Id,
            WorkoutTypeName = r.RealWorkout.WorkoutType.Name,
            InstructorId = r.RealWorkout.Instructor.Id,
            InstructorFullName = string.Join(" ", r.RealWorkout.Instructor.FirstName, r.RealWorkout.Instructor.LastName),
            IsWorkoutExpired = _dateTimeProvider.CheckIsExpired(r.RealWorkout.Date, r.RealWorkout.EndTime)
        });
        
        return new PagedResultDto<ReservationListDto>(dto,
            pagedResult.totalItems, request.PageSize, request.PageNumber);
    }
}