namespace BrunoCampiol.Domain.Core.Interfaces
{
    public interface IHandler<T> : IDisposable where T : IDomainEvent
    {
        void Handle(T args);
        bool HasNotifications();
        IEnumerable<T> GetNotifications();
        string GetNotificationMessages();
        void ClearNotifications();
    }
}
