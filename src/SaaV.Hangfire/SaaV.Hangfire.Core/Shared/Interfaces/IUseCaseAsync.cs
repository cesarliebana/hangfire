namespace SaaV.Hangfire.Core.Shared.Interfaces
{
    public interface INoResponseUseCaseAsync<TRequest>
    {
        Task ExecuteAsync(TRequest request);
    }
}
