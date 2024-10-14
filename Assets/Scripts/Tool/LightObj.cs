using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace game
{
    public class LightObj : MonoBehaviour
    {
        [SerializeField] 
        private LightColliderData lightColliderData;
        
        private SpriteRenderer[] _spriteRenderers;

        private float _isLight;
        private bool isLight;
        List<TweenerCore<Color,Color,ColorOptions>> doColorList;
        
        private void Awake()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            doColorList = new List<TweenerCore<Color, Color, ColorOptions>>();
            if (lightColliderData.isTransparency)
            {
                for(int i=0;i<_spriteRenderers.Length;i++)
                {
                    doColorList.Add(_spriteRenderers[i].DOFade(0,0));
                }
            }
            isLight = false;
        }


    
        public void LightEnter()
        {
            if (!lightColliderData.isTransparency || isLight)
                return;

            isLight = true;
            foreach (var doColor in doColorList)
            {
                doColor.Kill();
            }
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                doColorList[i] = _spriteRenderers[i].DOFade(1, StaticValue.TransparencyTime);
            }
        }

        public void LightExit()
        {
            if (!lightColliderData.isTransparency || !isLight)
                return;
            isLight = false;
            
            foreach (var doColor in doColorList)
            {
                doColor.Kill();
            }
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                doColorList[i] = _spriteRenderers[i].DOFade(0, StaticValue.TransparencyTime);
            }
            
        }
    }
}
