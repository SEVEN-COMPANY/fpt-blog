using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FPTBlog.Src.RewardModule;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.UserModule;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FPTBlog.Utils.CronJob {
    public class GiveRewardJob : CronJobService {
        private readonly IServiceScopeFactory ServiceScopeFactory;
        private readonly IRewardService RewardService;
        private readonly IUserService UserService;
        public GiveRewardJob(IScheduleConfig<GiveRewardJob> config, IServiceScopeFactory serviceScopeFactory)
        : base(config.CronExpression, config.TimeZoneInfo) {
            ServiceScopeFactory = serviceScopeFactory;
            RewardService = (RewardService)ServiceScopeFactory.CreateScope().ServiceProvider.GetService<IRewardService>();
            UserService = (UserService) ServiceScopeFactory.CreateScope().ServiceProvider.GetService<IUserService>();
        }

        public override Task StartAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Give reward cron job starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken) {
            this.RewardService.GiveRewardForUserHave_N_Posts(5);
            this.RewardService.GiveRewardForUserHave_N_Posts(10);
            this.RewardService.GiveRewardForUserHave_N_Posts(25);
            this.RewardService.GiveRewardForUserHave_N_Posts(50);

            Console.WriteLine($"{DateTime.Now:hh:mm:ss} Give reward cron job is working.");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Give reward cron job is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }


}
