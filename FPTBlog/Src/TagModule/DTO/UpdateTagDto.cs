using FluentValidation;

namespace FPTBlog.Src.TagModule.DTO
{
    public class UpdateTagDto
    {
        public string TagId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
    {
        public UpdateTagDtoValidator()
        {
            RuleFor(x => x.TagId).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(5, 30);
        }
    }
}