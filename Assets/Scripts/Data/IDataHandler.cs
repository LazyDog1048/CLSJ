namespace data
{
    public interface IDataHandler<T> :IDataKeeper where T : new()
    {
        KeepDataHandler<T> DataHandler { get; }
        T Data { get; }
    }
    
    public interface IDataKeeper
    {
        void SaveData();
        void SetToDefaultData();
    }
}
