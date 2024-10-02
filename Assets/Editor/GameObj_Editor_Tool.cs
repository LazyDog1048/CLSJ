using System.Collections.Generic;
using System.IO;
using data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Editor.Tool
{
    public static class GameObj_Editor_Tool
    {
        private static readonly int[] textureSize = {32,64,128,256,512,1024,2048,4096};
        
        [MenuItem("Assets/Check/TextureToSprite2D&Pixel_20")]
        public static void ChangeDirTextureToSprite2D_20()
        {
            ChangeDirTextureToSprite2D(20);
        }
        
        [MenuItem("Assets/Check/TextureToSprite2D&Pixel_40")]
        public static void ChangeDirTextureToSprite2D_40()
        {
            ChangeDirTextureToSprite2D(40);
        }

        public static void ChangeDirTextureToSprite2D(int perUnit)
        {
            var select = Selection.activeObject;
            var selectPath = AssetDatabase.GetAssetPath(select);
            List<string> allPath = FindNoneRefrences.GetAllResourcePath(selectPath, "t:Texture");

            foreach (var path in allPath)
            {
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                
                TextureImporterSettings importerSettings = new TextureImporterSettings();
                importerSettings.spriteMeshType = SpriteMeshType.FullRect;
                textureImporter.ReadTextureSettings(importerSettings);
                
                Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spritePixelsPerUnit = perUnit;
                textureImporter.filterMode = FilterMode.Point;
                
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed; 
                textureImporter.maxTextureSize = BestSize(texture);
                textureImporter.wrapMode = TextureWrapMode.Repeat;
                textureImporter.SaveAndReimport();
            }
        }

        public static int BestSize(Texture texture)
        {
            int bestSize = 2048;
            for (int i = 0; i < textureSize.Length; i++)
            {
                int biggestSize = textureSize[i];
                if (texture.height < biggestSize && texture.width < biggestSize)
                {
                    bestSize = biggestSize;
                    break;
                }
            }
            return bestSize;
        }
        
        public static GameObject SaveObjectToPrefab(GameObject saveObject,string path)
        {
            if (saveObject == null)
            {
                Debug.LogError("No object selected.");
                return null;
            }
            string prefabPath = path + ".prefab";
            // 判断文件是否存在，如果存在则删除
            if (File.Exists(prefabPath))
            {
                File.Delete(prefabPath);
            }
            // // 检查保存路径是否存在，如果不存在则创建
            // if (!AssetDatabase.IsValidFolder(prefabPath))
            // {
            //     AssetDatabase.CreateFolder("Assets", "Prefabs");
            // }
       
            Debug.Log($"path");
            // 将选定的物体保存为预制体
            
            return PrefabUtility.SaveAsPrefabAsset(saveObject, prefabPath);
        }
        
        public static string GetPrefabResourcesPath(GameObject gameObject)
        {
            string path = AssetDatabase.GetAssetPath(gameObject);
            path = path.Replace(Data_Path.ResourcesPath, "");
            path = path.Replace(".prefab", "");
            // Debug.Log($"path {path}");
            return path;
        }

      
        
    }

    public static class CheckObj
    {
        [MenuItem("Assets/Check/FindGameObjDo")]
        static void CleanupMissingScript()
        {
           ChangeSelectFolderPrefab(ComoponentThing);
        }
        
        public static void ChangeSelectFolderPrefab(UnityAction<GameObject> doChange)
        {
            // var objs = Folder_Editor_Tool.GetSelectFolderAllObjectsPath();
            // List<RuntimeAnimatorController> animatorControllers =
            //     Loader_Editor_Tool.GetAllForm_Asset<RuntimeAnimatorController>("Assets/Animater");
            var select = Selection.activeObject;
            var selectPath = AssetDatabase.GetAssetPath(select);
            var objs =  Loader_Editor_Tool.GetAllAssetDataPath<GameObject>(selectPath);

            foreach (var path in objs)
            {
                FindObj(path,doChange);
                // RenameObj<ScriptableObject>(path);
            }
        }
        
        private static void FindObj(string path,UnityAction<GameObject> doChange)
        {
            GameObject parentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObject parent = GameObject.Instantiate(parentPrefab);
            parent.name = parentPrefab.name;
            doChange(parent);
            EditorUtility.SetDirty(parent);
            PrefabUtility.SaveAsPrefabAsset(parent, path);
            GameObject.DestroyImmediate(parent);
        }
        
        private static void ComoponentThing(GameObject obj)
        {
            var TouchPlayer = obj.transform.Find("TouchPlayer");
            // EnemyData data = obj.GetComponent<baseene>();
            // uipoint.Find("HpBar").gameObject.SetActive(false);
            // Transform bone = uipoint.transform.Find("BoneHpBar");
            if (TouchPlayer != null)
            {
                BoxCollider2D box = TouchPlayer.GetComponent<BoxCollider2D>();
                box.offset = new Vector2(0,0);
            }
                
        }

        
        [MenuItem("Assets/Check/GetDependencies")]
        private static void GetDepend()
        {
            GameObject select = Selection.activeGameObject;
            foreach (var dependency in AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(select)))
            {
                Debug.Log(dependency);
            }
        }
        
        


        private static void SetImageSize(Vector2 scale,GameObject gameObject)
        {
            var image = gameObject.GetComponent<Image>();
            if (image == null)
                return;
            image.SetNativeSize();
            
            Vector3 size = image.rectTransform.sizeDelta;
            image.rectTransform.localScale = Vector3.one;
            image.rectTransform.sizeDelta = new Vector2(size.x * scale.x, size.y * scale.y);
        }
        
       
        [MenuItem("Assets/Check/TryRename")]
        public static void TryRename()
        {
            List<RuntimeAnimatorController> allAimater =
                Loader_Editor_Tool.GetAllFormSelectFolder_Asset<RuntimeAnimatorController>();
            Debug.Log($"Rename {allAimater.Count}");
            foreach (var animator in allAimater)
            {
                string path = AssetDatabase.GetAssetPath(animator);
                // string[] dirs = path.Split('/');
                Debug.Log(animator.name);
                string name = $"Enemy_{animator.name}.{path.Split('.')[^1]}";
                AssetDatabase.RenameAsset(path, name);
                // if (dirs[^3].Contains("_Tower"))
                // {
                //     Debug.Log(name);
                // }

            }
        }
   
    }
}
