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

using System.Xml.Linq;
using System.Linq;

namespace GDE.SmallGameLib
{

    /// <summary>
    /// 配置解析器
    /// </summary>
    public class Config
    {
        /// <summary>
        /// config文件对应地址
        /// </summary>
        public const string configUrl = "Config/Config.xml";

        /// <summary>
        /// singleton模式
        /// 获取Config实例
        /// </summary>
        public static Config getInstance()
        { 
            if( _config == null )
            {
                _config = new Config();
            }
            return _config;
        }

        private static Config _config = null;

        /// <summary>
        /// 私有构造，不允许实例化
        /// </summary>
        private Config()
        {
            ParseConfig();//解析config文件
        }

        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public int ScreenW;

        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int ScreenH;

        /// <summary>
        /// 游戏循环的间隔
        /// 单位：毫秒
        /// </summary>
        public int GameLoopTime;

        /// <summary>
        /// 物体包围框颜色
        /// </summary>
        public Color BounderColor;

        /// <summary>
        /// 图片存放位置
        /// </summary>
        public string ImageUrl;

        /// <summary>
        /// XML文档
        /// </summary>
        private XDocument doc;

        /// <summary>
        /// 解析Config文件
        /// </summary>
        private void ParseConfig()
        {
            doc = XDocument.Load( configUrl );

            ScreenW = GetAttributeAsInt("ScreenWidth");
            ScreenH = GetAttributeAsInt("ScreenHeight");
            GameLoopTime = GetAttributeAsInt("GameLoopTime");
            string borderColor = GetAttribute("ObjBorderColor");
            if (!ImageTools.getColorFromString(borderColor, out BounderColor))
                BounderColor = Colors.Red;

            ImageUrl = GetAttribute("ImageUrl");
            XElement statemachine = doc.Descendants("StateMachine").First<XElement>();

            var statuses = from u in statemachine.Descendants("Status") select u;
            foreach (var u in statuses)
            { 
                string s = u.Value;
                GameStatus.Add(s);
            }
        }

        #region XML解析工具

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="AttributeName">属性名</param>
        /// <returns></returns>
        private string GetAttribute(string AttributeName)
        {
            return doc.Descendants(AttributeName).First<XElement>().Value;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="AttributeName">属性名</param>
        /// <returns>整型</returns>
        private int GetAttributeAsInt(string AttributeName)
        {
            return Int32.Parse(doc.Descendants(AttributeName).First<XElement>().Value);
        }
        #endregion
    }
}
