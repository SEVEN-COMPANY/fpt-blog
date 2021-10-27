using System.Collections.Generic;
using FPTBlog.Src.NotificationModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.NotificationModule.Interface {
    public interface INotificationRepository : IRepository<Notification> {
        public (List<Notification>, int) GetNotificationsLevelAndTimeWithCount(int pageIndex, int pageSize, string search, NotificationLevel searchLevel, string startDate, string endDate);

        public (List<Notification>, int) GetUserNotificationTimeWithCount(string userId, int pageIndex, int pageSize, string search, string startDate, string endDate);
    }

}