using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Editor
{
    public class CheckNonoRefrencePath
    {
        public static string[] checkDir = {"Assets/RawResources","Assets/Resources"};
    }

    public class FindNoneRefrences
    {
        public static List<string> allPrefabAssetPaths= new List<string>();
        static List<string> noneRefrencePngList = new List<string>();
        static List<string> hadRefrencePngList = new List<string>();
        static public void Find()
        {
            allPrefabAssetPaths = new List<string>();
            noneRefrencePngList = new List<string>();
            hadRefrencePngList = new List<string>();
            
            for (int i = 0; i < CheckNonoRefrencePath.checkDir.Length; i++)
            {
                if(i == 0)
                    noneRefrencePngList.AddRange(GetAllResourcePath(CheckNonoRefrencePath.checkDir[i],"t:Texture"));
                allPrefabAssetPaths.AddRange(GetAllResourcePath(CheckNonoRefrencePath.checkDir[i],"t:Prefab"));
            }

            List<string> dependence = GetDependenceByType(allPrefabAssetPaths, ".png");
            for (int i = 0; i < dependence.Count; i++)
            {
                if (noneRefrencePngList.Contains(dependence[i]))
                {
                    noneRefrencePngList.Remove(dependence[i]);
                    hadRefrencePngList.Add(dependence[i]);
                }
            }
        }
        
        [MenuItem("Tools/Check/Move None Refrences To UnPackage(only texture)")]
        static private void MoveTexture()
        {
            if (noneRefrencePngList.Count < 1)
                Find();
            
            // MoveTextureToUnPackage(noneRefrencePngList,CheckNonoRefrencePath.textureNewPath);
        }
        
        [MenuItem("Assets/Check/Refrence (only texture)")]
        private static void GetNoneRefrenceTexture()
        {
            if (noneRefrencePngList.Count < 1)
                Find();
            
            var objList = Selection.objects;
            CheckIsRefrences(objList);
            
        }
        
        [MenuItem("Assets/Check/PngUsedByWitchPrefab (only texture)")]
        public static List<string> GetPngUsedBy()
        {
            if (allPrefabAssetPaths.Count < 1)
                Find();

            return GetUsedByPrefab(allPrefabAssetPaths,Selection.activeObject);

        }

        public static List<string> GetPngUsedByObj(Object obj)
        {
            if (allPrefabAssetPaths.Count < 1)
                Find();

            return GetUsedByPrefab(allPrefabAssetPaths, obj);

        }


        [MenuItem("Assets/Check/FindPngInGame")]
        private static void FindPngInGame()
        {
            var sprites = GameObject.FindObjectsOfType<Image>(true);
            var cur =  AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(Selection.activeObject));
            bool isFind = false;
            foreach (var item in sprites)
            {
                if(item.sprite == cur )
                {
                    Debug.Log("yns  " + item.name);
                    if(!isFind)
                        Selection.activeObject = item.gameObject;
                }
            }
        }
        [MenuItem("Assets/Check/FindPngInPrefab")]
        private static void FindPngInPrefab()
        {
            //var sprites = GameObject.FindObjectsOfType<Image>();
            //AssetDatabase.LoadAllAssetsAtPath()
            UnityEditor.SceneManagement.PrefabStage prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null)
                return;
            GameObject prefabRoot = prefabStage.prefabContentsRoot;
            if (prefabRoot == null)
                return;

            Debug.Log(prefabRoot);


            //var prefab = PrefabStageUtility.GetCurrentPrefabStage();
            var sprites = prefabRoot.GetComponentsInChildren<Image>(true);
            //Debug.Log("yns  prefab " + prefab.name);


            var cur = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(Selection.activeObject));
            bool isFind = false;
            foreach (var item in sprites)
            {
                //Debug.Log("yns  ite" + item.name);
                if (item.sprite == cur)
                {
                    Debug.Log("yns  " + item.name);
                    if (!isFind)
                        Selection.activeObject = item.gameObject;
                }
            }
        }
        
        [MenuItem("Assets/Check/MoveSpriteToCommon")]
        static void CheckCommonToMove()
        {
            if(hadRefrencePngList.Count<1)
                Find();

            for (int i = 0; i < hadRefrencePngList.Count; i++)
            {
                List<string> refrencePath = GetUsedByPrefab(allPrefabAssetPaths, hadRefrencePngList[i]);
                //move common refrence 3
                if(refrencePath.Count < 3)
                    continue;
                string prefabName = refrencePath[0].Split('/').Last();
                
                // MoveTextureToUnPackage(hadRefrencePngList[i], $"{CheckNonoRefrencePath.commonPath}/{prefabName}");
            }
        }

        static void MoveTextureToUnPackage(List<string> moveList,string newPath)
        {
            for (int i = 0; i < moveList.Count; i++)
            {
                MoveTextureToUnPackage(moveList[i], newPath);
            }
        }
        
        static void MoveTextureToUnPackage(string oldPath,string newPath)
        {
            string pngName = oldPath.Split('/').Last();
            Debug.Log(pngName);
            AssetDatabase.MoveAsset(oldPath, $"{newPath}/{pngName}");
        }
        
        //get all type thing in this directory
        public static List<string> GetAllResourcePath(string directory,string filter)
        {
            if (string.IsNullOrEmpty(directory) || !directory.StartsWith("Assets"))
                throw new ArgumentException("folderPath");
     
            List<string> allObjPath = new List<string>();
            string[] subFolders = Directory.GetDirectories(directory);
            // if (subFolders.Length == 0)
            //     subFolders = new string[1] {directory};

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
                FindNoneRefrences.GetAllResources<T>(path,
                    $"t:{typeof(T)}");

            var animDic = new Dictionary<string, T>();

            foreach (var controller in overrideList)
            {
                if (!animDic.ContainsKey(controller.name))
                    animDic.Add(controller.name, controller);
            }
            
            return animDic;
        }
        
        //get all type dependence of all prefab  
        static List<string> GetDependenceByType(List<string> prefabPAth,string type)
        {
            List<string> dependencePath = new List<string>();
            string[] allDependencies = AssetDatabase.GetDependencies(prefabPAth.ToArray(), true);
            for (int i = 0; i < allDependencies.Length; i++)
            {
                if (allDependencies[i].ToLower().EndsWith(type))
                {
                    dependencePath.Add(allDependencies[i]);
                }
            }
            return dependencePath;
        }
        
        
        static void CheckIsRefrences(Object[] selects)
        {
            List<Object> noneUse = new List<Object>();
            for (int i = 0; i < selects.Length; i++)
            {
               string path = AssetDatabase.GetAssetPath(selects[i]);
               if (noneRefrencePngList.Contains(path))
               {
                   noneUse.Add(selects[i]);
                   Debug.Log($"None Refrences {path}");
               }
            }
            if(noneUse.Count < 1)
                Debug.Log("all Texture had Refrence");
            Selection.objects = noneUse.ToArray();
        }
        
        //get resources dependence in this prefab path
        static List<string> GetUsedByPrefab(List<string> prefabPath,Object obj)
        {
            return GetUsedByPrefab(prefabPath,AssetDatabase.GetAssetPath(obj));
        }

        static List<string> GetUsedByPrefab(List<string> prefabPath,string path)
        {
            List<string> usedPrefabPath = new List<string>();
            for (int i = 0; i < prefabPath.Count; i++)
            {
                string[] allDependence = AssetDatabase.GetDependencies(prefabPath[i], true);
                for (int j = 0; j < allDependence.Length; j++)
                {
                    if (path == allDependence[j])
                    {
                        Debug.Log("prefabPath " + prefabPath[i]);
                        usedPrefabPath.Add(prefabPath[i]);
                        break;
                    }
                }
            }
            
            if (usedPrefabPath.Count < 1)
            {
                Debug.Log("No Used By Any Prefab");
            }
            
            return usedPrefabPath;
        }
        
    }
    

}
