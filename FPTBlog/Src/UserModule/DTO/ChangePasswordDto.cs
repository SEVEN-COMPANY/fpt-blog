﻿using FluentValidation;
using FluentValidation.Results;
using FPTBlog.Utils.Locale;
using FPTBlog.Utils.Validator;

namespace FPTBlog.Src.UserModule.DTO {
    public class ChangePasswordDto {
        public string OldPassword {
            get; set;
        }
        public string NewPassword {
            get; set;
        }
        public string ConfirmNewPassword {
            get; set;
        }
    }

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto> {
        public ChangePasswordDtoValidator() {
            RuleFor(x => x.OldPassword).NotEmpty().NotNull().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX);
            RuleFor(x => x.NewPassword).NotEmpty().NotNull().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX)
            .Custom((value, context) => {
                bool hasUpperCaseLetter = false;
                bool hasLowerCaseLetter = false;
                bool hasDecimalDigit = false;
                bool hasWhiteSpace = false;

                if (value == null) {
                    return;
                }

                foreach (char c in value) {
                    if (char.IsUpper(c)) {
                        hasUpperCaseLetter = true;
                    }
                    if (char.IsLower(c)) {
                        hasLowerCaseLetter = true;
                    }
                    if (char.IsDigit(c)) {
                        hasDecimalDigit = true;
                    }
                    if (char.IsWhiteSpace(c)) {
                        hasWhiteSpace = true;
                    }
                }

                if (!hasDecimalDigit || !hasLowerCaseLetter || !hasUpperCaseLetter) {
                    string errorMessage = ValidatorOptions.Global.LanguageManager.GetString(CustomLanguageValidator.ErrorMessageKey.ERROR_PASSWORD_CONTAIN_CHARACTER);
                    context.AddFailure(new ValidationFailure("newPassword", errorMessage));
                }

                if (hasWhiteSpace) {
                    string errorMessage = ValidatorOptions.Global.LanguageManager.GetString(CustomLanguageValidator.ErrorMessageKey.ERROR_PASSWORD_CONTAIN_WHITESPACE);
                    context.AddFailure(new ValidationFailure("newPassword", errorMessage));
                }
            });
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().Equal(x => x.NewPassword);
        }
    }
}
