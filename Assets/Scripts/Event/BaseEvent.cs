using System.Collections.Generic;
using UnityEngine.Events;

namespace game
{
    
    public interface IEventInfo
    {

    }

    //用于无参事件
    public class EventInfo : IEventInfo           //基类存子类 里氏转换原则
    {
        private UnityAction actions;
        private UnityAction onceActions;
        
        public void AddEvent(UnityAction action,bool once)
        {
            if (once)
                onceActions += action;
            else
                actions += action;
        }
        public void RemoveEvent(UnityAction action)
        {
            onceActions -= action;
            actions -= action;
        }

        public void Invoke()
        {
            actions?.Invoke();
            onceActions?.Invoke();
            onceActions = null;
        }

    }

    public class EventInfo<T> : IEventInfo           //基类存子类 里氏转换原则
    {
        private UnityAction<T> actions;
        private UnityAction<T> onceActions;
        
        public void AddEvent(UnityAction<T> action,bool once)
        {
            if (once)
                onceActions += action;
            else
                actions += action;
        }
        public void RemoveEvent(UnityAction<T> action)
        {
            onceActions -= action;
            actions -= action;
        }

        public void Invoke(T info)
        {
            actions?.Invoke(info);
            onceActions?.Invoke(info);
            onceActions = null;
        }
    }

    public class EventInfo<T0,T1> : IEventInfo           //基类存子类 里氏转换原则
     {
         private UnityAction<T0,T1> actions;
         private UnityAction<T0,T1> onceActions;
        
         public void AddEvent(UnityAction<T0,T1> action,bool once)
         {
             if (once)
                 onceActions += action;
             else
                 actions += action;
         }
         public void RemoveEvent(UnityAction<T0,T1> action)
         {
             onceActions -= action;
             actions -= action;
         }

         public void Invoke(T0 info0,T1 info1)
         {
             actions?.Invoke(info0,info1);
             onceActions?.Invoke(info0,info1);
             onceActions = null;
         }
     }

    public class EventInfo<T0,T1,T2> : IEventInfo           //基类存子类 里氏转换原则
    {
        private UnityAction<T0,T1,T2> actions;
        private UnityAction<T0,T1,T2> onceActions;
        
        public void AddEvent(UnityAction<T0,T1,T2> action,bool once)
        {
            if (once)
                onceActions += action;
            else
                actions += action;
        }
        public void RemoveEvent(UnityAction<T0,T1,T2> action)
        {
            onceActions -= action;
            actions -= action;
        }

        public void Invoke(T0 info0,T1 info1,T2 info2)
        {
            actions?.Invoke(info0,info1,info2);
            onceActions?.Invoke(info0,info1,info2);
            onceActions = null;
        }

    }

    //事件中心  单例模式
    //dictionary
    //委托
    //观察者设计模式
    //泛型
    public abstract class BaseEvent<T> where T : new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
        
        //UnityAction unity自带的委托
        //key 时间名字 value 监听事件的函数们
        protected Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

        #region EventInScene

        #region 添加一次性监听事件
       
        //无参重载
        public virtual void OnceListener(string name, UnityAction action)
        {
            eventDic.CheckAdd(name, new EventInfo());
            (eventDic[name] as EventInfo).AddEvent(action,true);
        }
      
        public virtual void OnceListener<T0>(string name, UnityAction<T0> action) 
        {
            eventDic.CheckAdd(name, new EventInfo<T0>());
            (eventDic[name] as EventInfo<T0>).AddEvent(action,true);
        }
        public virtual void OnceListener<T0,T1>(string name, UnityAction<T0,T1> action)
        {
            eventDic.CheckAdd(name, new EventInfo<T0,T1>());
            (eventDic[name] as EventInfo<T0,T1>).AddEvent(action,true);
        }
        
        public virtual void OnceListener<T0,T1,T2>(string name, UnityAction<T0,T1,T2> action)
        {
            eventDic.CheckAdd(name, new EventInfo<T0,T1,T2>());
            (eventDic[name] as EventInfo<T0,T1,T2>).AddEvent(action,true);
        }
        
        
        #endregion
        
        #region 添加监听事件
       
        //无参重载
        public virtual void AddListener(string name, UnityAction action,bool once = false)
        {
            eventDic.CheckAdd(name, new EventInfo());
            (eventDic[name] as EventInfo).AddEvent(action,once);
        }
      
        public virtual void AddListener<T0>(string name, UnityAction<T0> action,bool once = false)
        {
            eventDic.CheckAdd(name, new EventInfo<T0>());
            (eventDic[name] as EventInfo<T0>).AddEvent(action,once);
        }
        public virtual void AddListener<T0,T1>(string name, UnityAction<T0,T1> action,bool once = false)
        {
            eventDic.CheckAdd(name, new EventInfo<T0,T1>());
            (eventDic[name] as EventInfo<T0,T1>).AddEvent(action,once);
        }
        
        public virtual void AddListener<T0,T1,T2>(string name, UnityAction<T0,T1,T2> action,bool once = false)
        {
            eventDic.CheckAdd(name, new EventInfo<T0,T1,T2>());
            (eventDic[name] as EventInfo<T0,T1,T2>).AddEvent(action,once);
        }
        
        
        #endregion
        
        #region 移除对应事件监听

        public virtual  void RemoveListener(string name, UnityAction action)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo).RemoveEvent(action);
        }
        
        public virtual void RemoveListener<T0>(string name, UnityAction<T0> action)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo<T0>).RemoveEvent(action);
        }
        public virtual void RemoveListener<T0,T1>(string name, UnityAction<T0,T1> action)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo<T0,T1>).RemoveEvent(action);
        }
        public virtual void RemoveListener<T0,T1,T2>(string name, UnityAction<T0,T1,T2> action)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo<T0,T1,T2>).RemoveEvent(action);
        }
        #endregion

        #region 事件触发
        public virtual void EventTrigger(string name)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo).Invoke();
        }
        
        public virtual void EventTrigger<T0>(string name,T0 info)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo<T0>).Invoke(info);
        }

        public virtual void EventTrigger<T0,T1>(string name, T0 info0,T1 info1)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo<T0,T1>).Invoke(info0,info1);
        }
        
        public virtual void EventTrigger<T0,T1,T2>(string name, T0 info0,T1 info1,T2 info2)
        {
            if (!eventDic.ContainsKey(name))
                return;
            (eventDic[name] as EventInfo<T0,T1,T2>).Invoke(info0,info1,info2);
        }
        #endregion    

        #endregion
        

        //情况事件中心 主要在场景切换上
        public virtual void Clear()
        {
            eventDic.Clear();
        }
    }
}
