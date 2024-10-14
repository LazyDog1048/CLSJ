using UnityEngine;

namespace other
{
    public class GameCursor : Mono_Singleton<GameCursor>
    {
        private GameObject body;
        private bool cursorClicked;

        protected override void Awake()
        {
            base.Awake();
            body = transform.Find("Body").gameObject;
        }

        private void CursorNormal()
        {
            
        }

        public void HideCursor()
        {
            body.gameObject.SetActive(false);
        }
        
        public void ShowCursor()
        {
            body.gameObject.SetActive(true);
        }
        
        private void Update()
        {
            transform.position = GetMousePos.GetMousePosition();
        }

    }

    public enum CursorState
    {
        Normal_Idle,
        Normal_Click,
        Gather_Idle,
        Gather_X_Idle,
        Gather_X_Click,
        Teleport_Idle,
    }

    
}
