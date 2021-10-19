using System;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Utils.Repository;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.NotificationModule {
    public class NotificationRepository : Repository<Notification>, INotificationRepository {
        private readonly DB DB;

        public NotificationRepository(DB dB) : base(dB) {
            this.DB = dB;
        }

        public (List<Notification>, int) GetNotificationsLevelAndTimeWithCount(int pageIndex, int pageSize, NotificationLevel searchLevel, string startDate, string endDate) {
            DateTime startDateTime = Convert.ToDateTime(startDate);
            DateTime endDateTime = Convert.ToDateTime(endDate);

            List<Notification> list = new List<Notification>();
            if (searchLevel == 0) {
                list = (List<Notification>) this.GetAll();
            }
            else {
                list = (List<Notification>) this.GetAll(item => item.Level == searchLevel);
            }

            foreach (var item in list) {
                DateTime date = Convert.ToDateTime(item.CreateDate);
                if (!(DateTime.Compare(date, startDateTime) > 0 && (DateTime.Compare(date, endDateTime) < 0))) {
                    list.Remove(item);
                }
            }

            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            foreach (var notification in pagelist) {
                this.DB.Entry(notification).Reference(item => item.Receiver).Load();
                this.DB.Entry(notification).Reference(item => item.Sender).Load();
            }
            return (pagelist, count);
        }

    }
}
