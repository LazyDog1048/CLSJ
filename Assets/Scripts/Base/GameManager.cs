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
            DataManager.Instance.Create();
            ResourcesDataManager.Instance.Create();
            InputManager.Instance.Create();
            GamePlay_InputAction.Instance.Create();
            Ui_InputAction.Instance.Create();
            CameraManager.Instance.Load();
            LayerPanel.Load();
            PlayerUiPanel.Load();
            Package_Panel.Load();
        }
        
        public void Pause(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        
    }
    
}
