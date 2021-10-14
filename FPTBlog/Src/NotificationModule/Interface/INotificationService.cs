using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.NotificationModule.Interface {
    public interface INotificationService {
        public void AddNotification(Notification notification);
        public void UpdateCategory(Notification notification);
        public void RemoveCategory(Notification notification);
    }
}