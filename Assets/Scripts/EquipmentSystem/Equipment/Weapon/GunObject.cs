using EquipmentSystem;
using game;
using GridSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class GunObject : GridGameObject,IAnimatorController
    {
        [SerializeField]
        private Light2D fireLight;
        public GunState CurState
        {
            get => gunAnimatorController.CurState;
            set => gunAnimatorController.SetAnim(value);
        }
        private GunAnimatorController gunAnimatorController;
        private SpriteRenderer icon;

        public void Init()
        {
            gunAnimatorController = new GunAnimatorController(this);
        }

        public void ReloadGun(GunData data)
        {
            gridObjectSo = data;
            gunAnimatorController.ReloadAnimator(data.gunAnimator);
            CurState = GunState.Idle;
        }
     
        public void AnimatorStateEnter()
        {
            switch (CurState)
            {
                case GunState.Shot:

                    fireLight.enabled = true;
                    this.DelayExecute(0.07f, () =>
                    {
                        fireLight.enabled = false;
                    });
                    break;
            }
        }

        public void AnimatorStateComplete()
        {
            switch (CurState)
            {
                case GunState.Shot:
                    CurState = GunState.Idle;
                    break;
            }
        }
    }
    
}
