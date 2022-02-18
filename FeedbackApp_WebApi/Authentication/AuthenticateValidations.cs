using System;

namespace FeedbackApp.WebApi.Authentication
{
    public static class AuthenticateValidations
    {
        /// <summary>
        /// Check password Requirements
        /// </summary>
        /// <param name="password"></param>
        /// <returns>true when password meet requirements</returns>
        public static bool CheckPwRequirements(string password)
        {
            bool containsUpper = false;
            bool containsLower = false;
            bool containsDigit = false;
            bool containsWhiteSpace = false;

            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < 6) return false;

            foreach (char character in password)
            {
                if (char.IsWhiteSpace(character)) containsWhiteSpace = true;
                if (char.IsDigit(character)) containsDigit = true;
                if (char.IsUpper(character)) containsUpper = true;
                if (char.IsLower(character)) containsLower = true;
            }

            if (!containsWhiteSpace && containsDigit && containsUpper && containsLower)
                return true;

            else return false;
        }

        public static bool CheckUsernameRequirements(string userName)
        {
            bool containsWhiteSpace = false;
            bool containsNotAllowedChars = false;

            if (string.IsNullOrEmpty(userName)) return false;
            if (userName.Length < 6 || userName.Length > 26) return false;

            foreach (char character in userName)
            {
                if (char.IsWhiteSpace(character)) containsWhiteSpace = true;
                if (!char.IsLetterOrDigit(character)) containsNotAllowedChars = true;
            }

            if (!containsWhiteSpace && !containsNotAllowedChars)
                return true;

            else return false;
        }
    }
}
