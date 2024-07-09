using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollBot.Jobs
{
    internal class JobsConfigurator
    {

        public static async Task Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            scheduler.JobFactory = new JobFactory(serviceProvider);


            IJobDetail pollJob = JobBuilder.Create<PollCreateJob>().Build();
            ITrigger pollTriger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule("0 0 14 * * ?", x => x
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")))
                .Build();
            await scheduler.ScheduleJob(pollJob, pollTriger);


            IJobDetail answerJob = JobBuilder.Create<CompletePoll>().Build();
            ITrigger answerTriger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .WithCronSchedule("0 0 17 * * ?", x => x
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")))
                .Build();
            await scheduler.ScheduleJob(answerJob, answerTriger);

            IJobDetail consoleJob = JobBuilder.Create<ConsoleHandleJob>().Build();
            ITrigger consoleTriger = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group3").StartNow()
                .Build();
            await scheduler.ScheduleJob(consoleJob, consoleTriger);
        }
    }
}
