using System.Text.RegularExpressions;

namespace NadinSoftTask.Infrastructure
{
    public static class EmailHelper
    {
        public static bool IsValidEmail(this string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.Match(email).Success;
        }
    }
}