using other;
using UnityEngine;

namespace game
{
    public class UiCursor : Mono_Singleton<UiCursor>
    {
        private void Update()
        {
            transform.position = GetMousePos.GetUiMousePositionWithZ();
        }
    }
    
}
