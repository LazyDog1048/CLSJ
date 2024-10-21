using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ui
{
    public class TextMeshProControll : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.raycastTarget = false;
        }
    }
    
}
