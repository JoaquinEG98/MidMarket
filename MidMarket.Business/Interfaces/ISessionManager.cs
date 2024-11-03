namespace MidMarket.Business.Interfaces
{
    public interface ISessionManager
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
        void AbandonSession();
        bool IsObserverSubscribed();
        void ObserverSubscribe();
    }
}
