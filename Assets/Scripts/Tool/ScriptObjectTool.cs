using tool;
using UnityEngine;

namespace data
{
    
    public static class ScriptObjectTool
    {
        public static T GetData<T>(string path) where T: ScriptableObject
        {
            // path = $"{path}.asset";
            T data = Loader.ResourceLoad<T>(path);
            return data;
        }

        
    }
}
