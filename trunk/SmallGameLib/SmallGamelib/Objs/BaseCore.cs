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

using System.Collections.Generic;
using System.Windows.Threading;

namespace GDE.SmallGameLib
{
    /// <summary>
    /// 游戏内核基类
    /// 
    /// 使用顺序
    /// 构造 -> Init -> AddLayer -> Begin
    /// </summary>
    public abstract class BaseCore
    {  
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mainPage">主页面</param>
        /// <param name="rootCanvas">根画布</param>
        /// <param name="startStatus">初始状态</param>
        public void Init(UserControl mainPage, Canvas rootCanvas, int startStatus)
        {
            gameStatus = startStatus;
            this.rootCanvas = rootCanvas;
            this.mainPage = mainPage;

            //设置剪裁区域
            rootCanvas.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, config.ScreenW, config.ScreenH) };

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(TickGameFrameLoop);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(config.GameLoopTime);
        }

        /// <summary>
        /// config类
        /// </summary>
        protected Config config = Config.getInstance();

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void Begin()
        {
            LoopStart();
        }

        /// <summary>
        /// 游戏状态
        /// </summary>
        protected int gameStatus { get; set; }
        
        /// <summary>
        /// 游戏帧定时器
        /// </summary>
        private DispatcherTimer dispatcherTimer;

        /// <summary>
        /// 主页面
        /// </summary>
        protected UserControl mainPage = null;

        /// <summary>
        /// 根画布
        /// </summary>
        protected Canvas rootCanvas = null;

        /// <summary>
        /// 层集合
        /// </summary>
        protected List<BaseLayer> layers = new List<BaseLayer>();
       
        /// <summary>
        /// 获取根画布
        /// </summary>
        /// <returns></returns>
        public Canvas getRootCanvas()
        {
            return rootCanvas;
        }

        /// <summary>
        /// 增加层
        /// </summary>
        /// <param name="layer">层对象</param>
        /// <param name="zindex">Z轴坐标</param>
        public void AddLayer( BaseLayer layer,int zindex )
        {
            layer.setZIndex(zindex);
            layers.Add(layer);
            rootCanvas.Children.Add(layer);
        }

        /// <summary>
        /// 增加层
        /// </summary>
        /// <param name="layer">层对象</param>
        /// <param name="zindex">Z轴坐标</param>
        /// <param name="name">层名</param>
        public void AddLayer(BaseLayer layer, int zindex, string name)
        {
            foreach (BaseLayer l in layers)
            {
                if (l.MyName == name)
                    throw new Exception("层名[" + name + "]已经存在,不能重复添加！");
            }
            layer.MyName = name;
            layer.setBaseCore(this);
            AddLayer(layer, zindex);
        }

        /// <summary>
        /// 根据层名获取层对象
        /// </summary>
        /// <param name="name">层名</param>
        /// <returns>层对象</returns>
        public BaseLayer GetLayerByName(string name)
        {
            foreach (BaseLayer l in layers)
            {
                if (l.MyName == name)
                    return l;
            }
            throw new Exception("层名[" + name + "]不存在!");
        }

        /// <summary>
        /// 开始逻辑循环
        /// </summary>
        public void LoopStart()
        {
            dispatcherTimer.Start();
        }

        /// <summary>
        /// 中止逻辑循环
        /// </summary>
        protected void LoopStop()
        {
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// 设置游戏状态，并驱动循环
        /// </summary>
        /// <param name="status">游戏状态</param>
        public void Status( int status )
        {
            gameStatus = status;
            LoopStart();
        }

        /// <summary>
        /// 获取游戏状态
        /// </summary>
        /// <returns>当前游戏状态</returns>
        public int getStatus()
        {
            return gameStatus;
        }

        /// <summary>
        /// 帧逻辑函数
        /// 
        /// 在实现中可以调用layersLogic()来向各层派发逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        abstract protected void TickGameFrameLoop(object sender, EventArgs e);

        /// <summary>
        /// 重置各层
        /// </summary>
        protected void Reset()
        {
            foreach (BaseLayer layer in layers)
            {
                layer.reset();
            }
        }

        /// <summary>
        /// 派发层逻辑
        /// </summary>
        protected void layersLogic()
        {
            foreach (BaseLayer layer in layers)
            {
                layer.checkLogicCircle();
            }
        }
    }
}
