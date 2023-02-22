using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutReservation.API.Controllers.Base
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;
        private ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        protected async Task<IActionResult> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken token = default) 
            => Ok(await Mediator.Send(request, token));
    }
}
