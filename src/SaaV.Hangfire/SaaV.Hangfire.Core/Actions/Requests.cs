namespace SaaV.Hangfire.Core.Actions
{
    public record struct JobActionRequest(Guid Id, int Index, int Milliseconds);
}
