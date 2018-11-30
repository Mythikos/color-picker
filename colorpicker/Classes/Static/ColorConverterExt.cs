using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker.Classes.Static
{
    public static class ColorConverterExt
    {
        /// <summary>
        /// Returns the plain name of the color if it can be found in the System.Drawing.Color class
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToPlainString(System.Drawing.Color color)
        {
            System.Drawing.Color definedColor;
            string colorName = "Unknown";

            foreach (PropertyInfo colorProperty in typeof(System.Drawing.Color).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (colorProperty.PropertyType == typeof(System.Drawing.Color))
                {
                    definedColor = (System.Drawing.Color)colorProperty.GetValue(null);
                    if (definedColor.ToArgb().Equals(color.ToArgb()))
                    {
                        colorName = definedColor.Name;
                        break;
                    }
                }
            }

            return colorName;
        }

        /// <summary>
        /// Returns the hex string value of the input color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHexString(System.Drawing.Color color) => $"#{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}";

        /// <summary>
        /// Provides the rgb string value of the input color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToRgbString(System.Drawing.Color color) => $"rgb({color.R.ToString("##0")}, {color.G.ToString("##0")}, {color.B.ToString("##0")})";

        /// <summary>
        /// Provides the Hsl string value of the input color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHslString(System.Drawing.Color color) => $"hsl({color.GetHue().ToString("##0")}, {color.GetSaturation().ToString("#0%")}, {color.GetBrightness().ToString("#0%")})";

        /// <summary>
        /// Converts the input System.Drawing.Color to System.Windows.Media.Brush
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static System.Windows.Media.Brush ColorToSolidColorBrush(System.Drawing.Color color) => new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
    }
}
