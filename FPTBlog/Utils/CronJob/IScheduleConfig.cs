using System;

namespace FPTBlog.Utils.CronJob
{
    public interface IScheduleConfig<T> {
        string CronExpression {
            get; set;
        }
        TimeZoneInfo TimeZoneInfo {
            get; set;
        }
    }
}
