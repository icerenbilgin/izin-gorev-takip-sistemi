using Microsoft.AspNetCore.SignalR;
using api.Hubs;

namespace api.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext?.Request.Query["userId"].ToString();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                await Groups.AddToGroupAsync(
                    Context.ConnectionId,
                    userId
                );
            }

            await base.OnConnectedAsync();
        }
    }
}
