using DG.Tweening;
using UnityEngine;

namespace game
{
    public class GameTips : SceneObject
    {
        [SerializeField]
        private float stayTime;
        private SpriteRenderer tips;
        
        protected override void Awake()
        {
            base.Awake();
            tips = transform.Find("Tips").GetComponent<SpriteRenderer>();
        }
        
        
        public override void PressE()
        {
            ShowTips();
        }

        private void ShowTips()
        {
            tips.gameObject.SetActive(false);
            tips.gameObject.SetActive(true);
            DoFade(0,1,1f);
            this.DelayExecute(stayTime, () =>
            {
                DoFade(1,0,1f);
                this.DelayExecute(1, () =>
                {
                    tips.gameObject.SetActive(false);
                });
            });
        }
        
        
        public void DoFade(float startA,float a,float time)
        {
            tips.gameObject.SetActive(true);
            tips.DOFade(startA, 0);
            tips.DOFade(a, time);
            

        }
    }
    
}
