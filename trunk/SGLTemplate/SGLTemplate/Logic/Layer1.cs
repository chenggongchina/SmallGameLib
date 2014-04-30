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
    public class Layer1 : BaseLayer
    {
        public Layer1()
        {
        }

        public override void logic()
        {
            base.logic();
            //ObjectsAction(GetObjByName("2b"));
            if (MathTools.IsProbEventHappen(1))
                    AddObj(new Obj1());
        }
    }
}
