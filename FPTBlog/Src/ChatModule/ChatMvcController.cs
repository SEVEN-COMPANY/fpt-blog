using FPTBlog.Src.ChatModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.ChatModule {
    [Route("chat")]
    public class ChatMvcController : Controller {
        private readonly IChatService ChatService;
        public ChatMvcController(IChatService chatService) {
            this.ChatService = chatService;
        }

        [HttpGet("me")]
        public IActionResult GetChat() {
            User user = (User) this.ViewData["user"];
            var (messages, total) = this.ChatService.GetUserMessages(user.UserId);

            this.ViewData["messages"] = messages;
            this.ViewData["total"] = total;

            return View();
        }
    }
}