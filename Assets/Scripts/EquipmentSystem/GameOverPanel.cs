using game;
using UnityEngine;

namespace ui
{
    public class GameOverPanel : SingletonPanel<GameOverPanel>
    {
        public GameOverUi GameOverUi;
        public static GameOverPanel Load()
        {
            return Create(UiObjRefrenceSO.Instance.GameoverPanelObj);
        }
        public GameOverPanel(Transform trans) : base(trans)
        {
            PanelType = PanelType.PauseGame;
            GameOverUi = trans.GetComponentInChildren<GameOverUi>();
            GameOverUi.Init();
        }

        public void OpenDoor()
        {
            Show();
            GameOverUi.Enter();
            GamePlay_InputAction.Instance.Enable(false);
            Ui_InputAction.Instance.Enable(false);
        }
    }
       
}
