using System.Collections.Generic;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.ChatModule.Entity;
using FPTBlog.Src.ChatModule.Interface;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.ChatModule {
    [Route("/api/admin/chat")]
    [ServiceFilter(typeof(AuthGuard))]
    public class ChatAdminApiController : Controller {
        private readonly IChatService ChatService;
        public ChatAdminApiController(IChatService chatService) {
            this.ChatService = chatService;
        }

        [HttpGet("")]
        public ObjectResult GetUserChat(string userId) {
            var res = new ServerApiResponse<List<Message>>();
            var (messages, _) = this.ChatService.GetUserMessages(userId);

            res.data = messages;
            return new ObjectResult(res.getResponse());
        }
    }
}