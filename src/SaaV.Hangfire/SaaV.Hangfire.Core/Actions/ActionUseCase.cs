using SaaV.Hangfire.Core.Shared.Interfaces;
using System;

namespace SaaV.Hangfire.Core.Actions
{
    public class ActionUseCase : INoResponseUseCaseAsync<JobActionRequest>
    {
        public async Task ExecuteAsync(JobActionRequest request)
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
