using UnityEngine;

namespace ui
{
    public class FontPanel : BasePanel
    {
        public PanelType PanelType = PanelType.Normal;
        public virtual LayerType TargetLayerType => LayerType.Font;
        public bool IsHide => CurLayerType == LayerType.Hide;
        protected bool CanShow =>CheckCanShow();
        protected bool CanHide =>CheckCanHide();
        protected virtual bool CanAutoHide => true;
        public LayerType CurLayerType;

        protected FontPanel(Transform trans) : base(trans)
        {
            CurLayerType = LayerType.None;
            LayerPanel.Instance.AddPanel(this);
        }

        public virtual void Show()
        {
            if(!CanShow)
               return;
            // Debug.Log($"Show {panelTypeString}");
            CurLayerType = TargetLayerType;
            LayerPanel.Instance.ShowPanel(this);
            OnShowAction();
        }

        protected virtual void OnShowAction()
        {
            
        }

        public void AutoHide()
        {
            if(CanAutoHide)
                Hide();
        }
        
        public virtual void Hide()
        {
            if(!CanHide)
                return;
            // Debug.Log($"Hide {panelTypeString}");
            CurLayerType = LayerType.Hide;
            LayerPanel.Instance.HidePanel(this);
            OnHideAction();
        }

        protected virtual void OnHideAction()
        {
            
        }
        
        public virtual void ShowOrHide()
        {
            if(IsHide)
                Show();
            else
                Hide();
        }

        protected virtual bool CheckCanShow()
        {
            return IsHide || CurLayerType == LayerType.None;
        }
        
        protected virtual bool CheckCanHide()
        {
            return !IsHide;
        }

        public virtual void DestroyPanel()
        {
            Hide();
            RemoveListener();
            LayerPanel.Instance.RemoveFont(this);
            GameObject.Destroy(obj);
        }
    }

}
