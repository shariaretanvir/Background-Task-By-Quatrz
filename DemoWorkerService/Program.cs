using DemoWorkerService.Jobs;
using DemoWorkerService.Models;
using DemoWorkerService.Schedular;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddHostedService<Worker>();
                    //services.AddHostedService<MyWorkerService>();


                    services.AddSingleton<IJobFactory, JobFactory.JobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                    #region jobType
                    services.AddSingleton<NotificationJob>();
                    services.AddSingleton<LoggingJob>();
                    #endregion

                    #region addingJobs
                    //for multiple jobs
                    List<JobMetadata> jobMetadatas = new List<JobMetadata>();
                    jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/10 * * * * ?"));
                    jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(LoggingJob), "Log Job", "0/5 * * * * ?"));

                    services.AddSingleton(jobMetadatas);
                    
                    //for single job
                    //services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/10 * * * * ?"));

                    #endregion

                    services.AddHostedService<MySchedular>();
                });
    }
}
