using data;
using UnityEngine;

namespace so
{
    public abstract  class SingletonSo<T> : ScriptableObject where T :ScriptableObject
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = ScriptObjectTool.GetData<T>($"So/GameSetting/{typeof(T).Name}");
                }

                return instance;
            }
        }
    }
    
  
}
