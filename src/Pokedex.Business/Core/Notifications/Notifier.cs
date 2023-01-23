using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Pokedex.Business.Core.Notifications
{
    public class Notifier : INotifier
    {
        private readonly ILogger<Notifier> _logger;
        private readonly List<Notification> _notifications = new();

        public bool HasNotification => _notifications.Any();

        public Notifier(ILogger<Notifier> logger)
        {
                _logger= logger;
        }

        public IReadOnlyCollection<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Notify(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            _logger.LogInformation("Notified: {message}", message);
            _notifications.Add(new Notification(message));
        }

        public void Notify(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors.Select(e => e.ErrorMessage))
            {
                Notify(error);
            }
        }

        public JsonResult GetAsJsonResult()
        {
            return new JsonResult(GetNotifications())
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };
        }
    }
}
