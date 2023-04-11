using Hangfire;
using Microsoft.Extensions.Logging;
using SaaV.Hangfire.Core.Actions;
using SaaV.Hangfire.Core.Shared.Interfaces;
using SaaV.Hangfire.Core.Webhooks;

namespace SaaV.Hangfire.Infrastructure.Hangfire.Jobs
{
    public class ActionJob
    {
        private readonly ILogger<ActionJob> _logger;
        private readonly INoResponseUseCaseAsync<JobActionRequest> _useCase;

        public ActionJob(ILogger<ActionJob> logger, INoResponseUseCaseAsync<JobActionRequest> useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [Queue("action-queue")]
        public async Task Execute(Guid id, int index)
        {
            _logger.LogInformation($"Init Webhook process! [Guid: {id}] [Index: {index}]");
            await _useCase.ExecuteAsync(new JobActionRequest { Id = id, Index = index, Milliseconds = Random.Shared.Next(2000) + 500 });
            _logger.LogInformation($"End Webhook process! [Guid: {id}] [Index: {index}]");
        }
    }
}
