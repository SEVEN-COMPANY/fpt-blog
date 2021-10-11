using FluentValidation;

namespace FPTBlog.Src.RewardModule.DTO {
    public class DeleteRewardDto {
        public string RewardId {
            get; set;
        }
    }

    public class DeleteRewardDtoValidator : AbstractValidator<DeleteRewardDto> {
        public DeleteRewardDtoValidator() {
            RuleFor(x => x.RewardId).NotEmpty().NotNull();
        }
    }
}