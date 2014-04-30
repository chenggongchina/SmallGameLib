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

namespace GDE.SmallGameLib
{
    /// <summary>
    /// 数学工具集
    /// </summary>
    static public class MathTools
    {
        #region 碰撞检测

        /// <summary>
        /// 判断矩形是否相交
        /// </summary>
        /// <param name="rect1">矩形1</param>
        /// <param name="rect2">矩形2</param>
        /// <returns>是否相交</returns>
        static public bool IsHit(Rect rect1, Rect rect2)
        {
            rect1.Intersect(rect2);
            if (!double.IsInfinity(rect1.Height) && !double.IsInfinity(rect1.Width))
                return true;
            return false;
        }

        #endregion

        #region 随机及概率

        /// <summary>
        /// 随机数发生器
        /// </summary>
        static public Random rand = new Random();

        /// <summary>
        /// 生成一个在屏幕内的随机点
        /// </summary>
        /// <returns></returns>
        static public Point RandomWindowPoint()
        {
            return new Point(rand.Next(Config.getInstance().ScreenW), rand.Next(Config.getInstance().ScreenH));
        }

        /// <summary>
        /// 生成一个随机边界点
        /// 
        /// 在屏幕四周边界位置随机生成一个点
        /// </summary>
        /// <returns></returns>
        static public Point RandomBorderPoint()
        {
            Point point = RandomWindowPoint();
            int line = rand.Next(4);
            switch (line)
            { 
                case 0:
                    point.X = 0;
                    break;
                case 1:
                    point.Y = 0;
                    break;
                case 2:
                    point.X = Config.getInstance().ScreenW;
                    break;
                case 3:
                    point.Y = Config.getInstance().ScreenH;
                    break;  
            }
            return point;
        }

        /// <summary>
        /// 产生概率事件
        /// </summary>
        /// <param name="Base">概率基数，以 1/Base 发生该事件</param>
        /// <returns>true发生 false不发生</returns>
        static public bool IsProbEventHappen(int Base)
        {
            return (rand.Next(Base)==0);
        }

        /// <summary>
        /// 随机生成a到b之间的浮点数
        /// </summary>
        /// <param name="a">下限</param>
        /// <param name="b">上限</param>
        /// <returns>随机浮点数</returns>
        static public double RandomDouble(double a, double b)
        {
            double c = rand.NextDouble();
            return c * (b - a) + a;
        }

        /// <summary>
        /// 随机生成a到b之间的整数
        /// </summary>
        /// <param name="a">下限</param>
        /// <param name="b">上限</param>
        /// <returns>随机整数</returns>
        static public int RandomInt(int a, int b)
        {
            return rand.Next(a, b);
        }

        /// <summary>
        /// 生成一个模为length的随机向量
        /// 该向量以0,0为起点
        /// </summary>
        /// <param name="length">向量的模</param>
        /// <returns>向量终点坐标</returns>
        static public Point RandomVector(double length)
        {
            double r = RandomDouble(0, 2*Math.PI);
            return new Point(length * Math.Cos(r), length * Math.Sin(r));
        }

        #endregion
    }
}
