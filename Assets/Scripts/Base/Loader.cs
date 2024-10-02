using UnityEngine;
using Object = UnityEngine.Object;

namespace game.manager
{
    public static class Loader
    {
        //同步加载资源   name 一个路径
        public static T ResourceLoad<T>(string path) where T : Object
        {
            //从路径中加载object
            T res = Resources.Load<T>(path);
            if (res == null)
            {
                Debug.LogError($"No Find Data {path}");
            }
            return res;
        }
        
        public static T ResourceLoadGameObj<T>(string path) where T : Object
        {
            //从路径中加载object
            T res = Resources.Load<T>(path);
            if (res == null)
            {
                Debug.LogError($"No Find Object {path}");
            }
            return GameObject.Instantiate(res);
        }
 
        public static T[] GetAllForm_Resource<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
        
        public static T ResourceLoadJson<T>() where T: new()
        {
            string fileName = typeof(T).Name;
            string path = $"Data/{fileName}";           
            var readData = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(readData.text);
        }
        
        public static T LoadDialigJson<T>(string path) where T: new()
        {
            // string fileName = typeof(T).Name;
            // string path = $"Data/{fileName}";       {
            
            var readData = Resources.Load<TextAsset>(path);
            string text = "{" + "\"dialogs\":" + readData.text + "}";
            return JsonUtility.FromJson<T>(text);
        }
    }
    

}
