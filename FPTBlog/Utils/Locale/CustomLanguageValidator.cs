namespace FPTBlog.Utils.Locale {
    public class CustomLanguageValidator : FluentValidation.Resources.LanguageManager {
        public static class ErrorMessageKey {
            public static readonly string ERROR_LOGIN_FAIL = "ERROR_LOGIN_FAIL";
            public static readonly string ERROR_EXISTED = "ERROR_EXISTED ";
            public static readonly string ERROR_FAIL_TO_SAVE = "ERROR_FAIL_TO_SAVE";
            public static readonly string ERROR_UPDATE_FAIL = "ERROR_UPDATE_FAIL";
            public static readonly string ERROR_DELETE_FAIL = "ERROR_DELETE_FAIL";
            public static readonly string ERROR_WRONG = "ERROR_WRONG";
            public static readonly string ERROR_DISSABLED_ACCOUNT = "ERROR_DISSABLED_ACCOUNT";
            public static readonly string ERROR_NOT_FOUND = "ERROR_NOT_FOUND";
            public static readonly string ERROR_NOT_ALLOW = "ERROR_NOT_ALLOW";
            public static readonly string ERROR_PASSWORD_CONTAIN_CHARACTER = "ERROR_PASSWORD_CONTAIN_CHARACTER";
            public static readonly string ERROR_PASSWORD_CONTAIN_WHITESPACE = "ERROR_PASSWORD_CONTAIN_WHITESPACE";
            public static readonly string ERROR_OLD_PASSWORD_IS_WRONG = "ERROR_OLD_PASSWORD_IS_WRONG";
            public static readonly string FILE_TOO_LARGE = "FILE_TOO_LARGE";
            public static readonly string FILE_WRONG_EXTENSION = "FILE_WRONG_EXTENSION";
        }

        public static class MessageKey {
            public static readonly string MESSAGE_UNFOLLOW_SUCCESS = "MESSAGE_UNFOLLOW_SUCCESS";
            public static readonly string MESSAGE_FOLLOW_SUCCESS = "MESSAGE_FOLLOW_SUCCESS";
            public static readonly string MESSAGE_POSTED_SUCCESS = "MESSAGE_POSTED_SUCCESS";
            public static readonly string MESSAGE_LOGIN_SUCCESS = "MESSAGE_LOGIN_SUCCESS";
            public static readonly string MESSAGE_REGISTER_SUCCESS = "MESSAGE_REGISTER_SUCCESS";
            public static readonly string MESSAGE_LOGOUT_SUCCESS = "MESSAGE_LOGOUT_SUCCESS";
            public static readonly string MESSAGE_UPDATE_SUCCESS = "MESSAGE_UPDATE_SUCCESS";
            public static readonly string MESSAGE_ADD_SUCCESS = "MESSAGE_ADD_SUCCESS";
            public static readonly string MESSAGE_DELETE_SUCCESS = "MESSAGE_DELETE_SUCCESS";
            public static readonly string MESSAGE_SAVE_SUCCESS = "MESSAGE_SAVE_SUCCESS";
        }
        public CustomLanguageValidator() {

            // Success message
            // EN
            AddTranslation("en", MessageKey.MESSAGE_FOLLOW_SUCCESS, "follow success");
            AddTranslation("en", MessageKey.MESSAGE_UNFOLLOW_SUCCESS, "unfollow success");
            AddTranslation("en", MessageKey.MESSAGE_POSTED_SUCCESS, "post success");
            AddTranslation("en", MessageKey.MESSAGE_LOGIN_SUCCESS, "login success");
            AddTranslation("en", MessageKey.MESSAGE_REGISTER_SUCCESS, "register success");
            AddTranslation("en", MessageKey.MESSAGE_LOGOUT_SUCCESS, "logout success");
            AddTranslation("en", MessageKey.MESSAGE_UPDATE_SUCCESS, "update success");
            AddTranslation("en", MessageKey.MESSAGE_ADD_SUCCESS, "add success");
            AddTranslation("en", MessageKey.MESSAGE_DELETE_SUCCESS, "delete success");
            AddTranslation("en", MessageKey.MESSAGE_SAVE_SUCCESS, "save success");

            // VI
            AddTranslation("vi", MessageKey.MESSAGE_LOGIN_SUCCESS, "đăng nhập thành công");
            AddTranslation("vi", MessageKey.MESSAGE_REGISTER_SUCCESS, "đăng kí thành công");
            AddTranslation("vi", MessageKey.MESSAGE_LOGOUT_SUCCESS, "đăng xuất thành công");
            AddTranslation("vi", MessageKey.MESSAGE_UPDATE_SUCCESS, "cập nhật thành công");
            AddTranslation("vi", MessageKey.MESSAGE_ADD_SUCCESS, "thêm mới thành công");
            AddTranslation("vi", MessageKey.MESSAGE_DELETE_SUCCESS, "xóa thành công");

            // Error message
            // EN
            AddTranslation("en", ErrorMessageKey.ERROR_LOGIN_FAIL, "username or password is wrong");
            AddTranslation("en", ErrorMessageKey.ERROR_EXISTED, "is already exist");
            AddTranslation("en", ErrorMessageKey.ERROR_FAIL_TO_SAVE, "database error");
            AddTranslation("en", ErrorMessageKey.ERROR_UPDATE_FAIL, "update fail");
            AddTranslation("en", ErrorMessageKey.ERROR_DELETE_FAIL, "delete fail");
            AddTranslation("en", ErrorMessageKey.ERROR_WRONG, "is wrong");
            AddTranslation("en", ErrorMessageKey.ERROR_DISSABLED_ACCOUNT, "your account has been disabled, if you have a problem please contact");
            AddTranslation("en", ErrorMessageKey.ERROR_NOT_FOUND, "is not found");
            AddTranslation("en", ErrorMessageKey.ERROR_NOT_ALLOW, "not allow");
            AddTranslation("en", ErrorMessageKey.ERROR_PASSWORD_CONTAIN_CHARACTER, "should contain at least 1 uppercase, 1 lowercase, 1 number");
            AddTranslation("en", ErrorMessageKey.ERROR_PASSWORD_CONTAIN_WHITESPACE, "should not contain white space");
            AddTranslation("en", ErrorMessageKey.ERROR_OLD_PASSWORD_IS_WRONG, "old password is wrong");
            AddTranslation("en", ErrorMessageKey.FILE_TOO_LARGE, "file is too large");
            AddTranslation("en", ErrorMessageKey.FILE_WRONG_EXTENSION, "file is wrong extension");

            // VI
            AddTranslation("vi", ErrorMessageKey.ERROR_LOGIN_FAIL, "username hoặc password không đúng");
            AddTranslation("vi", ErrorMessageKey.ERROR_EXISTED, "đã tồn tại");
            AddTranslation("vi", ErrorMessageKey.ERROR_FAIL_TO_SAVE, "lỗi cơ sở dữ liệu");
            AddTranslation("vi", ErrorMessageKey.ERROR_UPDATE_FAIL, "cập nhật không thành công");
            AddTranslation("vi", ErrorMessageKey.ERROR_DELETE_FAIL, "xóa không thành công");
            AddTranslation("vi", ErrorMessageKey.ERROR_WRONG, "không đúng");
            AddTranslation("vi", ErrorMessageKey.ERROR_DISSABLED_ACCOUNT, "tài khoản của bạn đã bị vô hiệu hóa, nếu bạn có vấn đề muốn phản hồi, xin hãy liên lạc");
            AddTranslation("vi", ErrorMessageKey.ERROR_NOT_FOUND, "không tìm thấy");
            AddTranslation("vi", ErrorMessageKey.ERROR_NOT_ALLOW, "không có quyền");
            AddTranslation("vi", ErrorMessageKey.ERROR_PASSWORD_CONTAIN_CHARACTER, "phải có ít nhất 1 kí tự hoa, 1 kí tự thường, 1 số");
            AddTranslation("vi", ErrorMessageKey.ERROR_PASSWORD_CONTAIN_WHITESPACE, "không được chứa khoảng trắng");
            AddTranslation("vi", ErrorMessageKey.ERROR_OLD_PASSWORD_IS_WRONG, "mật khẩu cũ không đúng");

            // Don't touch me please
            AddTranslation("en", "EmailValidator", "is not a valid email address");
            AddTranslation("en", "GreaterThanOrEqualValidator", "should be greater than or equal to {ComparisonValue}");
            AddTranslation("en", "GreaterThanValidator", "should be greater than {ComparisonValue}");
            AddTranslation("en", "LengthValidator", "should be between {MinLength} and {MaxLength} characters");
            AddTranslation("en", "MinimumLengthValidator", "should be at least {MinLength} characters");
            AddTranslation("en", "MaximumLengthValidator", "should be {MaxLength} characters or fewer");
            AddTranslation("en", "LessThanOrEqualValidator", "should be less than or equal to {ComparisonValue}");
            AddTranslation("en", "LessThanValidator", "should be less than {ComparisonValue}");
            AddTranslation("en", "NotEmptyValidator", "should not be empty");
            AddTranslation("en", "NotEqualValidator", "should not be equal to {ComparisonValue}");
            AddTranslation("en", "NotNullValidator", "should not be empty");
            AddTranslation("en", "RegularExpressionValidator", "is not in the correct format");
            AddTranslation("en", "EqualValidator", "should be equal to {ComparisonValue}");
            AddTranslation("en", "ExactLengthValidator", "should be equal to {ComparisonValue}");
            AddTranslation("en", "InclusiveBetweenValidator", "should be between {From} and {To}");
            AddTranslation("en", "ExclusiveBetweenValidator", "should be between {From} and {To} (exclusive)");
            AddTranslation("en", "NullValidator", "must be empty");
            AddTranslation("en", "EmptyValidator", "must be empty");
            AddTranslation("en", "EnumValidator", "has a range of values which does not include {PropertyValue}");


            AddTranslation("vi", "EmailValidator", "không hợp lệ");
            AddTranslation("vi", "GreaterThanOrEqualValidator", "phải lớn hơn hoặc bằng với {ComparisonValue}");
            AddTranslation("vi", "GreaterThanValidator", "phải lớn hơn {ComparisonValue}");
            AddTranslation("vi", "LengthValidator", "phải nằm trong khoảng từ {MinLength} đến {MaxLength} kí tự");
            AddTranslation("vi", "MinimumLengthValidator", "tối thiểu {MinLength} kí tự");
            AddTranslation("vi", "MaximumLengthValidator", "tối đa  {MaxLength} kí tự");
            AddTranslation("vi", "LessThanOrEqualValidator", "phải nhỏ hơn hoặc bằng {ComparisonValue}");
            AddTranslation("vi", "LessThanValidator", "phải nhỏ hơn {ComparisonValue}");
            AddTranslation("vi", "NotEmptyValidator", "không được rỗng");
            AddTranslation("vi", "NotEqualValidator", "không được bằng {ComparisonValue}");
            AddTranslation("vi", "NotNullValidator", "phải có giá trị");
            AddTranslation("vi", "RegularExpressionValidator", "không đúng định dạng");
            AddTranslation("vi", "EqualValidator", "phải bằng {ComparisonValue}");
            AddTranslation("vi", "ExactLengthValidator", "phải có độ dài chính xác {MaxLength} kí tự");
            AddTranslation("vi", "InclusiveBetweenValidator", "phải có giá trị trong khoảng từ {From} đến {To}");
            AddTranslation("vi", "ExclusiveBetweenValidator", "phải có giá trị trong khoảng giữa {From} và {To}");
            AddTranslation("vi", "EmptyValidator", "phải là rỗng");
            AddTranslation("vi", "NullValidator", "không được chứa giá trị");
            AddTranslation("vi", "EnumValidator", "nằm trong một tập giá trị không bao gồm {PropertyValue}");
        }
    }
}
