using SaaV.Hangfire.Core.Shared.Interfaces;
using System;

namespace SaaV.Hangfire.Core.Webhooks
{
    public class WebhookUseCase : INoResponseUseCaseAsync<JobWebhookRequest>
    {
        public async Task ExecuteAsync(JobWebhookRequest request)
        {
            string path = $"{Directory.GetCurrentDirectory()}\\Hangfire\\webhook-{request.Id}-{request.Index}.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync($"Inicio");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(request.Milliseconds);
                    await writer.WriteLineAsync($"Iteración {i}");
                }
                await writer.WriteLineAsync($"Fin");
            }
        }
    }
}
