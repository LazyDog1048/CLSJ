using System;
using UnityEngine;

namespace game
{
    public class SceneObject : MonoBehaviour
    {
        private GameObject eCheck;

        protected virtual void Awake()
        {
            eCheck = transform.Find("ECheck").gameObject;
            eCheck.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals("Player"))
            {
                eCheck.gameObject.SetActive(true);
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag.Equals("Player"))
            {
                eCheck.gameObject.SetActive(false);
            }
        }

        public virtual void PressE()
        {
            
        }
    }
    
}
