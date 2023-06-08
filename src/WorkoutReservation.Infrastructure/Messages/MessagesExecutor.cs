using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.Events;

namespace WorkoutReservation.Infrastructure.Messages;

[DisallowConcurrentExecution]
internal sealed class MessagesExecutor : IJob
{
    private readonly IPublisher _publisher;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<MessagesExecutor> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public MessagesExecutor(IPublisher publisher, AppDbContext dbContext, 
        ILogger<MessagesExecutor> logger, IDateTimeProvider dateTimeProvider)
    {
        _publisher = publisher;
        _dbContext = dbContext;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext.Messages
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
                message.ProcessedOn = _dateTimeProvider.GetNow();
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