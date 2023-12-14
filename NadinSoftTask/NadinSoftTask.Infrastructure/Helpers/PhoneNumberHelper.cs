using System.Text.RegularExpressions;

namespace NadinSoftTask.Infrastructure.Helpers
{
    public static class PhoneNumberHelper
    {
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            var regex = new Regex("^0[0-9]{2,}[0-9]{7,}$");
            return regex.Match(phoneNumber).Success;
        }
    }
}