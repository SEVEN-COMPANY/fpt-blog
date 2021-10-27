using System.Collections.Generic;
using FPTBlog.Src.ChatModule.Entity;

namespace FPTBlog.Src.ChatModule.Interface {
    public interface IChatService {
        #region Add, Update, Remove
        public void AddMessage(Message message);
        public void UpdateMessage(Message message);
        public void RemoveMessage(Message message);
        #endregion

        public (List<Message>, int) GetUserMessages(string userId);

    }
}