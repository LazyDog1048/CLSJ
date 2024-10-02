using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Tool
{
    public static class Loader_Editor_Tool
    {
        public static GameObject LoadGameObject(string path)
        {
            GameObject parentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return GameObject.Instantiate(parentPrefab);
            
        }
        
        public static T LoadAssetAtPath<T>(string path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }
        
        public static List<T> GetAllFormSelectFolder_Asset<T>() where T : Object
        {
            var select = Selection.activeObject;
            var selectPath = AssetDatabase.GetAssetPath(select);
            return GetAllForm_Asset<T>(selectPath);
        }
        
         public static List<T> GetAllForm_Asset<T>(string directory) where T : Object
         {
             return GetAllForm_Asset<T>(directory, typeof(T).Name);
         }

        private static List<T> GetAllForm_Asset<T>(string directory,string filterType) where T : Object
        {
            if (string.IsNullOrEmpty(directory) || !directory.StartsWith("Assets"))
                throw new ArgumentException($"No Find {directory}");
     
            List<T> allResources = new List<T>();
            List<string> subFolders = new List<string>(Directory.GetDirectories(directory));
            subFolders.Add(directory);
            
            foreach (var folder in subFolders)
            {
                string[] guids = AssetDatabase.FindAssets($"t:{filterType}", new string[] {folder});

                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    T test = AssetDatabase.LoadAssetAtPath<T>(path);
                    if(test == null)
                        continue;
                    allResources.Add(test);
                    // Debug.Log($"{path} {test.name}");
                }
            }
            return allResources;
        }

        public static List<string> GetAllAssetDataPath<T>(string directory) where T : Object
        {

            Type type = typeof(T);
            if (string.IsNullOrEmpty(directory) || !directory.StartsWith("Assets"))
                throw new ArgumentException($"No Find {directory}");
     
            List<string> allResources = new List<string>();
            List<string> subFolders = new List<string>(Directory.GetDirectories(directory));
            subFolders.Add(directory);

            foreach (var folder in subFolders)
            {
                string[] guids = AssetDatabase.FindAssets($"t:{type.Name}", new string[] {folder});

                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    // Debug.Log(path);
                    allResources.Add(path);
                }
            }
            return allResources;
        }
        
        public static List<string> GetAllResourcePath(string directory,string filter)
        {
            if (string.IsNullOrEmpty(directory) || !directory.StartsWith("Assets"))
                throw new ArgumentException("folderPath");
     
            List<string> allObjPath = new List<string>();
            string[] subFolders = Directory.GetDirectories(directory);

            string[] thisFolder = AssetDatabase.FindAssets(filter, new string[] {directory});
            foreach (var file in thisFolder)
            {
                allObjPath.Add(AssetDatabase.GUIDToAssetPath(file));
            }
            
            foreach (var folder in subFolders)
            {
                string[] guids = AssetDatabase.FindAssets(filter, new string[] {folder});
                
                for (int i = 0; i < guids.Length; ++i)
                {
                    allObjPath.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
                }
            }
            return allObjPath;
        }
        
        public static List<T> GetAllResources<T>(string directory,string filter) where T : Object
        {
            List<string> allPath = GetAllResourcePath(directory, filter);
                
            List<T> allObj = new List<T>();
            
            for (int i = 0; i < allPath.Count; ++i)
            {
                allObj.Add( AssetDatabase.LoadAssetAtPath<T>(allPath[i]));
                
            }
            return allObj;
        }
        
        public static List<T> GetAllResources<T>(string directory) where T : Object
        {
            return GetAllResources<T>(directory, $"t:{typeof(T).Name}");
        }
        
        
        public static Dictionary<string,T> GetAllObject<T>(string path) where T : Object
        {
            var overrideList =
                GetAllResources<T>(path,
                    $"t:{typeof(T).Name}");

            var animDic = new Dictionary<string, T>();

            foreach (var controller in overrideList)
            {
                if (!animDic.ContainsKey(controller.name))
                    animDic.Add(controller.name, controller);
            }
            
            return animDic;
        }
        
        public static Dictionary<string,T> GetAllObject<T>(string path,string filter) where T : Object
        {
            var overrideList =
                GetAllResources<T>(path,
                    $"t:{typeof(T)}");

            var animDic = new Dictionary<string, T>();

            foreach (var controller in overrideList)
            {
                if (!animDic.ContainsKey(controller.name))
                    animDic.Add(controller.name, controller);
            }
            
            return animDic;
        }
    }
}