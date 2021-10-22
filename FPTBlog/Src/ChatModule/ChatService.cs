using System.Collections.Generic;
using FPTBlog.Src.ChatModule.Entity;
using FPTBlog.Src.ChatModule.Interface;

namespace FPTBlog.Src.ChatModule {
    public class ChatService : IChatService {
        private readonly IChatRepository ChatRepository;
        public ChatService(IChatRepository chatRepository) {
            this.ChatRepository = chatRepository;
        }

        #region Add, Update, Remove
        public void AddMessage(Message message) => this.ChatRepository.Add(message);
        public void UpdateMessage(Message message) => this.ChatRepository.Update(message);
        public void RemoveMessage(Message message) => this.ChatRepository.Remove(message);
        # endregion

        public (List<Message>, int) GetUserMessages(string userId) {
            List<Message> messages = (List<Message>) this.ChatRepository.GetAll(item => item.OwnerId == userId);
            int count = messages.Count;
            return (messages, count);
        }
    }
}