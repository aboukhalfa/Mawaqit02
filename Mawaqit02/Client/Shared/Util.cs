using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mawaqit02.Client.Shared
{
    public class Util
    {
        public static IEnumerable<string> GetSalatArabicNames()
        {
            return new[] { "صبح", "شروق", "ظهر", "عصر", "غروب", "مغرب", "عشاء" };
        }

        public static int Color(string colorString)
        {
            try
            {
                return int.Parse(colorString.Substring(1));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static (byte r, byte g, byte b) ToRGB(string colorString)
        {
            try
            {
                var val = int.Parse(colorString.Substring(1), System.Globalization.NumberStyles.HexNumber);
                var r = (byte)(val / 0x10000);
                var g = (byte)((val & 0x00FFFF) / 0x100);
                var b = (byte)(val & 0xFF);
                return (r, g, b);
            }
            catch (Exception)
            {
                return (0, 0, 0);
            }
        }

        public static string FromRGB((byte r, byte g, byte b) value)
        {
            return $"#{value.r:X2}{value.g:X2}{value.b:X2}";
        }

        public static string Color(int colorInt)
        {
            try
            {
                return $"#{colorInt:X6}";
            }
            catch (Exception)
            {
                return "#000000";
            }
        }
    }
}
