using FluentValidation;


namespace FPTBlog.Src.CommentModule.DTO {
    public class GetCommentOfPostDto {
        public string PostId {
            get; set;
        }
    }
    public class GetCommentOfPostDtoValidator : AbstractValidator<GetCommentOfPostDto> {
        public GetCommentOfPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
