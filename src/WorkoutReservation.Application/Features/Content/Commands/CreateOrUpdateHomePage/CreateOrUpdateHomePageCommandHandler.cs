using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Content.Commands.CreateOrUpdateHomePage;

public record CreateOrUpdateHomePageCommand(string HtmlContent) : IRequest;

public class CreateOrUpdateHomePageCommandHandler : IRequestHandler<CreateOrUpdateHomePageCommand>
{
    private readonly IContentRepository _contentRepository;

    public CreateOrUpdateHomePageCommandHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }
    
    public async Task<Unit> Handle(CreateOrUpdateHomePageCommand request, CancellationToken token)
    {
        var lastHomePageContent = await _contentRepository
            .GetLastContentAsync(ContentType.HomePageHtml, false, token);

        if (lastHomePageContent is null)
        {
            var content = new Domain.Entities.Content(ContentType.HomePageHtml, request.HtmlContent);
            await _contentRepository.CreateAsync(content, token); 
        }
        else
        {
            lastHomePageContent.Update(request.HtmlContent);
            await _contentRepository.UpdateAsync(lastHomePageContent, token);
        }
        
        return Unit.Value;
    }
}