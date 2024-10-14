using System;
using UnityEngine;

namespace ui
{
    public class SingletonPanel<T> : StaticPanel where T : StaticPanel
    {
        protected SingletonPanel(Transform trans) : base(trans)
        {
            
        }

        public static T Instance { get; private set; }

        protected static T Create(GameObject obj)
        {
            GameObject go = GameObject.Instantiate(obj);
            // GameObject go = GameObject.Instantiate(obj,LayerPanel.Panel_Hide);
            go.name = typeof(T).Name;
            Instance =  (T)Activator.CreateInstance(typeof(T),new object[]{go.transform});
            return Instance;
        }
        
        public override void DestroyPanel()
        {
            Instance = null;
            base.DestroyPanel();
        }
    }
    
}
