using BrunoCampiol.CrossCutting.Common.Common;
using BrunoCampiol.Domain.Core.Interfaces;
using BrunoCampiol.Domain.Core.Notifications;
using BrunoCampiol.Services.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        protected Task<dynamic> ResponseApiAsync(Func<Task<dynamic>> action) => ExecuteAsync(async () => ResponseApi(await action()));

        protected ActionResult ResponseApi(Func<dynamic> action) => Execute(() => ResponseApi(action()));

        protected void NotifyModelStateErrors()
        {
            var result = from ms in ModelState
                         where ms.Value.Errors.Any()
                         let fieldKey = ms.Key
                         let errors = ms.Value.Errors
                         from error in errors
                         select new
                         {
                             code = fieldKey.Replace("ENTITY.", "", StringComparison.InvariantCultureIgnoreCase)
                            .Replace("VIEWMODEL.", "", StringComparison.InvariantCultureIgnoreCase)
                            .Replace("MODEL.", "", StringComparison.InvariantCultureIgnoreCase)
                            .Replace("$.", "", StringComparison.InvariantCultureIgnoreCase),
                             message = error.Exception == null ? error.ErrorMessage : error.Exception.Message
                         };

            result.ToList().ForEach(x =>
            {
                Notifications.Handle(DomainNotification.ModelValidation($"{x.code}: {x.message}"));
            });
        }

        protected void NotifyError(string message)
        {
            Notifications.Handle(DomainNotification.Error(message));
        }

        private async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            return await action();

            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    ex.AllExceptionMessages().ForEach(x =>
            //    {
            //        NotifyError("", x);
            //    });

            //    return ResponseApi();
            //}
        }

        private T Execute<T>(Func<T> action)
        {
            return action();

            //try
            //{
                
            //}
            //catch (Exception ex)
            //{

            //    ex.GetErrorList().ForEach(x =>
            //    {
            //        NotifyError("", x);
            //    });

            //    return ResponseApi();
            //}
        }

        protected Task<dynamic> ResponseApiAsync(Func<Task> action)
            => ExecuteAsync(async () =>
            {
                await action();
                return ResponseApi();
            });

        protected dynamic ResponseApi(dynamic result = null, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (!IsValidOperation())
            {
                var errorApi = new ErrorResponse()
                {
                    Message = "We were unable to process your request. Please check the errors.",
                    Details = Notifications.GetNotifications()
                };

                return BadRequest(errorApi);
            }

            return Ok(result);
        }
    }
}
