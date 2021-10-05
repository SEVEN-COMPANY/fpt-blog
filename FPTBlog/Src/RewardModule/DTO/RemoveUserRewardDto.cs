using FluentValidation;

namespace FPTBlog.Src.RewardModule.DTO {
    public class RemoveUserRewardDto {
        public string UserId {
            get; set;
        }

        public string RewardId {
            get; set;
        }
    }

    public class RemoveUserRewardDtoValidator : AbstractValidator<RemoveUserRewardDto> {
        public RemoveUserRewardDtoValidator() {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.RewardId).NotEmpty().NotNull();
        }
    }
}