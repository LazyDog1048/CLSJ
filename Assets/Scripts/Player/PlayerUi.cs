using System.Collections;
using System.Collections.Generic;
using other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUi : Mono_Singleton<PlayerUi>
    {
        private Image _staminaImage;
        private TextMeshProUGUI _staminaText;
        
        private Image _gunImage;
        private TextMeshProUGUI _gunText;
        protected override void Awake()
        {
            base.Awake();
            var _staminaBar = transform.Find("StaminaBar");
            _staminaImage = _staminaBar.Find("Image").GetComponent<Image>();
            _staminaText = _staminaBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            
            var _gunBar = transform.Find("GunBar");
            _gunImage = _gunBar.Find("Image").GetComponent<Image>();
            _gunText = _gunBar.transform.Find("Text").GetComponent<TextMeshProUGUI>();
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
        
    }
    
}
