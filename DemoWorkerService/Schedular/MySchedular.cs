using DemoWorkerService.Models;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWorkerService.Schedular
{
    public class MySchedular : IHostedService
    {
        public IScheduler scheduler { get; set; }

        private readonly IJobFactory jobFactory;
        private readonly List<JobMetadata> jobMetadata;
        private readonly ISchedulerFactory schedulerFactory;

        public MySchedular(IJobFactory jobFactory, List<JobMetadata> jobMetadata, ISchedulerFactory schedulerFactory)
        {
            this.jobFactory = jobFactory;
            this.jobMetadata = jobMetadata;
            this.schedulerFactory = schedulerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //create schedular 
            scheduler = await schedulerFactory.GetScheduler();
            scheduler.JobFactory = jobFactory;

            //create job

            //for multiple job
            jobMetadata?.ForEach(metadata =>
            {
                IJobDetail jobDetail = CreateJob(metadata);
                ITrigger trigger = CreateTrigger(metadata);

                scheduler.ScheduleJob(jobDetail, trigger, cancellationToken).GetAwaiter();
            });
            //IJobDetail jobDetail = CreateJob(jobMetadata);

            ////create trigger 
            //ITrigger trigger = CreateTrigger(jobMetadata);
            
            ////schedule job
            //await scheduler.ScheduleJob(jobDetail, trigger,cancellationToken);

            //start  the schedular
            await scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobID.ToString())
                .WithCronSchedule(jobMetadata.CronExpression)
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobID.ToString())
                .WithDescription(jobMetadata.JobName)
                .Build();

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await scheduler.Shutdown();
        }
    }
}
