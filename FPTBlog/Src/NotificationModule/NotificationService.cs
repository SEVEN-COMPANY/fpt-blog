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
        public Notification GetNotificationByNotificationId(string notificationId) => this.NotificationRepository.GetFirstOrDefault(item => item.NotificationId == notificationId, includeProperties: "Sender,Receiver");

        public (List<Notification>, int) GetUserNotification(string userId) {
            var notifications = this.NotificationRepository.GetAll(item => item.ReceiverId == userId).OrderByDescending(item => item.CreateDate).ToList();
            int count = notifications.Count;
            return (notifications, count);
        }
        public (List<Notification>, int) GetUserNotificationTimeWithCount(string userId, int pageIndex, int pageSize, string search, string startDate, string endDate) => this.NotificationRepository.GetUserNotificationTimeWithCount(userId, pageIndex, pageSize, search, startDate, endDate);

        public (List<Notification>, int) GetNotificationsLevelAndTimeWithCount(int pageIndex, int pageSize, string search, NotificationLevel searchLevel, string startDate, string endDate) => this.NotificationRepository.GetNotificationsLevelAndTimeWithCount(pageIndex, pageSize, search, searchLevel, startDate, endDate);

        public List<SelectListItem> GetNotificationLevelDropList() {
            var level = new List<SelectListItem>(){
                new SelectListItem(){ Value = NotificationLevel.BANNED.ToString(), Text = "Banned"},
                new SelectListItem(){  Value =  NotificationLevel.INFORMATION.ToString(), Text = "Information"},
                new SelectListItem(){  Value =  NotificationLevel.WARNING.ToString(), Text = "Warning"}
            };

            return level;
        }
    }
}
