using DG.Tweening;
using EquipmentSystem;
using TMPro;
using ui;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUiPanel : SingletonPanel<PlayerUiPanel>
    {
        public override LayerType TargetLayerType => LayerType.StaticUi;
        private Image _staminaImage;
        private TextMeshProUGUI _staminaText;
        
        private Image hpImage;
        private TextMeshProUGUI hpText;
        
        
        private Image _gunImage;
        private TextMeshProUGUI _gunText;
        private Transform _gunBar;

        private Transform reloadTransform;
        private Image reloadingImage;
        public static PlayerUiPanel Load()
        {
            return Create(UiObjRefrenceSO.Instance.PlayerUiPanel);
        }
        
        public PlayerUiPanel(Transform trans) : base(trans)
        {
            var _staminaBar = trans.Find("StaminaBar");
            _staminaImage = _staminaBar.Find("Image").GetComponent<Image>();
            _staminaText = _staminaBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            
            var hpBar = trans.Find("HpBar");
            hpImage = hpBar.Find("Image").GetComponent<Image>();
            hpText = hpBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            _gunBar = trans.Find("GunBar");
            _gunImage = _gunBar.Find("Image").GetComponent<Image>();
            _gunText = _gunBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            
            reloadTransform = trans.Find("Reloading");
            reloadingImage = reloadTransform.Find("Image").GetComponent<Image>();
            
        }
        
        public void UpdateStaminaBar(int currentStamina,int maxStamina)
        {
            _staminaImage.fillAmount = (float)currentStamina / maxStamina;
            _staminaText.text = $"{currentStamina}/{maxStamina}";
        }
        
        public void UpdateHpBar(int currentHp,int MaxHp)
        {
            hpImage.fillAmount = (float)currentHp / MaxHp;
            hpText.text = $"{currentHp}/{MaxHp}";
        }
        
        public void UpdateGunBar(int currentAmmo,int maxAmmo)
        {
            _gunImage.fillAmount = (float)currentAmmo / maxAmmo;
            _gunText.text = $"{currentAmmo}/{maxAmmo}";
        }
        
        public void GunReloading(float time)
        {
            reloadTransform.gameObject.SetActive(true);
            
            reloadingImage.fillAmount = 0;
            var doTween = reloadingImage.DOFillAmount(1, time).SetEase(Ease.Linear);
            doTween.onUpdate = () =>
            {
                Debug.Log(reloadingImage.fillAmount);
                reloadTransform.position = PlayerController.Instance.Head.position;
            };
            
            doTween.OnComplete(() =>
            {
                reloadTransform.position = new Vector3(1000,1000,1000);
                reloadTransform.gameObject.SetActive(false);
            });
        }

        public void SwitchGun(BaseGun gun)
        {
            if(gun == null)
                _gunBar.gameObject.SetActive(false);
            else
            {
                _gunBar.gameObject.SetActive(true);
                UpdateGunBar(gun.currentAmmo,gun.maxAmmo);
            }
        }
    }
}
