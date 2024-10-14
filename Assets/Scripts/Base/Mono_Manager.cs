using EquipmentSystem;
using GridSystem;
using other;
using ui;
using UnityEngine;
using UnityEngine.Events;

//mono管理
//1.生命周期函数
//2.事件
//3.协程
namespace game
{
    public class Mono_Manager : KeepMonoSingleton<Mono_Manager>
    {

        #region Listener


        public override void BeforeSceneChange()
        {
            // StopAllCoroutines();
        }

        #endregion

        // Update is called once per frame
        void Update()
        {
            CheckThing();
        }

        void FixedUpdate()
        {
            
        }

        
        private void CheckThing()
        {
            // if(!GameSetting.Instance.EditorMode)
            //     return;
            // Test_Audio_Manager.Instance.InputCheck();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                EditorUi();
            }
                   // GameManager.Instance.LevelComplete(SettlementType.Lose);
            // else if(Input.GetKeyDown(KeyCode.O))
            //     GameManager.Instance.LevelComplete(SettlementType.Victory);
        }

        private void EditorUi()
        {
           
// #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Package_Panel.Instance.AddItemToBox("Meat");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                   
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {                
                
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                
            }
// #endif
        }
    }

    

}
