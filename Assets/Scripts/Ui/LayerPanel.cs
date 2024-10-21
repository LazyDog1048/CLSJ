using System.Collections.Generic;
using System.Linq;
using game;
using GridSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ui
{
    public enum LayerType
    {
        None,
        Hide,
        Buttom,
        Middle,
        StaticUi,
        Font
    }
    
    public class LayerPanel :BasePanel
    {
        public static LayerPanel Instance { get; private set; }
        public static bool cantHidePanel; 
        public bool isFontNull => fontPanelStack.Count == 0;
        // public bool LockPlayer =>fontPanelStack.Any(basePanel => basePanel.PanelType == PanelType.LockPlayer);
        public bool PauseGame =>fontPanelStack.Any(basePanel => basePanel.PanelType == PanelType.PauseGame);
        
        private Dictionary<LayerType,RectTransform> layerDic;
        private Dictionary<string,FontPanel> panelDic;
        private Stack<FontPanel> fontPanelStack;
        private FontPanel defaultPanel;
        public Canvas canvas { get;private set; }
        public static RectTransform Panel_Hide => Instance.layerDic[LayerType.Hide];
        public static RectTransform Panel_Font => Instance.layerDic[LayerType.Font];

        private UiObjRefrenceSO uiObjRefrenceSO;
        public static LayerPanel Load()
        {
            GameObject go = GameObject.Instantiate(UiObjRefrenceSO.Instance.CommonUi_LayerPanelObj);
            GameObject.DontDestroyOnLoad(go);
            go.name = nameof(LayerPanel);
            Instance = new LayerPanel(go.transform);
            return Instance;
        }
        
        public LayerPanel(Transform trans) : base(trans)
        {
            InitCanvas(trans);
            Instance = this;
            layerDic = new Dictionary<LayerType, RectTransform>();
            panelDic = new Dictionary<string, FontPanel>();
            fontPanelStack = new Stack<FontPanel>();
            layerDic.Add(LayerType.Font,trans.Find("FontPanel") as RectTransform);
            layerDic.Add(LayerType.StaticUi,trans.Find("StaticPanel") as RectTransform);
            layerDic.Add(LayerType.Middle,trans.Find("MiddlePanel") as RectTransform);
            layerDic.Add(LayerType.Buttom,trans.Find("BottomPanel") as RectTransform);
            layerDic.Add(LayerType.Hide,trans.Find("HidePanel") as RectTransform);

            Ui_InputAction.Instance.ConfirmUiAction(ConfirmUi);
            Ui_InputAction.Instance.RightClickUiAction(RightClick);
            Ui_InputAction.Instance.CancelUiAction(CancelUi);
            Ui_InputAction.Instance.SpeedUiRegisterAction(SpeedChange);
            Ui_InputAction.Instance.MouseMoveRegisterAction(MouseMoveUi);
        }

        private void InitCanvas(Transform trans)
        {
            canvas = trans.GetComponent<Canvas>();
            canvas.worldCamera = CameraManager.Instance.mainCamera;
        }


        #region basePanel
        public override void RemoveListener()
        {
            Ui_InputAction.Instance.DisposeInputAction();
            foreach (var key_val in panelDic)
            {
                key_val.Value.RemoveListener();
            }
        }
        #endregion

        private void MouseMoveUi(InputAction.CallbackContext context)
        {
            // Debug.Log("MouseMoveUi");
            Package_Panel.Instance.MouseMove(context);
            // if (!isFontNull)
            //     fontPanelStack.Peek().MouseMove(context);
        }
        
        private void SpeedChange(InputAction.CallbackContext context)
        {
        }
        
        private void ConfirmUi(InputAction.CallbackContext context)
        {
            Package_Panel.Instance.Confirm(context);
            // if (!isFontNull)
            //     fontPanelStack.Peek().Confirm(context);
        }
        
        private void RightClick(InputAction.CallbackContext context)
        {
            Package_Panel.Instance.RightClick(context);
            // if (!isFontNull)
            //     fontPanelStack.Peek().Confirm(context);
        }
        
        private void CancelUi(InputAction.CallbackContext context)
        {
            if(context.phase != InputActionPhase.Started || cantHidePanel)
                return;
            
            if(context.control.device is Keyboard && isFontNull)
            {
                defaultPanel?.Show();
                return;
            }
            
            if(!isFontNull)
                fontPanelStack.Peek().Hide();
        }


        private void MoveUI(InputAction.CallbackContext context)
        {
            if(context.phase != InputActionPhase.Started)
                return;
            if(context.control.device is Keyboard && isFontNull)
            {
                defaultPanel?.Move(context,Ui_InputAction.moveDir);
                return;
            }
            if(!isFontNull)
                fontPanelStack.Peek().Move(context,Ui_InputAction.moveDir);
        }
        
        public void AddPanel(FontPanel panel)
        {
            panelDic.Add(panel.panelTypeString,panel);
            panel.transform.SetParent(layerDic[LayerType.Hide]);
            SetPanel(panel.rectTransform);
            if (panel.TargetLayerType == LayerType.StaticUi)
            {
                panel.Show();
            }
            else
            {
                panel.Hide();
            }
        }

        private void SetPanel(RectTransform panel)
        {
            // if(panel.GetComponent<Canvas>()!=null)
            SetForCanvas(panel);
            panel.localScale = Vector3.one;
        }
        
        private void SetForCanvas(RectTransform panel)
        {
            // Debug.Log(panel.name);
            panel.anchoredPosition3D = Vector2.zero;
            panel.sizeDelta = Vector2.zero;
            
            panel.pivot = Vector2.zero;
            panel.anchorMin = Vector2.zero;
            panel.anchorMax = Vector2.one;
            
        }
        public void SetDefaultPanel(FontPanel panel)
        {
            defaultPanel = panel;
        }

        public void ShowPanel(string panelName)
        {
            if(panelDic.ContainsKey(panelName))
                 panelDic[panelName].Show();
        }

        public void HidePanel(string panelName)
        {
            if(panelDic.ContainsKey(panelName))
                panelDic[panelName].Hide();
        }
        
        public void ShowOrHidePanel(string panelName)
        {
            if(panelDic.ContainsKey(panelName))
                panelDic[panelName].ShowOrHide();
        }
        public void ShowFontPanel(FontPanel panel)
        {
            panel.Show();
            // panel.transform.SetAsLastSibling();
        }
        public void ShowPanel(FontPanel panel)
        {
            if (panel.TargetLayerType == LayerType.Font)
            {
                fontPanelStack.Push(panel);
                CheckPanelShow();
            }
            panel.transform.SetParent(layerDic[panel.TargetLayerType]);
        }
        
        public void HidePanel(FontPanel panel)
        {
            if (fontPanelStack.Count > 0 && panel.TargetLayerType == LayerType.Font)
            {
                fontPanelStack.Pop();
                CheckPanelHide();
            }
            panel.transform.SetParent(layerDic[LayerType.Hide]);
        }

        private void CheckPanelShow()
        {
            //font不为空
            if (!isFontNull)
            {
                GamePlay_InputAction.Instance.UiBlock(true);
            }
            if(PauseGame)
            {
                GamePlay_InputAction.Instance.UiBlock(true);
            }
        }
        private void CheckPanelHide()
        {
            if (isFontNull)
            {
                GamePlay_InputAction.Instance.UiBlock(false);
            }
            
            if (!PauseGame)
            {
                GamePlay_InputAction.Instance.UiBlock(false);
                             
            }
        }
  
        private BasePanel GetBasePanel(string panelName)
        {
            if (!panelDic.ContainsKey(panelName))
                return null;
            return panelDic[panelName];
        }
        
        private BasePanel GetBasePanel<T>()
        {
            string panelName = typeof(T).Name;
            return GetBasePanel(panelName);
        }

        public void HideAllFont()
        {
            while (fontPanelStack.Count > 0)
            {
                // if(fontPanelStack.Peek() is Mask_Panel)
                //     continue;
                fontPanelStack.Peek().AutoHide();
            }
        }

        public void RemoveFont(FontPanel panel)
        {
            panelDic.Remove(panel.panelTypeString);
        }

        public void RemoveAllFont()
        {
            if(!Application.isPlaying)
                return;
            var keyList = panelDic.Keys.ToArray();
            for(int i = keyList.Length-1;i>=0;i--)
            {
                FontPanel fontPanel = panelDic[keyList[i]];
                fontPanel.DestroyPanel();
            }
        }
    }
}