using FluentValidation;


namespace FPTBlog.Src.UserModule.DTO {
    public class FollowUnfollowUserDto {
        public string FollowerId {
            get; set;
        }
    }
    public class FollowUnfollowUserDtoValidator : AbstractValidator<FollowUnfollowUserDto> {
        public FollowUnfollowUserDtoValidator() {
            RuleFor(x => x.FollowerId).NotEmpty().NotNull();
        }
    }
}
