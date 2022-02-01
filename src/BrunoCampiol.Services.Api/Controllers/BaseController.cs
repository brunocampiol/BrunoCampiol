using BrunoCampiol.Domain.Core.Interfaces;
using BrunoCampiol.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrunoCampiol.Services.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IHandler<DomainNotification> Notifications { get; }

        public BaseController(IHandler<DomainNotification> notifications)
        {
            Notifications = notifications;
        }

        protected bool IsValidOperation() => !Notifications.HasNotifications();

        protected void NotifyError(string message)
        {
            Notifications.Handle(DomainNotification.Error(message));
        }

        protected ActionResult ResponseApi(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);
            foreach (var error in errors) NotifyError(error.ErrorMessage);
            return ResponseApi();
        }

        protected ActionResult ResponseApi(object result = null)
        {
            if (IsValidOperation()) return Ok(result);

            var errorDetails = new ValidationProblemDetails();
            errorDetails.Type = "Error";
            errorDetails.Title = "Please check the errors.";
            errorDetails.Errors.Add("Title", Notifications.GetNotifications().Select(x => x.Message).ToArray());
            ValidationProblem(errorDetails);
            
            return BadRequest(errorDetails);
        }
    }
}
