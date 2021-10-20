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

        public (List<Notification>, int) GetNotificationsLevelAndTimeWithCount(int pageIndex, int pageSize, string search, NotificationLevel searchLevel, string startDate, string endDate) {
            DateTime startDateTime = Convert.ToDateTime(startDate);
            DateTime endDateTime = Convert.ToDateTime(endDate);

            List<Notification> resultList = new List<Notification>();

            List<Notification> list = new List<Notification>();
            if (searchLevel == 0) {
                list = (List<Notification>) this.GetAll();
            }
            else {
                list = (List<Notification>) this.GetAll(item => item.Level == searchLevel);
            }

            foreach (var item in list) {
                this.DB.Entry(item).Reference(item => item.Receiver).Load();
                this.DB.Entry(item).Reference(item => item.Sender).Load();

                DateTime date = Convert.ToDateTime(item.CreateDate);
                if ((DateTime.Compare(date, startDateTime) > 0 && (DateTime.Compare(date, endDateTime) < 0)) && (item.Receiver.Username.Contains(search) || item.Receiver.Email.Contains(search) || item.NotificationId.Contains(search))) {
                    resultList.Add(item);
                }
            }

            var count = resultList.Count();
            var pagelist = resultList.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            return (pagelist, count);
        }

    }
}
