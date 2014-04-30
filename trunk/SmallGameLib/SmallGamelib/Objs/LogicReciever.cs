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
    /// 逻辑接收者
    /// </summary>
    public abstract class LogicReciever : Canvas
    {
        public string MyName { get; set; }
        public void setZIndex(int index) 
        {
            Canvas.SetZIndex(this, index); 
        }

        protected int LogicCircle = 1;
        private int currLogicCircle = 0;

        /// <summary>
        /// 设置逻辑周期circle
        /// 若设置成0，则不向该物体派发逻辑
        /// 系统每 (circle*父亲节点周期) 个周期 向该物体派发逻辑
        /// </summary>
        /// <param name="circle">周期</param>
        public void setLogicCircle(int circle)
        {
            if (circle < 1)
                throw new Exception("逻辑周期不能小于1");
            LogicCircle = circle;
        }

        /// <summary>
        /// 检测是否向该物体派发逻辑
        /// </summary>
        internal void checkLogicCircle()
        {
            if (LogicCircle < 1)
                return;
            currLogicCircle++;
            if (currLogicCircle == LogicCircle)
            {
                currLogicCircle = 0;
                logic();
            }
        }

        /// <summary>
        /// 逻辑函数
        /// </summary>
        abstract public void logic();
    }
}
