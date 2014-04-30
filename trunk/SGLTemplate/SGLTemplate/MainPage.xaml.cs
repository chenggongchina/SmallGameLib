using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using GDE.SmallGameLib;
using SGLTemplate.Logic;

namespace SGLTemplate
{
    public partial class MainPage : UserControl
    {
        GameCore core = null;
        
        public MainPage()
        {
            InitializeComponent();
            
            core = new GameCore();
            core.Init(this, RootCanvas, GameStatus.Get("停止"));
            core.AddLayer(new Layer1(), 0, "主层");
            core.AddLayer(new Layer2(), 1, "上层");
            core.Begin();

            playstopbutton.Content = "播放";
            
        }

        private void onplaystop(object sender, System.Windows.RoutedEventArgs e)
        {
            if (core.getStatus() == GameStatus.Get("播放"))
            {
                core.Status(GameStatus.Get("停止"));
                playstopbutton.Content = "播放";
            }
            else
            {
                core.Status(GameStatus.Get("播放"));
                playstopbutton.Content = "停止";
            }
            
        }
    }
}
