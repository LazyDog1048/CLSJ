using System;
using System.Collections;
using System.Collections.Generic;
using data;
using GridSystem;
using other;
using Player;
using ui;
using UnityEngine;

namespace game
{
    public class GameManager : KeepMonoSingleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
            DataManager.Instance.SaveAllData();
            LayerPanel.Load();
            PlayerUiPanel.Load();
            Package_Panel.Load();
        }

    }
    
}
