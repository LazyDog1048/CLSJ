using System;
using System.Collections.Generic;
using game;
using game.manager;
using UnityEngine;

namespace other
{
    public class ObjPool : IDisposable
    {
        public static readonly Vector2 poolPos = new Vector2(10000, 10000);
        private readonly Stack<IPoolObj> m_Stack;
        // private readonly Func<T> m_CreateFunc;
        private readonly int m_MaxSize;
        private readonly string loadPath;
        private readonly string Name;
        public int CountAll { get; private set; }

        public int CountActive => CountAll - CountInactive;
        //池内已有数量
        public int CountInactive => m_Stack.Count;
        private readonly GameObject parentObj;
        
        public ObjPool(Transform parent,string poolName,string path,
            int defaultCapacity = 1,
            int maxSize = 100)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max Size must be greater than 0", nameof (maxSize));
            loadPath = path;
            m_Stack = new Stack<IPoolObj>(defaultCapacity);
            m_MaxSize = maxSize;
            Name = poolName;
            parentObj = new GameObject($"Pool_{Name}");
            parentObj.transform.SetParent(parent);
        }
        
        public GameObject Get()
        {
            IPoolObj iPoolObj;
            if (m_Stack.Count == 0)
            {
                iPoolObj = LoadObj(loadPath);
                iPoolObj.GetObj().name = $"{Name}_{iPoolObj.GetObj().GetHashCode()}";
                ++CountAll;
            }
            else
                iPoolObj = m_Stack.Pop();
            OnGet(iPoolObj);
            return iPoolObj.GetObj();
        }

        private IPoolObj LoadObj(string path)
        {
            GameObject obj= Loader.ResourceLoadGameObj<GameObject>(path);
            if(obj == null)
                throw  new NullReferenceException($"Can not Find in {path}");
            IPoolObj mono = obj.GetComponent<IPoolObj>();
            if(mono == null)
                throw  new NullReferenceException($"Can not Find {path} MonoPoolObj in resources");
            return mono;
        }
        
        protected virtual void OnGet(IPoolObj iPoolObj)
        {
            GameObject obj = iPoolObj.GetObj();
            obj.transform.SetParent(null);
            iPoolObj.GetObj().SetActive(true);
            iPoolObj.OnPopObj();
            iPoolObj.isInPool = false;
        }
        
        public void Release(IPoolObj element)
        {
            if (m_Stack.Count > 0 && m_Stack.Contains(element))
                return;
                // throw new InvalidOperationException($"Trying to release an {Name} that has already been released to the pool.");
            if (CountInactive < m_MaxSize)
            {
                OnRelease(element);
                m_Stack.Push(element);
            }
            //else destory
        }

        protected virtual void OnRelease(IPoolObj Obj)
        {
            Obj.GetObj().transform.SetParent(parentObj.transform);
            Obj.OnPushObj();
            Obj.GetObj().transform.position = poolPos;
            Obj.GetObj().SetActive(false);
            Obj.isInPool = true;
        }
        
        public void Clear()
        {
            foreach (IPoolObj obj in m_Stack)
                obj.DestroyObj();
                // GameObject.DestroyImmediate(obj.GetObj());
            m_Stack.Clear();
            CountAll = 0;
            GameObject.DestroyImmediate(parentObj); 
        }

        public void Dispose() => Clear();
    }
    
}
