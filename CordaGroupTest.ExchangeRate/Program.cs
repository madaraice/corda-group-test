using System;
using System.IO;
using System.Threading.Tasks;
using CordaGroupTest.ExchangeRate.Jobs;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;

namespace CordaGroupTest.ExchangeRate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = GetConfigurationRoot("appsettings.json")
                .GetSection("MsSQL")
                .GetSection("ConnectionString");

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CurrencyJob>()
                .WithIdentity("job1", "group1")
                .UsingJobData("connectionString", connectionString.Value)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            Console.ReadKey();
            await scheduler.Shutdown();
        }

        private static IConfigurationRoot GetConfigurationRoot(string path)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path, optional: false, reloadOnChange: false)
                .Build();
        }
    }
}
