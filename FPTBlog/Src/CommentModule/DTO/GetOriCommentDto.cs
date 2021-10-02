using FluentValidation;


namespace FPTBlog.Src.CommentModule.DTO {
    public class GetOriCommentDto {
        public string PostId {
            get; set;
        }
    }
    public class GetOriCommentDtoValidator : AbstractValidator<GetOriCommentDto> {
        public GetOriCommentDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
