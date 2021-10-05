using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FPTBlog.Src.RewardModule.DTO {
    public class CreateRewardDto {

        public string Description {
            get; set;
        }
        public IFormFile File {
            get; set;
        }
    }

    public class CreateRewardDtoValidator : AbstractValidator<CreateRewardDto> {
        public CreateRewardDtoValidator() {
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.File).NotNull();
        }
    }
}