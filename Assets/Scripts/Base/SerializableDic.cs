using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace data
{
    [Serializable]
    public class SerializableDic<TKey,TValue>
    {
        public List<TKey> Keys=>keys;
        public List<TValue> Values=>values;
        
        
        // [ReadOnly]
        [HorizontalGroup("Key_Value",LabelWidth = 50)]
        [SerializeField]
        [OnValueChanged(nameof(ChangeKeyLength))]
        private List<TKey> keys;
        
        [HorizontalGroup("Key_Value")]
        [SerializeField]
        [OnValueChanged(nameof(ChangeValueLength))]
        private List<TValue> values;
        public int Count => keys.Count;
        public TValue this[TKey key]
        {
            get =>Get(key);
            set =>Set(key,value);
        }
        
        public SerializableDic()
        {
            keys = new List<TKey>();
            values = new List<TValue>();
            Init();
        }

        private void Init()
        {
            OnInit();
        }

        private void ChangeKeyLength()
        {
            if(keys.Count > values.Count)
                values.Add(default);
            else if(keys.Count < values.Count)
                values.RemoveAt(values.Count-1);
        }
        
        private void ChangeValueLength()
        {
            Debug.Log("change");
            if(values.Count > keys.Count)
                keys.Add(default);
            else if(values.Count < keys.Count)
                keys.RemoveAt(keys.Count-1);
        }
        
        protected virtual void OnInit()
        {
            
        }
        
        public void Set(TKey key,TValue value)
        {
            if (!keys.Contains(key))
            {
                Debug.LogError($"没有该key {key}");
                return;
            }
            values[keys.IndexOf(key)] = value;
        }
        
        public TValue Get(TKey key)
        {
            if (!keys.Contains(key))
            {
                Debug.LogError($"没有该key {key}");
                throw null!;
            }
            return values[keys.IndexOf(key)];
        }
        
        public void Add(TKey key,TValue value)
        {
            if (keys.Contains(key))
            {
                Debug.LogError($"已有该key {key}");
                return;
            }
            keys.Add(key);
            values.Add(value);
        }
        
        

        public void Remove(TKey key)
        {
            if (!keys.Contains(key))
            {
                Debug.LogError($"没有该key {key}");
                return;
            }
            values.RemoveAt(keys.IndexOf(key));
            keys.Remove(key);
        }
        
        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }
        
        public bool ContainsKey(TKey key)
        {
            return keys.Contains(key);
        }
    }
}
