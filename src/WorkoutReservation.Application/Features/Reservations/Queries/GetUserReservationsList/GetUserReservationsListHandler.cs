using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

public record GetUserReservationsListQuery : IRequest<PagedResultDto<UserReservationsListDto>>, IPagedQuery
{
    public Guid UserId { get; set; }
    public string SearchPhrase { get; set; }
    public string SortBy { get; set; }
    public bool SortByDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal sealed class GetUserReservationsListHandler : IRequestHandler<GetUserReservationsListQuery,
    PagedResultDto<UserReservationsListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetUserReservationsListHandler(IReservationRepository reservationRepository, 
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<UserReservationsListDto>> Handle(GetUserReservationsListQuery request, 
        CancellationToken token)
    {
        var validator = new GetUserReservationsListValidator();
        await validator.ValidateAndThrowAsync(request, token);
        
        var pagedResult = await _reservationRepository.GetPagedAsync(request, request.UserId, token);
        var mappedResult = _mapper.Map<List<UserReservationsListDto>>(pagedResult.reservations);

        return new PagedResultDto<UserReservationsListDto>(mappedResult,
            pagedResult.totalItems, request.PageSize, request.PageNumber);
    }
}
