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
        
        private Image _gunImage;
        private TextMeshProUGUI _gunText;
        private Image _reloadingImage;
        
        public static PlayerUiPanel Load()
        {
            return Create(UiObjRefrenceSO.Instance.PlayerUiPanel);
        }
        
        public PlayerUiPanel(Transform trans) : base(trans)
        {
            var _staminaBar = trans.Find("StaminaBar");
            _staminaImage = _staminaBar.Find("Image").GetComponent<Image>();
            _staminaText = _staminaBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            
            var _gunBar = trans.Find("GunBar");
            _gunImage = _gunBar.Find("Image").GetComponent<Image>();
            _gunText = _gunBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            _reloadingImage = _gunBar.Find("ReloadingImage").GetComponent<Image>();
        }
        
        public void UpdateStaminaBar(int currentStamina,int maxStamina)
        {
            _staminaImage.fillAmount = (float)currentStamina / maxStamina;
            _staminaText.text = $"{currentStamina}/{maxStamina}";
        }
        
        public void UpdateGunBar(int currentAmmo,int maxAmmo)
        {
            _gunImage.fillAmount = (float)currentAmmo / maxAmmo;
            _gunText.text = $"{currentAmmo}/{maxAmmo}";
        }
        
        public void GunReloading(float time)
        {
            _reloadingImage.fillAmount = 0;
            _reloadingImage.DOFillAmount(1, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                _reloadingImage.fillAmount = 0;
            });
        }

        public void SwitchGun(BaseGun gun)
        {
            UpdateGunBar(gun.currentAmmo,gun.maxAmmo);
        }
    }
}
