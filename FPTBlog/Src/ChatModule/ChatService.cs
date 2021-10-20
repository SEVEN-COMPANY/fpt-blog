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
    }
}