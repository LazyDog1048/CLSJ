using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.Tool
{
    public static class Texture2D_Editor_Tool
    {
        // using Object = UnityEngine.Object;
        public static List<Sprite> GetTextureSprites(Texture2D image)
        {
            string rootPath = Folder_Editor_Tool.AssetDir(AssetDatabase.GetAssetPath(image));//获取路径名称  
            string path = rootPath + "/" + image.name + ".PNG";//图片路径名称

            UnityEngine.Object[] objs = AssetDatabase.LoadAllAssetsAtPath(path);
            List<Sprite> sprites = new List<Sprite>();
            for (int i = 0; i < objs.Length; i++)
            {
                if(objs[i] is Sprite)
                    sprites.Add(objs[i] as Sprite);
            }
            // Debug.Log(sprites.Count);
            return sprites;
        }
    
        public static List<Sprite> GetSprites(string path)
        {
            UnityEngine.Object[] objs = AssetDatabase.LoadAllAssetsAtPath($"{path}.PNG");
            List<Sprite> sprites = new List<Sprite>();
            for (int i = 0; i < objs.Length; i++)
            {
                if(objs[i] is Sprite)
                    sprites.Add(objs[i] as Sprite);
            }
            // Debug.Log(sprites.Count);
            return sprites;
        }
        
         private static readonly int[] textureSize = {32,64,128,256,512,1024,2048,4096};
        
        [MenuItem("Assets/Check/TextureToSprite2D&Pixel_8")]
        public static void ChangeDirTextureToSprite2D_8()
        {
            ChangeDirTextureToSprite2D(8);
        }
        
        [MenuItem("Assets/Check/TextureToSprite2D&Pixel_16")]
        public static void ChangeDirTextureToSprite2D_16()
        {
            ChangeDirTextureToSprite2D(16);
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
                textureImporter.wrapMode = TextureWrapMode.Clamp;
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
    } 

}