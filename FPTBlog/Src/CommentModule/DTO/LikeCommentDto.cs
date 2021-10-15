using FluentValidation;

namespace FPTBlog.Src.CommentModule.DTO
{
    public class LikeCommentDto {
        public string CommentId {
            get; set;
        }
    }

    public class LikeCommentDtoValidator : AbstractValidator<LikeCommentDto> {
        public LikeCommentDtoValidator() {
            RuleFor(x => x.CommentId).NotEmpty().NotNull();
        }
    }
}
