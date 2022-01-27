using BrunoCampiol.Domain.Core.Notifications;

namespace BrunoCampiol.Services.Api.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message returned to requester with error summary
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of detailed error messages
        /// </summary>
        public IEnumerable<DomainNotification> Details { get; set; }
    }
}
