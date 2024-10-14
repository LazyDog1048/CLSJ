namespace other
{
    public abstract class KeepMonoSingleton<T> : Mono_Singleton<T> where T : Mono_Singleton<T>
    {
        protected override void Init()
        {
            if (Instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            } 
            // gameObject.transform.SetParent(ClassSingletonManager.Instance.DontDestoryParent);
            base.Init();
            KeepInit();
        }

        protected virtual void KeepInit()
        {
            
        }
        
        
    }
}

