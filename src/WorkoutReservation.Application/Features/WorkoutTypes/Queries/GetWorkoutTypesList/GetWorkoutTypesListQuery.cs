using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList
{
    public class GetWorkoutTypesListQuery : IRequest<PagedResultDto<WorkoutTypesListQueryDto>>, IPagedQuery
    {
        public string SearchPhrase { get; set; }
        public string SortBy { get; set; }
        public bool SortByDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
