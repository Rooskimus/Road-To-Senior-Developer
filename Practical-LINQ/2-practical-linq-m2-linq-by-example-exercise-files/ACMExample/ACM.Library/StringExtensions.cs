using System.Globalization;
using System.Threading;

namespace ACM.Library
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a list of strings to title case
        /// </summary>
        public static string ConvertToTitleCase(this string source)
        {
            CultureInfo cultureInfo   = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(source);
        }
    }
}
