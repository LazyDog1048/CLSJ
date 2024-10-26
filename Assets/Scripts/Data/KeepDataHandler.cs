using System;
using tool;

namespace data
{
    public class KeepDataHandler<T>:IDisposable,IDataHandler<T> where T : new()
    {
        public KeepDataHandler<T> DataHandler => this;
        public T Data => dataContainer;
        protected T dataContainer;
        // public DataSettings dataSettings { get; private set; }
        public readonly string dataId;
        public KeepDataHandler()
        {
            dataId = typeof(T).Name;
            dataContainer = new T();
        }

        public virtual void SetToDefaultData()
        {
        }

        public virtual void SaveData()
        {
            // SaveLoadTool.SaveToLocal(dataContainer,index);
        }

        public void SetData(T data)
        {
            dataContainer = data;
        }
        
        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }
        protected virtual void OnDispose()
        {
            
        }
        
    }
}
