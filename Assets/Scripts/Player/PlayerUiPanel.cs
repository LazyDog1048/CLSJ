using DG.Tweening;
using EquipmentSystem;
using game;
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

        private RectTransform reloadTransform;
        private Image reloadingImage;
        
        private Transform batteryBar;
        private Image _batteryImage;
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
            
            reloadTransform = trans.Find("Reloading") as RectTransform;
            reloadingImage = reloadTransform.Find("Image").GetComponent<Image>();

            batteryBar = trans.Find("BatteryBar");
            _batteryImage = batteryBar.Find("Image").GetComponent<Image>();
            
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
        
        public void UpdateBatteryBar(float currentTime,float MaxTime)
        {
            _batteryImage.fillAmount = currentTime / MaxTime;
        }
        
        public void GunReloading(float time)
        {
            ReloadingUi.Instance.GunReloading(time);
            return;
            reloadTransform.gameObject.SetActive(true);
            reloadingImage.fillAmount = 0;
            var doTween = reloadingImage.DOFillAmount(1, time).SetEase(Ease.Linear);
            doTween.onUpdate = () =>
            {
                reloadTransform.anchoredPosition = CameraManager.Instance.PlayerPosToUiPos();
            };
            
            doTween.OnComplete(() =>
            {
                reloadTransform.anchoredPosition = new Vector2(9999, 9999);
                reloadTransform.gameObject.SetActive(false);
            });
        }

        public void SwitchGun(PlayerGun gun)
        {
            if(gun == null)
                _gunBar.gameObject.SetActive(false);
            else
            {
                if (gun is Lamp lamp)
                {
                    batteryBar.gameObject.SetActive(true);
                    _gunBar.gameObject.SetActive(false);
                    UpdateBatteryBar(lamp.currentBattery, lamp.time);
                }
                else if (gun is Hand)
                {
                    batteryBar.gameObject.SetActive(false);
                    _gunBar.gameObject.SetActive(false);
                }
                else
                {
                    _gunBar.gameObject.SetActive(true);
                    batteryBar.gameObject.SetActive(false);
                    UpdateGunBar(gun.currentAmmo,gun.maxAmmo);
                }
            }
        }
    }
}
