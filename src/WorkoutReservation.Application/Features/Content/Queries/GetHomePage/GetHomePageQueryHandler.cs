using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Content.Queries.GetHomePage;

public record GetHomePageQuery() : IRequest<HomePageDto>;

public class GetHomePageQueryHandler : IRequestHandler<GetHomePageQuery, HomePageDto>
{
    private readonly IContentRepository _contentRepository;

    public GetHomePageQueryHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }
    
    public async Task<HomePageDto> Handle(GetHomePageQuery request, CancellationToken token)
    {
        var content = await _contentRepository.GetLastContentAsync(ContentType.HomePageHtml, false, token);

        if (content is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Content));
        }

        return new HomePageDto
        {
            Id = content.Id,
            Type = content.Type,
            Value = content.Value
        };
    }
}