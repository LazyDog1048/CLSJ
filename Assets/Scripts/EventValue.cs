using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace buff
{
    public delegate T DOGetter<out T>();
    public delegate void DOSetter<in T>(T pNewValue);

    public delegate void DefaultEvent();
    public abstract class ChangeValue<T>
    {
        public UnityEvent valueChange;
        protected  DOGetter<T> Getter;
        protected DOSetter<T> Setter;
        public T Value
        {
            get => Getter();
            set => Setter(value);
        }

        protected ChangeValue(DOGetter<T> getter,DOSetter<T> setter)
        {
            Getter = getter;
            Setter = setter;
            valueChange = new UnityEvent();
        }
        
        
        protected ChangeValue(T value)
        {
            var oriValue = value;
            Getter = () => oriValue;
            Setter = (x) => oriValue = x;
            valueChange = new UnityEvent();
        }

        
        protected virtual void SmoothChange(T endValue,float time)
        {
            
        }
    }

    public class EventSprite : ChangeValue<Sprite>
    {
        public EventSprite(DOGetter<Sprite> getter, DOSetter<Sprite> setter) : base(getter, setter)
        {
            
        }
    }
    
    public class EventBool : ChangeValue<bool>
    {
        public EventBool(DOGetter<bool> getter, DOSetter<bool> setter) : base(getter, setter)
        {
            
        }
    }
    
    public class EventFloat : ChangeValue<float>
    {
        public EventFloat(DOGetter<float> getter, DOSetter<float> setter) : base(getter, setter)
        {
        }
        
        protected EventFloat(float value) :base(value)
        {
            var oriValue = value;
            Getter = () => oriValue;
            Setter = (x) => oriValue = x;
            valueChange = new UnityEvent();
        }
    }
    
    public class EventInt : ChangeValue<int>
    {
        public EventInt(DOGetter<int> getter, DOSetter<int> setter) : base(getter, setter)
        {
        }
        
        protected EventInt(int value) :base(value)
        {
            var oriValue = value;
            Getter = () => oriValue;
            Setter = (x) => oriValue = x;
            valueChange = new UnityEvent();
        }
    }
    
    
    public class EventExtraFloat : EventFloat
    {
        private float _extraValue;
        private float _extraRate;
        private float Rate => 1 + ExtraRate;
         
        public float ExtraValue
        {
             get => _extraValue;
             set
             {
                 _extraValue = value;
                 valueChange?.Invoke();
             }
        }
        public float ExtraRate
        {
             get => _extraRate;
             set
             {
                 _extraRate = value;
                 valueChange?.Invoke();
             }
        }

        public float FinalValue => (Value + ExtraValue) * Rate;
        
        public float GetFinalValue(float value)
        {
            return (value + ExtraValue) * Rate;
        }

        public EventExtraFloat(float baseValue) : base(baseValue)
        {
            
        }
        protected override void SmoothChange(float endValue,float time)
        {
            DOTween.To(() => ExtraValue, (x) => ExtraValue = x, endValue, time);
        }

        public void Reset()
        {
            _extraValue = 0;
            _extraRate = 0;
        }
    }
    public class EventExtraInt : EventInt
    {
        private int _extraValue;
        private float _extraRate;
        private float Rate => 1 + ExtraRate;
         
        public int ExtraValue
        {
            get => _extraValue;
            set
            {
                _extraValue = value;
                valueChange?.Invoke();
            }
        }
        public float ExtraRate
        {
            get => _extraRate;
            set
            {
                _extraRate = value;
                valueChange?.Invoke();
            }
        }

        public int FinalValue => (int)((Value + ExtraValue) * Rate);

        public int GetFinalValue(int value)
        {
            return (int)((value + ExtraValue) * Rate);
        }
        public EventExtraInt(int baseValue) : base(baseValue)
        {
            
        }

        protected override void SmoothChange(int endValue,float time)
        {
            DOTween.To(() => ExtraValue, (x) => ExtraValue = x, endValue, time);
        }
        
        public void Reset()
        {
            _extraValue = 0;
            _extraRate = 0;
        }
    }
    
    
}
