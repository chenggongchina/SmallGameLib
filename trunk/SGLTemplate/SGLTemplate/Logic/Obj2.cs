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
    //随机移动的椭圆
    public class Obj2 : BaseObj
    {
        public Obj2()
        {
            BoundVisiable = false;
            Ellipse e = new Ellipse();
            e.Width = 10;
            e.Height = 10;
            e.Fill = new SolidColorBrush(Colors.Black);
            e.Stroke = new SolidColorBrush(Colors.Blue);
            e.Opacity = 0.5;
            this.Children.Add(e);

            LogicCircle = 2;

            Point startPos = MathTools.RandomWindowPoint();
            X = startPos.X;
            Y = startPos.Y;
        }

        public override void logic()
        {
            Point x = MathTools.RandomVector( 1 );
            X += x.X;
            Y += x.Y;
            if (this.IsOutOfScreen())
                dead = true;
        }

        protected override Rect getMyBounderRect()
        {
            return new Rect(0, 0, 1, 1);
        }
    }
}
