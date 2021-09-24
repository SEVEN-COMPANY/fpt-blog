using FluentValidation;
using FPTBlog.Utils.Validator;

namespace FPTBlog.Src.UserModule.DTO
{
    public class BlockUserDto
    {
        public string UserIdBlock { get; set; }
    }
    public class BlockUserDtoValidator : AbstractValidator<BlockUserDto>
    {
        public BlockUserDtoValidator()
        {
            RuleFor(x => x.UserIdBlock).NotEmpty().NotNull();
        }
    }
}