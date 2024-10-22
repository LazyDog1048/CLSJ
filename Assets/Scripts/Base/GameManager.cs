using data;
using GridSystem;
using other;
using Player;
using ui;

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

    }
    
}
