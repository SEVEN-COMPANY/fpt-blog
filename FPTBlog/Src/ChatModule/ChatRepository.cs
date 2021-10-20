using FPTBlog.Src.ChatModule.Entity;
using FPTBlog.Src.ChatModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.ChatModule {
    public class ChatRepository : Repository<Message>, IChatRepository {
        private readonly DB Db;
        public ChatRepository(DB Db) : base(Db) {
            this.Db = Db;
        }

    }
}