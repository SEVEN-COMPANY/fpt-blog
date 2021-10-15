using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.NotificationModule {
    public class NotificationService : INotificationService {
        private readonly INotificationRepository NotificationRepository;

        public NotificationService(INotificationRepository notificationRepository) {

            this.NotificationRepository = notificationRepository;
        }

        public void AddNotification(Notification notification) => this.NotificationRepository.Add(notification);
        public void UpdateCategory(Notification notification) => this.NotificationRepository.Update(notification);
        public void RemoveCategory(Notification notification) => this.NotificationRepository.Remove(notification);
    }
}
