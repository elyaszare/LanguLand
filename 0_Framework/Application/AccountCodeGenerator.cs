using System;
using System.Text;

namespace _0_Framework.Application
{
    public class AccountCodeGenerator
    {
        public static string Generate()
        {
            return $"{RandomNumber()}{RandomString(2)}{RandomNumberShort()}{RandomString(7)}{RandomNumber()}";
        }

        private static string RandomString(int length)
        {
            var strBuild = new StringBuilder();
            var random = new Random();

            for (var i = 0; i <= length; i++)
            {
                var flt = random.NextDouble();
                var shift = Convert.ToInt32(Math.Floor(25 * flt));
                var letter = Convert.ToChar(shift + 65);
                strBuild.Append(letter);
            }

            return strBuild.ToString().ToLower();
        }

        private static string RandomNumber()
        {
            var random = new Random();
            return random.Next(10000, 99999).ToString();
        }

        private static string RandomNumberShort()
        {
            var random = new Random();
            return random.Next(10, 99).ToString();
        }
    }
}
