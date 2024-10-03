using System;
using System.Collections.Generic;
using other;
using UnityEngine;

namespace game
{
    // public class PoolManager : Class_Singleton<PoolManager>
    public class PoolManager : Mono_Singleton<PoolManager>
    {
        private Dictionary<string, ObjPool> monoPoolDic;
        private GameObject pool =>transform.gameObject;

        protected override void Init()
        {
            base.Init();
            // pool = new GameObject("Pool");
            monoPoolDic = new Dictionary<string, ObjPool>();
            // pool.transform.SetParent(GameManager.Instance.GameStuffs);
        }

        protected override void OnMonoDestroy()
        {
            Clear();   
            base.OnMonoDestroy();
        }


        #region Pop

        public T PopObj<T>(string id,string path,int defaultCapacity = 1, int maxSize = 100)
        {
            return GetPool(id,path,defaultCapacity,maxSize).Get().GetComponent<T>();
        }
        
        public ObjPool GetPool(string id,string path,int defaultCapacity = 1, int maxSize = 100)
        {
            if (!monoPoolDic.ContainsKey(id))
            {
                monoPoolDic.Add(id,new ObjPool(pool.transform,id,path,defaultCapacity,maxSize));
            }

            return monoPoolDic[id];
        }

        #endregion

        //放入
        public void PushObj(string id,IPoolObj obj)
        {
            if (!monoPoolDic.ContainsKey(id))
            {
                throw  new NullReferenceException($"Can not Find {id} in PoolDic");
            }
            monoPoolDic[id].Release(obj);
        }

        //清空缓存池方法
        private void Clear()
        {
            foreach (var key_val in monoPoolDic)
            {
                key_val.Value.Clear();
            }
            monoPoolDic.Clear();
        }
    }
    
}