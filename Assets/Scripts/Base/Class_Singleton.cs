namespace other
{
    public abstract class Class_Singleton<T> : Action_Class where T :new()
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
    }
}
