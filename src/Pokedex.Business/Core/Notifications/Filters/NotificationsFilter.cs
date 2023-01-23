using Microsoft.AspNetCore.Mvc.Filters;

namespace Pokedex.Business.Core.Notifications.Filters
{
    public class NotificationsFilter : IActionFilter
    {
        private readonly INotifier _notifier;

        public NotificationsFilter(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!_notifier.HasNotification)
                return;

            context.Result = _notifier.GetAsJsonResult();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Not implemented.
        }
    }
}
