using System.Text.RegularExpressions;

namespace NadinSoftTask.Infrastructure
{
    public static class PhoneNumberHelper
    {
        public static bool IsValidPhoneNumber(this string email)
        {
            var regex = new Regex("[^09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}]");
            return regex.Match(email).Success;
        }
    }
}