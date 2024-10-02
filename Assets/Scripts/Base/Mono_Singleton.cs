namespace other
{
    public abstract class Mono_Singleton<T> : Action_Mono where T : Action_Mono
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = Mono_Extend.Create<T>();
                    // Debug.Log($"create {typeof(T).Name}");
                }
                return instance;
            }
        }

        protected override void Init()
        {
            instance = this as T;
            base.Init();
        }
    }
}
