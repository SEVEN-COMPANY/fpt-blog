using FluentValidation;


namespace FPTBlog.Src.CommentModule.DTO {
    public class AddCommentDto {
        public string Content {
            get; set;
        }
        public string PostId {
            get; set;
        }
        public string OriCommentId {
            get; set;
        }
    }

    public class AddCommentDtoValidator : AbstractValidator<AddCommentDto> {
        public AddCommentDtoValidator() {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
