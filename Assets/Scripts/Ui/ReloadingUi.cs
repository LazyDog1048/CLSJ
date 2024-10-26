using DG.Tweening;
using other;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class ReloadingUi : Mono_Singleton<ReloadingUi>
    {
        private RectTransform reloadTransform;
        private Image reloadingImage;

        protected override void Awake()
        {
            base.Awake();
            reloadTransform = transform as RectTransform;
            reloadingImage = reloadTransform.Find("Image").GetComponent<Image>();
            gameObject.SetActive(false);
        }

        
        
        public void GunReloading(float time)
        {
            reloadTransform.gameObject.SetActive(true);
            reloadingImage.fillAmount = 0;
            var doTween = reloadingImage.DOFillAmount(1, time).SetEase(Ease.Linear);
            // doTween.onUpdate = () =>
            // {
            //     reloadTransform.transform.position = PlayerController.Instance.Head.position;
            // };
            
            doTween.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
    
}
