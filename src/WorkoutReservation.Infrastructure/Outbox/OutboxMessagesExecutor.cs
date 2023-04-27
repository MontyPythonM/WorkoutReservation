using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.Events;

namespace WorkoutReservation.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class OutboxMessagesExecutor : IJob
{
    private readonly IPublisher _publisher;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<OutboxMessagesExecutor> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OutboxMessagesExecutor(IPublisher publisher, AppDbContext dbContext, 
        ILogger<OutboxMessagesExecutor> logger, IDateTimeProvider dateTimeProvider)
    {
        _publisher = publisher;
        _dbContext = dbContext;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext.OutboxMessages
            .Where(message => message.ProcessedOn == null)
            .Take(10)
            .ToListAsync(context.CancellationToken);
        
        foreach (var message in messages)
        {
            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                if (domainEvent is null)
                {
                    _logger.LogError("Domain event cannot be execute", domainEvent);
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);
                message.ProcessedOn = _dateTimeProvider.Now;
            }
            catch (Exception e)
            {
                message.Error = e.Message;
                _logger.LogCritical($"Outbox message process and execute failed. OutboxMessageId: {message.Id.ToString()}", e);
            }
        }

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}