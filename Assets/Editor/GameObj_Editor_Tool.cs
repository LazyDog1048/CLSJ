using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Editor.Tool
{
    public static class GameObj_Editor_Tool
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
