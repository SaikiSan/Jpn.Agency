using System.Linq;

namespace JPN.Core.Utils
{
    public static class StringUtils
    {
        public static string OnlyNumber(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
