using Hangfire;
using Hangfire.SqlServer;
using SaaV.Hangfire.Core.Actions;
using SaaV.Hangfire.Core.Shared.Interfaces;
using SaaV.Hangfire.Core.Webhooks;

namespace SaaV.Hangfire.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHangfire(configuration => configuration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(context.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        }));

                    services.AddHangfireServer(options =>
                    {
                        options.Queues = new[] { "webhook-queue", "action-queue" };
                        options.WorkerCount = context.Configuration.GetValue<int>("Hangfire:MaxWorkers");
                    });

                    services.AddTransient<INoResponseUseCaseAsync<JobWebhookRequest>, WebhookUseCase>();
                    services.AddTransient<INoResponseUseCaseAsync<JobActionRequest>, ActionUseCase>();
                })
                .Build();

            host.Run();
        }
    }
}