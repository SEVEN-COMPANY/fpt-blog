using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Microsoft.AspNetCore.Mvc;



namespace FPTBlog.Utils.Common {
    public class ServerMvcResponse {
        public static void SetFieldErrorMessage(string field, string key, ViewDataDictionary dataView) {
            string value = ValidatorOptions.Global.LanguageManager.GetString(key);
            dataView[$"{field}Error"] = value;
        }

        public static void SetMessage(string key, ViewDataDictionary dataView) {
            string value = ValidatorOptions.Global.LanguageManager.GetString(key);
            dataView["message"] = value;
        }

        public static void SetErrorMessage(string errorKey, ViewDataDictionary dataView) {
            string value = ValidatorOptions.Global.LanguageManager.GetString(errorKey);
            dataView["errorMessage"] = value;
        }

        public string getRedirectWithMessage(string link, string message, string errorMessage) {

            return $"{link}?message={message}&errorMessage={errorMessage}";
        }


        public static void MapDetails(ValidationResult result, ViewDataDictionary dataView) {
            foreach (var failure in result.Errors) {
                string field = failure.PropertyName;
                string message = Helper.StringFormat(failure.ErrorMessage, failure.FormattedMessagePlaceholderValues);
                dataView[$"{field}Error"] = message;
            }
        }
    }
}
