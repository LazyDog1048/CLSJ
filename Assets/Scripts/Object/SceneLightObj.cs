using UnityEngine;

namespace game
{
    public class SceneLightObj : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col.gameObject.tag);
            if(!col.gameObject.tag.Equals("HideInShadow"))
                return;
            var lightObj = col.GetComponentInParent<LightObj>();
            if (lightObj)
            {
                lightObj.LightEnter();
            }
        }
        
        private void OnTriggerStay2D(Collider2D col)
        {
            if(!col.gameObject.tag.Equals("HideInShadow"))
                return;
            var lightObj = col.GetComponentInParent<LightObj>();
            if (lightObj)
            {
                lightObj.LightEnter();
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if(!col.gameObject.tag.Equals("HideInShadow"))
                return;
            var lightObj = col.GetComponentInParent<LightObj>();
            if (lightObj)
            {
                lightObj.LightExit();
            }
        }
    }
    
}
