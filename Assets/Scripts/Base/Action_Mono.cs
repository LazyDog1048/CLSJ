using data;
using game.manager;
using UnityEngine;

namespace other
{
    public abstract class Action_Mono :MonoBehaviour
    {
        protected virtual void Awake()
        {
            if(Application.isPlaying)
                Init();
        }

        private void OnDestroy()
        {
            if(!Application.isPlaying)
                return;
            OnMonoDestroy();
        }
        
        protected virtual void Init()
        {
            
        }

        protected virtual void OnMonoDestroy()
        {
            
        }

        public virtual void LevelPrepare()
        {
        }


        public virtual void BeforeSceneChange()
        {
            
        }
        
        public virtual void AfterSceneChange()
        {
            
        }
    }
}





