using FluentValidation;
using FPTBlog.Src.TagModule.Entity;

namespace FPTBlog.Src.TagModule.DTO
{
    public class UpdateTagDto
    {
        public string TagId { get; set; }
        public string Name { get; set; }
        public TagStatus Status {get;set;}
    }

    public class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
    {
        public UpdateTagDtoValidator()
        {
            RuleFor(x => x.TagId).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(5, 30);
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }
    }
}