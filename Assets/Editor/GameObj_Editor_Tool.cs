using System.Collections.Generic;
using game;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
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
            // List<string> lightBock = new List<string>
            // {
            //     "HChair", "Hdesk", "Hdesk2", "Hdesk3", "Hdesk4", "VChair", "VDesk1"
            // };
            string[] lightBock= {"HChair","Hdesk","Hdesk2","Hdesk3","Hdesk4","VChair","VDesk1"};
            // string str = "HChair,Hdesk,Hdesk2,Hdesk3,Hdesk4,VChair,VDesk1";
            
            SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
            GameObject lightColliderObj = new GameObject("LightCollider");
            GameObject colliderObj = new GameObject("Collider");
            GameObject tile = new GameObject("TileMap");
            GameObject def = new GameObject("Default");
            
            lightColliderObj.transform.SetParent(obj.transform);
            tile.transform.SetParent(obj.transform);
            def.transform.SetParent(obj.transform);
            colliderObj.transform.SetParent(obj.transform);
            //
            // GameObject lightColliderObj = obj.transform.Find("LightCollider").gameObject;
            // GameObject colliderObj = obj.transform.Find("Collider").gameObject;
            // GameObject tile = obj.transform.Find("TileMap").gameObject;
            // GameObject def = obj.transform.Find("Default").gameObject;
            //
            
            lightColliderObj.transform.localPosition = Vector3.zero;
            colliderObj.transform.localPosition = Vector3.zero;
            tile.transform.localPosition = Vector3.zero;
            def.transform.localPosition = Vector3.zero;


            // colliderObj.AddComponent<CompositeCollider2D>();
            // var rg =colliderObj.AddComponent<Rigidbody2D>();
            // rg.bodyType = RigidbodyType2D.Static;
            
            foreach (var renderer in renderers)
            {
                Vector2 pivot = renderer.sprite.pivot;
                // Vector2 worldSize = renderer.sprite.rect.size / 16;
                // renderer.gameObject.transform.position -= new Vector3(0,worldSize.y / 2,0); 
                // var boxCollider2D = renderer.gameObject.AddComponent<BoxCollider2D>();
                if (pivot.y == 0)
                {
                    bool isLight = false;
                    foreach (var name in lightBock)
                    {
                        Vector2 worldSize = renderer.sprite.rect.size / 16;
                        renderer.gameObject.transform.position -= new Vector3(0,worldSize.y / 2,0);
                        if(renderer.sprite.name.Contains(name))
                        {
                            renderer.transform.SetParent(lightColliderObj.transform);
                            ShadowCaster2D s = renderer.gameObject.AddComponent<ShadowCaster2D>();
                            isLight = true;
                            break;
                        }
                        
                    }
                    if(!isLight)  
                        renderer.transform.SetParent(colliderObj.transform);
                    
                }
                else if(renderer.sortingLayerName == "TileMap")
                {
                    renderer.transform.SetParent(tile.transform);
                }
                else if(renderer.sortingLayerName == "Default")
                {
                    renderer.transform.SetParent(def.transform);
                }
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
