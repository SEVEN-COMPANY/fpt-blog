using System;
using System.Threading;
using System.Threading.Tasks;
using FPTBlog.Src.RewardModule.Interface;

namespace FPTBlog.Utils.CronJob {
    public class GiveRewardJob : CronJobService {
        public GiveRewardJob(IScheduleConfig<GiveRewardJob> config)
        : base(config.CronExpression, config.TimeZoneInfo) {
        }

        public override Task StartAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Give reward cron job starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken) {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss} Give reward cron job is working.");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Give reward cron job is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }


}
