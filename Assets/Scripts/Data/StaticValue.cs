using System.Collections.Generic;
using data;
using UnityEngine;

namespace game.Other
{
    public static class StaticValue
    {
        public static int StameGameID = 2064610; // 用你的游戏的 App ID 替代这里的值
     
        public static Vector2 InScreenSize = new Vector2(60, 32);
        public const float StartUiShowTime = 0.5f;
        public const float BtnAnimTime = 0.2f;
        public const int pixelsPerUnit = 40;
        public const float AnimInterval = 0.07f;
        public const float TransparencyTime = 0.5f;
        
        private static bool isDevelop = Debug.isDebugBuild;
        private static bool isOnEditor = Application.isEditor;
        
        public static int Layer_Path = LayerMask.GetMask("Path");
        
        public static int Layer_Click = LayerMask.GetMask("Click");

        public static int Layer_Damageable = LayerMask.GetMask("Damageable");
        
        public static int Layer_SceneInteractive = LayerMask.GetMask("SceneInteractive");
        
        public static string Tag_Enemy = "Enemy";
        
        //EnemyDisWithTarget
        public static float EnemyDisWithTarget = 0.81f;
        
        
        public static Vector2 BtnEnterSize = new Vector2(1.2f,1.2f);
        public static Vector2 BtnExitSize = new Vector2(1f,1f);

        public static WaitForSecondsRealtime WaitRealtime002 = new WaitForSecondsRealtime(0.02f);
        
        public static WaitForEndOfFrame WaitEndFrame = new WaitForEndOfFrame();
        
        public static WaitForFixedUpdate WaitFixedUpdate = new WaitForFixedUpdate();

        public static float FixedTimeScale => Time.fixedDeltaTime;
        
        public static string LocalDataKey0 = "UserData0";
        public static string LocalDataKey1 = "UserData1";
        public static string LocalDataKey2 = "UserData2";
        public static string[] LocalDataKey = new string[3]{LocalDataKey0,LocalDataKey1,LocalDataKey2};
        public static string ResourcesPath = "Assets/Resources/";
    }
}