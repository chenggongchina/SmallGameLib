using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Media.Imaging;
using System.Reflection;

namespace GDE.SmallGameLib
{
    /// <summary>
    /// 图片工具集
    /// </summary>
    static public class ImageTools
    {
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="address">图片地址</param>
        /// <returns>图片</returns>
        static public BitmapSource GetImage(string address)
        {
            return new BitmapImage(new Uri(string.Format(@"..{0}{1}", 
                Config.getInstance().ImageUrl,address), UriKind.Relative));
        }

        /// <summary>
        /// 字符串转颜色
        /// </summary>
        /// <param name="colorString">颜色字符串</param>
        /// <param name="myColor">out 颜色</param>
        /// <returns>是否转换成功</returns>
        static public bool getColorFromString(string colorString, out Color myColor)
        {
            myColor = new Color();
            try
            {
                if (colorString.StartsWith("#"))
                {
                    colorString = colorString.Replace("#", string.Empty);
                    int v = int.Parse(colorString, System.Globalization.NumberStyles.HexNumber);
                    myColor = new Color()
                    {
                        A = Convert.ToByte((v >> 24) & 255),
                        R = Convert.ToByte((v >> 16) & 255),
                        G = Convert.ToByte((v >> 8) & 255),
                        B = Convert.ToByte((v >> 0) & 255)
                    };
                    return true;
                }
                else
                {
                    Type colorType = (typeof(System.Windows.Media.Colors));
                    if (colorType.GetProperty(colorString) != null)
                    {
                        object color = colorType.InvokeMember(colorString, BindingFlags.GetProperty, null, null, null);
                        if (color != null)
                        {
                            myColor = (Color)color;
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
