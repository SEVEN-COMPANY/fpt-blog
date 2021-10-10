using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FPTBlog.Src.RewardModule.DTO {
    public class UpdateRewardDto {
        public string RewardId {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string Description {
            get; set;
        }

        public IFormFile File {
            get; set;
        }
    }
    public class UpdateRewardDtoValidator : AbstractValidator<UpdateRewardDto> {
        public UpdateRewardDtoValidator() {
            RuleFor(x => x.RewardId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();

        }
    }
}
