using Hangfire.Common;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaaV.Hangfire.Infrastructure.Hangfire.Jobs;

namespace SaaV.Hangfire.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobsController(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet("enqueueWebhook")]
        public IActionResult EnqueueWebHook()
        {
            for (int i = 0; i < 10; i++)
                _backgroundJobClient.Enqueue<WebhookJob>(job => job.Execute(Guid.NewGuid(), i));
            return Ok("Webhooks enqueued");
        }

        [HttpGet("enqueueAction")]
        public IActionResult EnqueueAction()
        {
            for (int i = 0; i < 10; i++)
                _backgroundJobClient.Enqueue<ActionJob>(job => job.Execute(Guid.NewGuid(), i));
            return Ok("Actions enqueued");
        }
    }
}
