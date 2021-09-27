using FluentValidation;

namespace FPTBlog.Src.TagModule.DTO {
    public class AddTagDto {
        public string Name {
            get; set;
        }
    }

    public class AddTagDtoValidator : AbstractValidator<AddTagDto> {
        public AddTagDtoValidator() {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(5, 30);
        }
    }
}
