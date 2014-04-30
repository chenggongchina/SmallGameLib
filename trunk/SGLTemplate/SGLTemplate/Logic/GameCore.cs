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
    public class GameCore : BaseCore
    {
        protected override void TickGameFrameLoop(object sender, EventArgs e)
        {
            if(gameStatus == GameStatus.Get("播放"))
            {
                layersLogic();
            }
            else if (gameStatus == GameStatus.Get("停止"))
            {
                LoopStop();
            }
        }
    }
}
