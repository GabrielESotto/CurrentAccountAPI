using CurrentAccount.Domain.Notifications;
using CurrentAccount.Domain.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CurrentAccount.Api.v1
{
    public class BaseController : ControllerBase
    {
        private readonly INotification _notification;

        public BaseController(INotification notification)
        {
            _notification = notification;
        }

        protected bool ValidOperation()
        {
            return !_notification.HasNotification();
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.GetNotifications().Select(e => e.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificateErrorInvalidModel(modelState);
            return CustomResponse();
        }

        protected void NotificateErrorInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificateError(errorMessage);
            }
        }

        protected void NotificateError(string errorMessage)
        {
            _notification.Handle(new Notification(errorMessage));
        }
    }
}
