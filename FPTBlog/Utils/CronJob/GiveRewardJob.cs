using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FPTBlog.Src.RewardModule;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FPTBlog.Utils.CronJob {
    public class GiveRewardJob : CronJobService {
        private readonly IServiceScopeFactory ServiceScopeFactory;
        private readonly IRewardService RewardService;
        public GiveRewardJob(IScheduleConfig<GiveRewardJob> config, IServiceScopeFactory serviceScopeFactory)
        : base(config.CronExpression, config.TimeZoneInfo) {
            ServiceScopeFactory = serviceScopeFactory;
            RewardService = (RewardService)ServiceScopeFactory.CreateScope().ServiceProvider.GetService<IRewardService>();
        }

        public override Task StartAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Give reward cron job starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken) {
            // Add huy hiệu cho student 5, 10, 25, 50 post





            Console.WriteLine($"{DateTime.Now:hh:mm:ss} Give reward cron job is working.");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Give reward cron job is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }


}
