using BrunoCampiol.Domain.Core.Interfaces;

namespace BrunoCampiol.Domain.Core.Notifications
{
    public class DomainNotification : IDomainEvent
    {
        public string Message { get; }
        public string Type { get; }
        public DateTime OccurrenceDate { get; }

        public DomainNotification(string type, string message)
        {
            
            Message = message;
            Type = type;
            OccurrenceDate = DateTime.UtcNow;
        }

        public static DomainNotification Error(string message)
        {
            return new DomainNotification("Error", message);
        }

        public static DomainNotification ModelValidation(string message)
        {
            return new DomainNotification("ModelValidation", message);
        }
    }
}