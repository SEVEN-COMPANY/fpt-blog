using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.NotificationModule.Entity;
using System;

namespace FPTBlog.Src.NotificationModule {
    public class NotificationService : INotificationService {
        private readonly INotificationRepository NotificationRepository;

        public NotificationService(INotificationRepository notificationRepository) {

            this.NotificationRepository = notificationRepository;
        }

        public void AddNotification(Notification notification) => this.NotificationRepository.Add(notification);
        public void UpdateCategory(Notification notification) => this.NotificationRepository.Update(notification);
        public void RemoveCategory(Notification notification) => this.NotificationRepository.Remove(notification);

        public (List<Notification>, int) GetUserNotification(string userId) {
            var notifications = this.NotificationRepository.GetAll(item => item.ReceiverId == userId).ToList();
            int count = notifications.Count;
            return (notifications, count);
        }

        public (List<Notification>, int) GetNotificationsLevelAndTimeWithCount(int pageIndex, int pageSize, NotificationLevel searchLevel, string startDate, string endDate) => this.NotificationRepository.GetNotificationsLevelAndTimeWithCount(pageIndex, pageSize, searchLevel, startDate, endDate);

        public List<SelectListItem> GetNotificationLevelDropList() {
            var level = new List<SelectListItem>(){
                new SelectListItem(){ Value = NotificationLevel.BANED.ToString(), Text = "Baned"},
                new SelectListItem(){  Value =  NotificationLevel.INFOMATION.ToString(), Text = "Infomation"},
                new SelectListItem(){  Value =  NotificationLevel.WARNING.ToString(), Text = "Warning"}
            };

            return level;
        }
    }
}
