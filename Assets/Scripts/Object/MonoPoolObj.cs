using GridSystem;
using UnityEngine;

namespace game
{
    public abstract class MonoPoolObj : MonoBehaviour,IPoolObj
    {
        private void Awake()
        {
            OnAwake();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        protected virtual void OnAwake()
        {
            
        }

        #region IMonoPoolObj
        public virtual string poolId => GetType().Name;

        public bool isInPool { get; set; }

        public GameObject GetObj()
        {
            return gameObject;
        }
        public void ReleaseObj()
        {
            PoolManager.Instance.PushObj(poolId,this);
        }

        public virtual void OnPopObj()
        {
            
        }
        public virtual void OnPushObj()
        {
            
        }

        public void DestroyObj()
        {
            Destroy(gameObject);
        }
        #endregion
    }
    
}