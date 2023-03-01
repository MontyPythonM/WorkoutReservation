using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;

public record GetReservationsQuery : IRequest<PagedResultDto<ReservationListDto>>
{
    public string SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetReservationsHandler : IRequestHandler<GetReservationsQuery, 
    PagedResultDto<ReservationListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetReservationsHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<ReservationListDto>> Handle(GetReservationsQuery request, 
        CancellationToken token)
    {
        var validator = new GetReservationsValidator();
        await validator.ValidateAndThrowAsync(request, token);

        var reservationsQuery = _reservationRepository.GetReservationsQuery();
        
        var query = reservationsQuery
            .Where(x => request.SearchPhrase == null ||
                        x.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.FirstName.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.LastName.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));
        
        var totalCount = query.Count();

        var result = query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToList();

        var mappedResult = _mapper.Map<List<ReservationListDto>>(result);

        return new PagedResultDto<ReservationListDto>(mappedResult,
            totalCount, request.PageSize, request.PageNumber);
    }
}