using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWorkerService
{
    public class MyWorkerService : BackgroundService
    {
        public ILogger<MyWorkerService> Logger { get; }
        public MyWorkerService(ILogger<MyWorkerService> logger)
        {
            Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("Worker running at: {time} by {name}", DateTimeOffset.Now,"Akash");
                await Task.Delay(2000, stoppingToken);
            }

        }
    }
}
