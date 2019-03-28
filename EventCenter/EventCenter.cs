using ETModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class EventCenterAwake : AwakeSystem<EventCenter>
    {
        public override void Awake(EventCenter self)
        {
            self.Awake();
        }
    }

    public class EventCenter:Component
    {
        public static EventCenter Instance;

        private  Dictionary<string, Delegate> m_EventTable = new Dictionary<string, Delegate>();

        public void Awake()
        {
            Instance = this;
        }
        private  void OnListenerAdding(string eventType, Delegate callBack)
        {
            if (!m_EventTable.ContainsKey(eventType))
            {
                m_EventTable.Add(eventType, null);
            }
            Delegate d = m_EventTable[eventType];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        private  void OnListenerRemoving(string eventType, Delegate callBack)
        {
            if (m_EventTable.ContainsKey(eventType))
            {
                Delegate d = m_EventTable[eventType];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
            }
        }
        private  void OnListenerRemoved(string eventType)
        {
            if (m_EventTable[eventType] == null)
            {
                m_EventTable.Remove(eventType);
            }
        }
        //no parameters
        public   void AddListener(string eventType, Action callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action)m_EventTable[eventType] + callBack;
        }
        //Single parameters
        public  void AddListener<T>(string eventType, Action<T> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T>)m_EventTable[eventType] + callBack;
        }
        //two parameters
        public  void AddListener<T, X>(string eventType, Action<T, X> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X>)m_EventTable[eventType] + callBack;
        }
        //three parameters
        public  void AddListener<T, X, Y>(string eventType, Action<T, X, Y> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y>)m_EventTable[eventType] + callBack;
        }
        //four parameters
        public  void AddListener<T, X, Y, Z>(string eventType, Action<T, X, Y, Z> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z>)m_EventTable[eventType] + callBack;
        }
        //five parameters
        public  void AddListener<T, X, Y, Z, W>(string eventType, Action<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z, W>)m_EventTable[eventType] + callBack;
        }

        //no parameters
        public  void RemoveListener(string eventType, Action callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //single parameters
        public  void RemoveListener<T>(string eventType, Action<T> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //two parameters
        public  void RemoveListener<T, X>(string eventType, Action<T, X> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //three parameters
        public  void RemoveListener<T, X, Y>(string eventType, Action<T, X, Y> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //four parameters
        public  void RemoveListener<T, X, Y, Z>(string eventType, Action<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //five parameters
        public  void RemoveListener<T, X, Y, Z, W>(string eventType, Action<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z, W>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }


        //no parameters
        public  void Broadcast(string eventType)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action callBack = d as Action;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //single parameters
        public  void Broadcast<T>(string eventType, T arg)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T> callBack = d as Action<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //two parameters
        public  void Broadcast<T, X>(string eventType, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X> callBack = d as Action<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //three parameters
        public  void Broadcast<T, X, Y>(string eventType, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X, Y> callBack = d as Action<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //four parameters
        public  void Broadcast<T, X, Y, Z>(string eventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X, Y, Z> callBack = d as Action<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //five parameters
        public  void Broadcast<T, X, Y, Z, W>(string eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X, Y, Z, W> callBack = d as Action<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
    }
}