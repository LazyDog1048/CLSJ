using game;
using ui;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace tool
{
    public static class UiTool
    {
        public static void ForeachAllChild(Transform transform, UnityAction<int,Transform> doThing)
        {
            if(transform==null || doThing==null)
                return;
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform rectTransform = transform.GetChild(i) as RectTransform;
                doThing?.Invoke(i,rectTransform);
                ForeachAllChild(rectTransform,doThing);
            }
        }
        
        public static void ForeachChild(Transform transform, UnityAction<int,Transform> doThing)
        {
            if(transform !=null)
                transform.ForeachChild(doThing);
        }

        public static void ForeachClone(Transform transform, GameObject obj,int count,UnityAction<int,Transform> doThing)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = GameObject.Instantiate(obj, transform);
                clone.SetActive(true);
                clone.transform.localScale = Vector3.one;
                clone.transform.localRotation = quaternion.identity;
                clone.name = $"{obj.name}_{i}";
            }
            ForeachChild(transform, doThing);
        }

        public static void CloneAndSort(Transform transform, GameObject obj,int count,Vector2 offset,UnityAction<int,Transform> doThing = null)
        {
            Vector2 previousAnchor = Vector2.zero;
            
            ForeachClone(transform,obj,count,ChangeChild);

            void ChangeChild(int index,Transform  childTrans)
            {
                RectTransform cloneRect = childTrans.GetComponent<RectTransform>();
                DisWithPrevious(cloneRect,ref previousAnchor,offset);
                doThing?.Invoke(index, childTrans);
            }
        }
        
        public static void ForeachChild(RectTransform rectTransform, UnityAction<int,RectTransform> doThing)
        {
            if(rectTransform==null || doThing==null)
                return;
            for (int i = 0; i < rectTransform.childCount; i++)
            {
                doThing?.Invoke(i,rectTransform.GetChild(i).GetComponent<RectTransform>());
            }
        }
        
        public static void SortChild(this RectTransform rectTransform,Vector2 offset)
        {
            Vector2 previousAnchor = Vector2.zero;
            ForeachChild(rectTransform, (index,rect) =>
            {
                DisWithPrevious(rect,ref previousAnchor,offset);
            });
        }

        public static void DisWithPrevious(RectTransform rectTransform,ref Vector2 previousAnchor,Vector2 offset)
        {
            //加上自身宽度的一半
            previousAnchor += rectTransform.sizeDelta/2;
            if(previousAnchor!=Vector2.zero)
                previousAnchor += offset;
            if (offset.x <= 0)
                previousAnchor.x = 0;
            if (offset.y <= 0)
                previousAnchor.y = 0;
            rectTransform.anchoredPosition = previousAnchor;
            //传递给下一个时 加上自身的宽度
            previousAnchor += rectTransform.sizeDelta/2;
            
        }
        
        public static Vector2 WorldToUiPos(RectTransform ParentUiRectTransform,Camera UiCamera, Vector3 worldPos)
        {
            //使用场景相机将世界坐标转换为屏幕坐标
            Vector2 screenUiPos = UiCamera.WorldToScreenPoint(worldPos);
            //屏幕坐标转换成ui坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ParentUiRectTransform, 
                screenUiPos,
                UiCamera, 
                out Vector2 retPos);
            return retPos;
        }
    
        public static Vector2 WorldToUiPos(RectTransform ParentUiRectTransform,Camera UiCamera)
        {
            //使用场景相机将世界坐标转换为屏幕坐标
            Vector2 screenUiPos = UiCamera.WorldToScreenPoint(GetMousePos.GetMousePosition());
            //屏幕坐标转换成ui坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ParentUiRectTransform, 
                screenUiPos,
                UiCamera, 
                out Vector2 retPos);
            return retPos;
        }
        
        public static Vector2 WorldToUiPos()
        {
            return WorldToUiPos(LayerPanel.Panel_Font,LayerPanel.Instance.canvas.worldCamera);
        }
        
        public static Vector2 WorldToUiPos(Vector3 pos)
        {
            //使用场景相机将世界坐标转换为屏幕坐标
            return WorldToUiPos(LayerPanel.Panel_Font, Camera.main, pos);
        }
        public static Vector2 WorldToUiPos(RectTransform transform, Vector3 pos)
        {
            //使用场景相机将世界坐标转换为屏幕坐标
            return WorldToUiPos(transform, Camera.main, pos);
        }
    }


}

