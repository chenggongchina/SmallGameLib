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

namespace GDE.SmallGameLib
{
    /// <summary>
    /// 游戏状态类
    /// </summary>
    static public class GameStatus
    {
        static private List<string> status = new List<string>();

        static public void Add(string s)
        {
            status.Add(s);
        }

        /// <summary>
        /// 获取状态对应ID
        /// </summary>
        /// <param name="s">状态</param>
        /// <returns>该状态ID</returns>
        static public int Get(string s)
        {
            for (int i = 0; i < status.Count; i++)
            {
                if (status[i] == s)
                    return i;
            }
            throw new Exception("状态"+s+"不存在!");
        }
    }
}
