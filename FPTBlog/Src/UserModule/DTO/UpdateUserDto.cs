using FluentValidation;
using FPTBlog.Utils.Validator;
using System.Text.RegularExpressions;

namespace FPTBlog.Src.UserModule.DTO
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Address).NotEmpty().NotNull().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX);
            RuleFor(x => x.Phone).NotEmpty().NotNull().Matches(new Regex(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b"));
        }
    }
}