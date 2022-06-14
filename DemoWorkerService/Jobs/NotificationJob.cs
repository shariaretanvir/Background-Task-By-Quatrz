using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWorkerService.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> logger;

        public NotificationJob(ILogger<NotificationJob> logger)
        {
            this.logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation($"Notify user on {DateTime.Now} and job type {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}
