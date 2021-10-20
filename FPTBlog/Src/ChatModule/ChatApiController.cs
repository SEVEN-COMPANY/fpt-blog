using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.ChatModule.DTO;
using FPTBlog.Src.ChatModule.Entity;
using FPTBlog.Src.ChatModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.ChatModule {
    [ApiController]
    [Route("/api/chat")]
    [ServiceFilter(typeof(AuthGuard))]
    public class ChatApiController : Controller {
        private readonly IChatService ChatService;
        private readonly IUserService UserService;

        public ChatApiController(IChatService chatService, IUserService userService) {
            this.ChatService = chatService;
            this.UserService = userService;
        }

        [HttpPost("")]
        public ObjectResult AddMessageHandler([FromBody] AddMessageDTO body) {
            var res = new ServerApiResponse<Message>();

            ValidationResult result = new AddMessageDTOValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            User owner = this.UserService.GetUserByUserId(body.OwnerId);
            if (owner == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "ownerId");
                return new BadRequestObjectResult(res.getResponse());
            }

            Message message = new Message();
            message.Content = body.Content;
            User user = (User) this.ViewData["user"];

            message.SenderId = user.UserId;
            message.OwnerId = body.OwnerId;

            this.ChatService.AddMessage(message);

            res.data = message;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}