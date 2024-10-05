using System;
using System.Collections.Generic;
using data;
using other;
using so;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//inputMagr
//input类
//事件中心
//公共mono
namespace game.manager
{
    public class InputManager : Class_Singleton<InputManager>
    {

        private Dictionary<string, ActionThing> keyDic;

        public InputActionAsset inputActionAsset { get; set; }
        
        public const string KEYBOARD = "Keyboard";
        public const string GAMEPAD = "Gamepad";

        public UnityEvent ControlSchemeChanged;
        
        #region InitPos
        protected override void Init()
        {
            base.Init();
            InitPlayerInput();
            keyDic = new Dictionary<string, ActionThing>();
            ControlSchemeChanged = new UnityEvent();

        }

        private void InitPlayerInput()
        {
            inputActionAsset = GetActionAsset();
        }
        
        public InputActionAsset GetActionAsset()
        {
            return Loader.ResourceLoad<InputActionAsset>("So/InputActions");
        }

        #endregion

        #region GetKeyThing
        public static ActionThing GetKeyThing(string key)
        {
            if(!Instance.keyDic.ContainsKey(key))
                throw new NullReferenceException($"未找到该 keyCode {key}");
            return Instance.keyDic[key];
        }

        public ActionThing AddAction(string Name, InputAction action)
        {
            if(!keyDic.ContainsKey(Name))
                keyDic.Add(Name,new ActionThing(action));
            return keyDic[Name];
        }

        #endregion
    
        private void RegisterAction()
        {
            // _inputKey = new InputKey();
            // _inputKey.RegisterAction();
        }
    }
}
