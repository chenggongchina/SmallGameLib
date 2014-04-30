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

using GDE.SmallGameLib;
namespace SGLTemplate.Logic
{
    //在屏幕中央产生随机向四周扩展的点
    public class Obj1 : BaseObj
    {
        double dx = 0;
        double dy = 0;

        public Obj1()
        {
            BoundVisiable = true;
            X = Config.getInstance().ScreenW / 2;
            Y = Config.getInstance().ScreenH / 2;

            do
            {
                dx = MathTools.RandomDouble(-1.5, 1.5);
                dy = MathTools.RandomDouble(-1.5, 1.5);
            } while (dx == 0 && dy == 0);
        }

        public override void logic()
        {
            X += dx;
            Y += dy;
            this.Opacity -= 0.02; //alpha衰减
            if (this.IsOutOfScreen()||this.Opacity<=0)
                dead = true;
        }

        protected override Rect getMyBounderRect()
        {
            return new Rect(0, 0, 1, 1);
        }
    }
}
