using MidMarket.Business.Interfaces;
using System.Web;
using System.Web.SessionState;

namespace MidMarket.Business
{
    public class SessionManager : ISessionManager
    {
        private const string clienteLanguageKey = "ClienteIdioma";

        private HttpSessionState Session => HttpContext.Current?.Session;

        public T Get<T>(string key)
        {
            return (T)Session[key];
        }

        public void Set<T>(string key, T value)
        {
            Session[key] = value;
        }

        public void Remove(string key)
        {
            Session.Remove(key);
        }

        public void AbandonSession()
        {
            Session.Abandon();
        }

        public bool IsObserverSubscribed()
        {
            return Session[clienteLanguageKey] != null && (bool)Session[clienteLanguageKey];
        }

        public void ObserverSubscribe()
        {
            Session[clienteLanguageKey] = true;
        }
    }
}
