using System.Globalization;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Extension
{
    public static class IntExtension
    {
        public static DateTime? ToDateTime(this int target, string format = "")
        {
            string text = target.ToString();
            if (string.IsNullOrWhiteSpace(format))
            {
                if (text.Length == 8)
                {
                    format = "yyyyMMdd";
                }
                else if (text.Length == 6)
                {
                    format = "yyyyMM";
                }
                else
                {
                    if (text.Length != 4)
                    {
                        return null;
                    }

                    format = "yyyy";
                }
            }

            if (DateTime.TryParseExact(target.ToString(), format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }

            return null;
        }
        public static int? ToInt(this DateTime value)
        {
            return int.Parse(value.ToString("yyyyMMdd"));
        }
    }
}
