using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using FPTBlog.Utils.Validator;
using FPTBlog.Utils.Locale;

namespace FPTBlog.Src.AuthModule.DTO
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
    }

    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX);
            RuleFor(x => x.Password).NotEmpty().NotNull().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX)
            .Custom((value, context) =>
            {
                bool hasUpperCaseLetter = false;
                bool hasLowerCaseLetter = false;
                bool hasDecimalDigit = false;
                bool hasWhiteSpace = false;

                if (value == null)
                {
                    return;
                }

                foreach (char c in value)
                {
                    if (char.IsUpper(c)) {
                        hasUpperCaseLetter = true;
                    }
                    if (char.IsLower(c)) {
                         hasLowerCaseLetter = true;
                
                    }
                    if (char.IsDigit(c))  {
                        hasDecimalDigit = true;
                    }
                    if (char.IsWhiteSpace(c))  {
                        hasWhiteSpace = true;
                    }
                }

                if (!hasDecimalDigit || !hasLowerCaseLetter || !hasUpperCaseLetter)
                {
                    string errorMessage = ValidatorOptions.Global.LanguageManager.GetString(CustomLanguageValidator.ErrorMessageKey.ERROR_PASSWORD_CONTAIN_CHARACTER);
                    context.AddFailure(new ValidationFailure("password", errorMessage));
                }

                if (hasWhiteSpace)
                {
                    string errorMessage = ValidatorOptions.Global.LanguageManager.GetString(CustomLanguageValidator.ErrorMessageKey.ERROR_PASSWORD_CONTAIN_WHITESPACE);
                    context.AddFailure(new ValidationFailure("password", errorMessage));
                }
            });
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
            RuleFor(x => x.Name).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
        }
    }
}
