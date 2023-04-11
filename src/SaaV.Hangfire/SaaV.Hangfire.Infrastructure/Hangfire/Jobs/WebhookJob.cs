using Hangfire;
using Microsoft.Extensions.Logging;
using SaaV.Hangfire.Core.Shared.Interfaces;
using SaaV.Hangfire.Core.Webhooks;

namespace SaaV.Hangfire.Infrastructure.Hangfire.Jobs
{
    public class WebhookJob
    {
        private readonly ILogger<WebhookJob> _logger;
        private readonly INoResponseUseCaseAsync<JobWebhookRequest> _useCase;

        public WebhookJob(ILogger<WebhookJob> logger, INoResponseUseCaseAsync<JobWebhookRequest> useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [Queue("webhook-queue")]
        public async Task Execute(Guid id, int index)
        {
            _logger.LogInformation($"Init Webhook process! [Guid: {id}] [Index: {index}]");
            await _useCase.ExecuteAsync(new JobWebhookRequest { Id = id, Index = index, Milliseconds = Random.Shared.Next(2000) + 500 });
            _logger.LogInformation($"End Webhook process! [Guid: {id}] [Index: {index}]");
        }
    }
}
