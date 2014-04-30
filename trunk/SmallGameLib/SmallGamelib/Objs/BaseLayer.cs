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
    /// 层基类
    /// </summary>
    public abstract class BaseLayer : LogicReciever
    {
        /// <summary>
        /// 获取自身所属游戏引擎实例
        /// </summary>
        public BaseCore getBaseCore() { return MyParent; }

        internal void setBaseCore(BaseCore ge) { MyParent = ge; } 
        private BaseCore MyParent = null;

        /// <summary>
        /// 物体列表
        /// </summary>
        protected List<BaseObj> objects = new List<BaseObj>();

        /// <summary>
        /// 增加物体
        /// </summary>
        /// <param name="obj"></param>
        protected void AddObj(BaseObj obj) 
        {
            objects.Add(obj);
            obj.setLayer(this);
            this.Children.Add(obj);
        }

        /// <summary>
        /// 增加物体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name">物体名</param>
        protected void AddObj(BaseObj obj, string name)
        {
            obj.MyName = name;
            AddObj(obj);
        }

        /// <summary>
        /// 根据名字获取物体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>物体集合</returns>
        protected List<BaseObj> GetObjByName( string name )
        {
            List<BaseObj> result = new List<BaseObj>();
            foreach (BaseObj obj in objects)
            {
                if (obj.MyName == name)
                    result.Add(obj);
            }
            return result;
        }

        /// <summary>
        /// 清除物体
        /// </summary>
        /// <param name="obj"></param>
        protected void RemoveObj(BaseObj obj)
        {
            objects.Remove(obj);
            this.Children.Remove(obj);
        }

        /// <summary>
        /// 向所属所有物体派发逻辑
        /// </summary>
        protected void allObjectsAction()
        {
            foreach (BaseObj obj in objects)
            {
                obj.checkLogicCircle();
            }
        }

        /// <summary>
        /// 指定对象行动
        /// </summary>
        /// <param name="objs">对象集合</param>
        protected void ObjectsAction(List<BaseObj> objs)
        {
            foreach (BaseObj obj in objs)
            {
                obj.checkLogicCircle();
            }
        }

        /// <summary>
        /// 默认的逻辑函数
        /// </summary>
        public override void logic()
        {
            this.cleanDeadObjects();
            this.allObjectsAction();
        }

        /// <summary>
        /// 清空物体
        /// 重置游戏请重载本函数
        /// </summary>
        public virtual void reset()
        {
            this.Children.Clear();
            objects.Clear();
        }

        /// <summary>
        /// 清除屏幕外的物体
        /// </summary>
        protected void removeObjectsOutside()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].IsOutOfScreen() )
                {
                    RemoveObj(objects[i]);
                }
            }
        }

        /// <summary>
        /// 清除死亡的物体
        /// </summary>
        protected void cleanDeadObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if ( objects[i].dead )
                {
                    RemoveObj(objects[i]);
                }
            }
        }
    }
}
