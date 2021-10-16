using FluentValidation;
using FPTBlog.Src.RewardModule.Entity;
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

        public RewardType Type {get;set;}

        public int Constraint {get;set;}

    }
    public class UpdateRewardDtoValidator : AbstractValidator<UpdateRewardDto> {
        public UpdateRewardDtoValidator() {
            RuleFor(x => x.RewardId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Type).NotEmpty().NotNull();
            RuleFor(x => x.Constraint).NotEmpty().NotNull();
        }
    }
}
