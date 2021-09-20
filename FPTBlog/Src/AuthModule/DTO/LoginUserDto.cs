using FluentValidation;
using FPTBlog.Utils.Validator;



namespace FPTBlog.Src.AuthModule.DTO
{
    public class LoginUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX);
            RuleFor(x => x.Password).NotEmpty().NotNull().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX);
        }
    }
}