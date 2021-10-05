using FluentValidation;

namespace FPTBlog.Src.RewardModule.DTO {
    public class GiveUserRewardDto {
        public string UserId {
            get; set;
        }
        public string RewardId {
            get; set;
        }
    }

    public class GiveUserRewardDtoValidator : AbstractValidator<GiveUserRewardDto> {
        public GiveUserRewardDtoValidator() {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.RewardId).NotEmpty().NotNull();
        }
    }
}