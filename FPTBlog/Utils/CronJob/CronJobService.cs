using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace FPTBlog.Utils.CronJob {
    public class CronJobService : IHostedService, IDisposable {
        private System.Timers.Timer CronJobTimer;
        private readonly CronExpression Expression;
        private readonly TimeZoneInfo TimeZoneInfo;

        public CronJobService(string expression, TimeZoneInfo timeZoneInfo) {
            Expression = CronExpression.Parse(expression);
            TimeZoneInfo = timeZoneInfo;
        }

        public void Dispose() {
            CronJobTimer?.Dispose();
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken) {
            await ScheduleJob(cancellationToken);
        }

        public virtual async Task DoWork(CancellationToken cancellationToken) {
            await Task.Delay(5000, cancellationToken);  // do the work
        }

        public virtual async Task ScheduleJob(CancellationToken cancellationToken) {
            var next = Expression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo);
            if (next.HasValue) {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                CronJobTimer = new System.Timers.Timer(delay.TotalMilliseconds);
                CronJobTimer.Elapsed += async (sender, args) => {
                    CronJobTimer.Dispose();  // reset and dispose timer
                    CronJobTimer = null;

                    if (!cancellationToken.IsCancellationRequested) {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested) {
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }
                };
                CronJobTimer.Start();
            }
            await Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken) {
            CronJobTimer?.Stop();
            await Task.CompletedTask;
        }
    }
}
