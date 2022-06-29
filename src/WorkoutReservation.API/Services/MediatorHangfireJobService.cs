using MediatR;

namespace WorkoutReservation.API.Services
{
    public class MediatorHangfireJobService
    {
        private IMediator _mediator;
        public MediatorHangfireJobService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendJob<T>(T command)
        {
            await _mediator.Send(command);
        }
    }
}
