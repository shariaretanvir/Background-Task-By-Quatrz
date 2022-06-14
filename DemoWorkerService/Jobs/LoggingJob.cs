using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWorkerService.Jobs
{
    public class LoggingJob : IJob
    {
        private readonly ILogger<LoggingJob> logger;

        public LoggingJob(ILogger<LoggingJob> logger)
        {
            this.logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation($"Log information save at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
