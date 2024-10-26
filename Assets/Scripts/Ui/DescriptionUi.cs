using DG.Tweening;
using other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class DescriptionUi : Mono_Singleton<DescriptionUi>
    {
        private RectTransform reloadTransform;
        private Image image;
        private TextMeshProUGUI text;
        private float stayTime = 5f;
        protected override void Awake()
        {
            base.Awake();
            reloadTransform = transform as RectTransform;
            image = transform.Find("Image").GetComponentInChildren<Image>();
            text = transform.Find("Text").GetComponentInChildren<TextMeshProUGUI>();
            gameObject.SetActive(false);
        }

        public void ShowDescription(string description)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
            text.text = description;
            DoFade(0,1,1f);
            this.DelayExecute(stayTime, () =>
            {
                DoFade(1,0,1f);
                this.DelayExecute(1, () =>
                {
                    gameObject.SetActive(false);
                });
            });
        }
        
        
        public void DoFade(float startA,float a,float time)
        {
            gameObject.SetActive(true);
            image.DOFade(startA, 0);
            text.DOFade(startA, 0);
            image.DOFade(a, time);
            text.DOFade(a, time);

        }
    }
    
}
