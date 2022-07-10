namespace WorkoutReservation.Application.Contracts
{
    public interface IPagedQuery
    {
        string SearchPhrase { get; set; }
        string SortBy { get; set; }
        bool SortByDescending { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
