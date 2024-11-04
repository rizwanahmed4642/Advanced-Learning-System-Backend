using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.COMMON
{
    public static class PasswordGenerator
    {
        private const string LOWER_CASE = "abcdefghijklmnopqrstuvwxyz";
        private const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "0123456789";
        private const string SPECIALS = "!@#$%^&*()-_=+[]{};:,.<>?";

        public static string GeneratePassword(int length, bool includeLowercase, bool includeUppercase, bool includeNumbers, bool includeSpecials)
        {
            if (length <= 0) throw new ArgumentException("Password length must be greater than zero.");

            StringBuilder charSet = new StringBuilder();
            if (includeLowercase) charSet.Append(LOWER_CASE);
            if (includeUppercase) charSet.Append(UPPER_CASE);
            if (includeNumbers) charSet.Append(NUMBERS);
            if (includeSpecials) charSet.Append(SPECIALS);

            if (charSet.Length == 0) throw new ArgumentException("At least one character type must be selected.");

            Random random = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(charSet.Length);
                password[i] = charSet[index];
            }

            return new string(password);
        }
    }
}
