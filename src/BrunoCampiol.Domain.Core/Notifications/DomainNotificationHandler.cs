using BrunoCampiol.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCampiol.Domain.Core.Notifications
{
    public class DomainNotificationHandler : IHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            ClearNotifications();
        }

        public void Handle(DomainNotification message)
        {
            if (!_notifications.Any(x => x.Message.Trim().ToUpper().Equals(message.Message.Trim().ToUpper())))
            {
                _notifications.Add(message);
            }
        }

        public virtual IEnumerable<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual string GetNotificationMessages()
        {
            return _notifications.Select(x => x.Message).Aggregate((current, next) => $"{current} : {next}");
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void ClearNotifications()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Dispose()
        {
            ClearNotifications();
            GC.SuppressFinalize(this);
        }
    }
}
