using System.Collections.Generic;
using data;
using Editor.Tool;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using so;
using UnityEditor;
using UnityEngine;

namespace Editor.Reader
{
    public class FxEditorTool
    {

        [AssetsOnly]
        [AssetList(Path = "Resources/SO/Fx",AutoPopulate = true )]
        public List<FxSoData> datas;


        private void OpenFolder(string path)
        {
            Folder_Editor_Tool.OpenFolder(path);
        }

        private static void RefreshFx(FxSoData data)
        {
            string newControllPath = Fx_Path.FxAnimControllerFolder + "/" + data.Name + ".overrideController";
            string clipPath = Fx_Path.FxAnimClipFolder + "/" + data.Name + ".anim";
            AnimatorOverrideController animatorOverride =
                AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(newControllPath);
            data.AnimatorController = animatorOverride;
            
            EditorUtility.SetDirty(data);

        }

        [MenuItem("Assets/Check/CreateFx")]
        private static void Create()
        {
            CreateFx(Selection.activeObject as Texture2D);
        }
        
        private static void CreateFx(Texture2D texture2D)
        {
            string newControllPath = Fx_Path.FxAnimControllerFolder + "/" + texture2D.name + ".overrideController";
        
            AssetDatabase.CopyAsset(Fx_Path.defaultFxAnim, newControllPath);
            AnimatorOverrideController animatorOverride =
                AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(newControllPath);

            AnimationClip clip = Anim_Editor_Tool.SpriteClip(texture2D, Fx_Path.FxAnimClipFolder);
            // Anim_Editor_Tool.AddFirstBlank(clip);
            Anim_Editor_Tool.ChangeStateAnimClip(animatorOverride, "Play", clip);
            
            
            FxSoData data = Editor_Tool_ScriptObject.GetData<FxSoData>($"{Fx_Path.SoPath}/{texture2D.name}");
            data.Name = texture2D.name;
            data.AnimatorController = animatorOverride;
            EditorUtility.SetDirty(data);
        
        }
        
        [MenuItem("Assets/Check/RemoveFirstBlankFx")]
        private static void Remove()
        {
            List<AnimationClip> allClips = Loader_Editor_Tool.GetAllFormSelectFolder_Asset<AnimationClip>();
            foreach (var clip in allClips)
            {
                Anim_Editor_Tool.RemoveFirstBlank(clip);
            }
        }
    }

    public static class Editor_Tool_ScriptObject
    {
        public static T GetData<T>(string path) where T: ScriptableObject
        {
            path = $"{path}.asset";
            T data = AssetDatabase.LoadAssetAtPath<T>(path);
            if(data != null)
                return data;
            data = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(data,path);
            return data;
        }
        
    }
    
}
