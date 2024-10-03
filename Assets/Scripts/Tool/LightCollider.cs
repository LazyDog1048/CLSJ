using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using game.Other;
using UnityEngine;

namespace game
{
    public class LightCollider : MonoBehaviour
    {
        [SerializeField] private LightColliderData lightColliderData;
        private SpriteRenderer[] _spriteRenderers;
        private Collider2D _collider2D;

        private bool isTransparency;
        
        private float lightSpeed;
        private float _isLight;
        private Color Color;
        private float isLight
        {
            get => _isLight;
            set => _isLight = value;
        }
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            Color= Color.white;
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            if (lightColliderData.isBlockLight)
                gameObject.layer = LayerMask.NameToLayer("LightBlock");
            else
                gameObject.layer = LayerMask.NameToLayer("Default");

            if (lightColliderData.isTransparency)
            {

            }
        }


        public void Update()
        {
            if (!lightColliderData.isTransparency)
                return;
            Color.a = isLight;
            if (isLight is >= 0 and <= 1)
            {
                foreach (var spriteRenderer in _spriteRenderers)
                {
                    spriteRenderer.color = Color;
                }
            }
            isLight-= lightSpeed * Time.deltaTime;
        }
        public void LightEnter()
        {
            if (!lightColliderData.isTransparency)
                return;
            isLight = isLight + lightSpeed * Time.deltaTime; 
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.DOFade(0, StaticValue.TransparencyTime);
            }
        }

        public void LightExit()
        {
            if (!lightColliderData.isTransparency)
                return;

            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.DOFade(1, StaticValue.TransparencyTime);
            }
        }
    }
}
