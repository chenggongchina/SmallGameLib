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
    public class Layer2 : BaseLayer
    {
        public Layer2()
        {
            for(int i=0;i<20;i++)
                AddObj(new Obj2());
        }
    }
}
