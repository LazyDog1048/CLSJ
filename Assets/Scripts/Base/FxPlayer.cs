using data;
using game;
using so;
using UnityEngine;

namespace item
{
    public partial class FxPlayer : MonoPoolObj,IAnimatorController
    {
        private FxSoData _soData;
        private FxParamater paramater;
        protected virtual FxSoData SoData
        {
            get => _soData;

            private set
            {
                _soData = value;
                
                if (value == null)
                {
                    OnSetDataNull();
                    fxAnimator.SetAnimNull();
                    return;
                }
                if(paramater.delayShowTime>0)
                    this.DelayExecute(paramater.delayShowTime, PlayParticle);
                else
                {
                    PlayParticle();
                }
            }
        }

        protected Vector3 position;
        protected FxAnimController fxAnimator;
        protected SpriteRenderer spriteRenderer;
        public FxCallback onComplete;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            // anim = GetComponentInChildren<FxAnimController>();
            fxAnimator = new FxAnimController(this);
        }

        protected virtual void OnSetDataNull()
        {
            spriteRenderer.sprite = null;
        }
        protected virtual void Play(FxSoData fxSoData,Transform parent)
        {
            Play(fxSoData,parent.position);
        }

        protected virtual void Play(FxSoData fxSoData,Vector3 pos)
        {
            paramater = new FxParamater(fxSoData);
            // Debug.Log($"{fxData.FxParamater.Name}");
            position = pos;
            SoData = fxSoData;
        }

        public FxPlayer Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
            return this;
        }
        public FxPlayer FlipX(bool isFlip)
        {
            if (paramater.isRandomFlip)
            {
              isFlip = 50.RandomBy100Percent();  
            }
            isFlip = paramater.isInvert ? !isFlip : isFlip;
            
            if(paramater.isFlip)
                transform.localScale = new Vector3(isFlip?-1:1,1,1);
            return this;
        }

        public virtual FxPlayer Position(Vector3 pos)
        {
            position = pos;
            return this;
        }
        
        protected virtual void PlayParticle()
        {
            // FadeFxPlayer.PlayFadeFx("Fx_GroundShadow", position, 1, 0);
            transform.position = position;
            transform.rotation = Quaternion.Euler(0, 0, paramater.rotaOffset);
            spriteRenderer.sortingLayerName = paramater.Layer;
            spriteRenderer.sortingOrder = paramater.sortingOrder;
            fxAnimator.ReloadAnimator(SoData.AnimatorController);
        }

        public override void OnPushObj()
        {
            onComplete?.Invoke();
            transform.localScale = Vector3.one;
            SoData = null;
            onComplete = null;
            base.OnPushObj();
        }
        
        #region IAnimController

        public void AnimatorStateEnter()
        {
            
        }

        public virtual void AnimatorStateComplete()
        { 
            if(!paramater.isLoop)
                ReleaseObj();
        }
        #endregion
       
    }

    public partial class FxPlayer
    {
        public static T Load<T>() where T:FxPlayer
        {
            string path = $"{Fx_Path.Re_ItemsPath}/{typeof(T).Name}";
            return PoolManager.Instance.PopObj<T>(typeof(T).Name,path);
        }
        
        public static FxPlayer PlayFx(string dataName,Transform parent)
        {
            return PlayFx(dataName, parent.position);
        }

        public static FxPlayer PlayFx(string dataName,Vector3 pos)
        {
            if (dataName == null)
                return null;
            FxSoData soData = ResourcesDataManager.GetFxSoData(dataName);
            return PlayFx(soData, pos);
        }
        
        public static FxPlayer PlayFx(FxSoData soData,Vector3 pos)
        {
            if (soData == null)
                return null;
            FxPlayer fx = Load<FxPlayer>();
            fx.Play(soData, pos);
            return fx;
        }

        public static FxPlayer PlayFx(FxSoData soData,Transform parent)
        {
            if (soData == null)
                return null;
            FxPlayer fx = Load<FxPlayer>();
            fx.Play(soData,parent.position);
            return fx;
        }
    }
    
    public delegate void FxCallback();
}

