using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWorkerService.Models
{
    public class JobMetadata
    {
        public Guid JobID { get; set; }
        public Type JobType { get; set; }
        public string JobName { get; set; }
        public string CronExpression { get; set; }

        public JobMetadata(Guid jobID, Type jobType, string jobName, string cronExpression)
        {
            JobID = jobID;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }
}
