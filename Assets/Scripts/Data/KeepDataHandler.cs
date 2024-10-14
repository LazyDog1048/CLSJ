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
        protected virtual int index { get; set; }
        public KeepDataHandler(int index)
        {
            dataId = typeof(T).Name;
            this.index = index;
            
            dataContainer = SaveLoadTool.LoadFromLocal<T>(index);
        }

        public virtual void SetToDefaultData()
        {
            SaveLoadTool.DeleteFromLocal<T>(index);
            dataContainer = SaveLoadTool.LoadFromLocal<T>(index);
        }

        public virtual void SaveData()
        {
            SaveLoadTool.SaveToLocal(dataContainer,index);
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
