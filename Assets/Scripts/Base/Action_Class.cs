
namespace other
{
    public abstract class Action_Class
    {
        protected Action_Class()
        {
            OnCreate();
        }

        private void OnCreate()
        {
            Init();
        }
        protected virtual void Init()
        {
            
        }

        public virtual void GameQuite()
        {
            
        }

        public virtual void BeforeSceneChange()
        {
       
        }

        public virtual void AfterSceneChange()
        {
       
        }

        public virtual void LevelPrepare()
        {
        }
        
    }
    
}
