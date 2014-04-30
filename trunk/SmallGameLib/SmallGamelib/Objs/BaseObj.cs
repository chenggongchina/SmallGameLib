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
    /// 物体基类
    /// </summary>
    public abstract class BaseObj : LogicReciever
	{
        public BaseObj()
        {
            this.Children.Add(image);
            
            //生成包围框
            this.Children.Add(boundRectangle);
            Rect rect = getBounderRect();
            Canvas.SetLeft(boundRectangle, rect.X);
            Canvas.SetTop(boundRectangle, rect.Y);
            boundRectangle.Width = rect.Width;
            boundRectangle.Height = rect.Height;
            Canvas.SetZIndex(boundRectangle, 1000); //包围框位于最上方
        }

        /// <summary>
        /// 获取自身所属层
        /// </summary>
        public BaseLayer getLayer() { return MyParent; }

        internal void setLayer(BaseLayer layer) { MyParent = layer; }
        private BaseLayer MyParent = null;

        private Image image = new Image();

        public void setImage(string picName)
        {
            this.image.Source = ImageTools.GetImage(picName);
        }

        /// <summary>
        /// 包围框
        /// </summary>
        private Rectangle boundRectangle = new Rectangle()
        {
            Stroke = new SolidColorBrush(Config.getInstance().BounderColor)
        };

        /// <summary>
        /// 包围框是否可见
        /// </summary>
        public Boolean BoundVisiable
        {
            set
            {
                if (value == true)
                    boundRectangle.Visibility = Visibility.Visible;
                else
                    boundRectangle.Visibility = Visibility.Collapsed;
            }
            get
            {
                return (boundRectangle.Visibility == Visibility.Visible);
            }
        }

        public double W
        {
            get;
            set;
        }

        public double H
        {
            get;
            set;
        }

        public double X
        {
            set { Canvas.SetLeft(this, value); }
            get { return Canvas.GetLeft(this); }
        }

        public double Y
        {
            set { Canvas.SetTop(this, value); }
            get { return Canvas.GetTop(this); }
        }

        /// <summary>
        /// 获取包围框绝对位置
        /// </summary>
        public Rect getBounderRect()
        {
            Rect rect = getMyBounderRect();
            rect.X += X;
            rect.Y += Y;
            return rect;
        }

        /// <summary>
        /// 获取相对定位的包围框
        /// 重载此函数用于设定物体的包围框相对自身图片的位置
        /// </summary>
        /// <returns></returns>
        protected virtual Rect getMyBounderRect() { return new Rect(0, 0, 0, 0); }

        /// <summary>
        /// 逻辑函数
        /// </summary>
        public override void logic() { }

        /// <summary>
        /// 角色是否死亡
        /// 设置成死亡，将被清除
        /// </summary>
        public Boolean dead = false;

        /// <summary>
        /// 判断物体是否在屏幕外
        /// </summary>
        /// <returns></returns>
        public bool IsOutOfScreen()
        {
            return (X + W < 0 || X > Config.getInstance().ScreenW || Y + H < 0 || Y > Config.getInstance().ScreenH);
        }


	}
}
