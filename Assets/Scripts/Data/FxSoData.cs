using System;
using data;
using UnityEngine;


namespace so
{
    
    [CreateAssetMenu(fileName = "FxData", menuName = "SO/FxData", order = 0)]
    public class FxSoData : ScriptableObject
    {
        public string Name;
        public FxType fxType = FxType.Normal;
        public string Layer = "Default";
        public int sortingOrder = 2;
        public float delayShowTime = 0;
        public float rotaOffset;
        public bool isLoop;
        public bool isFlip;
        public bool isRandomFlip;
        public bool isInvert;
        public bool isRotate;
        public RuntimeAnimatorController AnimatorController;
    }

    [Serializable]
    public class FxParamater
    {
        public string Name;
        // public string NameKey=>$"Name_{Name}";
        // public string DescriptionKey =>$"Description_{Name}";
        
        #region get&set
        public FxType fxType;
        public string Layer;
        public int sortingOrder;
        public float delayShowTime;
        public float rotaOffset;
        public bool isLoop;
        public bool isFlip;
        public bool isRandomFlip;
        public bool isInvert;
        public bool isRotate;
        
        #endregion
        
        public FxParamater(FxSoData fxSoData)
        {
            Name = fxSoData.Name;
            fxType = fxSoData.fxType;
            Layer = fxSoData.Layer;
            sortingOrder = fxSoData.sortingOrder;
            delayShowTime = fxSoData.delayShowTime;
            rotaOffset = fxSoData.rotaOffset; 
            isLoop = fxSoData.isLoop;
            isFlip = fxSoData.isFlip;
            isRandomFlip = fxSoData.isRandomFlip;
            isInvert = fxSoData.isInvert;
            isRotate = fxSoData.isRotate;
        }
        
    }

    public enum FxType
    {
        Normal,
        Explosion
    }
    
}
