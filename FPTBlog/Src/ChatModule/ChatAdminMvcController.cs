using FPTBlog.Src.AuthModule;
using FPTBlog.Src.ChatModule.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.ChatModule {
    [Route("/admin/chat")]
    [ServiceFilter(typeof(AuthGuard))]
    public class ChatAdminMvcController : Controller {

        private readonly IChatService ChatService;

        public ChatAdminMvcController(IChatService chatService) {
            this.ChatService = chatService;
        }


    }
}