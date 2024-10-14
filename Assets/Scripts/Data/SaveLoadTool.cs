using System.IO;
using data;
using UnityEngine;

namespace tool
{
    public static class SaveLoadTool
    {
        public static void SaveToLocal<T>(T data,int index)
        {
            string fileName = typeof(T).Name;
            if(!Directory.Exists(DataManager.GetLoadPath(index)))
            {
                Directory.CreateDirectory(DataManager.GetLoadPath(index));
            }
            string path = $"{DataManager.GetLoadPath(index)}/{fileName}.json";
            // Debug.Log($"SaveToLocal {File.Exists(path)} {path}");
            if(!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            string str = JsonUtility.ToJson(data);
            File.WriteAllText(path, str);
        }


        public static T LoadFromLocal<T>(int index) where T: new()
        {
            string fileName = typeof(T).Name;
            string path = $"{DataManager.GetLoadPath(index)}/{fileName}.json";
            if(!File.Exists(path))
            {
                var data = new T();
                SaveToLocal(data,index);
            }

            string readData;
            //写入数据
            using (StreamReader file = File.OpenText(path))
            {
                readData = file.ReadToEnd();
                file.Close();
            }
            return JsonUtility.FromJson<T>(readData);
        }

        public static void DeleteFromLocal<T>(int index)
        {
            string fileName = typeof(T).Name;
            string path = $"{DataManager.GetLoadPath(index)}/{fileName}.json";
                
            if(!File.Exists(path))
            {
                Debug.Log($"File  {path}  not exist");
            }
            else
            {
                Debug.Log($"Delete {path}   Success");
                File.Delete(path);
            }
        }
 
    }
}

