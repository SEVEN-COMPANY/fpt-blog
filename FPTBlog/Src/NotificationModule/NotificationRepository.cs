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
    }
}
