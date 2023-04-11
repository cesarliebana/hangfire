using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaV.Hangfire.Core.Webhooks
{
    public record struct JobWebhookRequest(Guid Id, int Index, int Milliseconds);
}
