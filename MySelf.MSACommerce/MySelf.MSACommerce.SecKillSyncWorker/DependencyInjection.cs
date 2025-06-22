using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SecKillSyncWorker
{
    public static class DependencyInjection
    {
        public static void ConfigureQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            var quartzOption = configuration.GetSection("Quartz").Get<QuartzOption>();

            if (quartzOption == null) return;

            var scheduler = SchedulerBuilder
                .Create(quartzOption.Schedulers[0].ToNameValueCollection())
                .BuildScheduler().Result;

            services.AddSingleton(scheduler);

            services.TryAddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.TryAddSingleton<IJobFactory, MicrosoftDependencyInjectionJobFactory>();

            services.AddHostedService<QuartzHostedService>();
        }
    }
}
