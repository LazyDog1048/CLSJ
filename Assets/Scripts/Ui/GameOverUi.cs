using System.Collections.Generic;
using data;
using game;
using UnityEngine;

namespace ui
{
    public class GameOverUi : MonoBehaviour,IAnimatorController
    {
        [SerializeField]
        private  FxAudioSourceClip endingAudio;
        public GameoverAnimator animator;
        
        public void Init()
        {
            animator = new GameoverAnimator(this);
        }

        public void Enter()
        {
            endingAudio.PlayClip();
            animator.SetAnim(GameOverUiType.Enter);
        }
        public void AnimatorStateEnter()
        {
            
        }

        public void AnimatorStateComplete()
        {
            if(animator.CurState == GameOverUiType.Enter)
                animator.SetAnim(GameOverUiType.Idle);
        }
    }
    
    public enum GameOverUiType
    {
        Enter,
        Idle
    }
    public class GameoverAnimator:BaseAnimController
    {
        public GameOverUiType CurState { get; set; }
        protected override int curState => (int)CurState;
        private Dictionary<GameOverUiType,int> animStateHashDic;
        
        public GameoverAnimator(GameOverUi enemy) : base(enemy, enemy)
        {
        }
        
        protected override void SetStateDic()
        {
            animStateHashDic = new Dictionary<GameOverUiType, int>
            {
                {GameOverUiType.Idle, AnimaHash.state_Idle},
                {GameOverUiType.Enter, AnimaHash.state_Enter}
            };
            
            CurState = GameOverUiType.Idle;
            AddLockState((int)GameOverUiType.Enter);
        }

        public void SetAnim(GameOverUiType state)
        {
            base.SetAnim((int)state);
        }


        protected override void ChangeAnimState(int state)
        {
            
            CurState = (GameOverUiType) state;
            
            animator.Play(animStateHashDic[CurState],0,0);
        }
        
        protected override void WaitStateComplete()
        {
            int tempState = curState;
            AnimController.AnimatorStateEnter();               
            mono.DelayRealTimeExecute(0.02f,() =>
            {
                mono.DelayRealTimeExecute(animator.GetCurrentAnimatorStateInfo(0).length - StaticValue.FixedTimeScale, () => { Complete(tempState); });
            });
        }
        
    }
}
