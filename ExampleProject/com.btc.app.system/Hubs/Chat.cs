using Microsoft.AspNetCore.SignalR;

namespace com.btc.app.system.Hubs
{
    public class Chat:Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
