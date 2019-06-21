using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.DesktopClient
{
    public class ChatService : IChatService
    {
        public async void Connect()
        {
            var connection = new HubConnection("http://127.0.0.1:8090/");
            var chatHub = connection.CreateHubProxy("ChatHub");

            await connection.Start()
                .ConfigureAwait(false);
        }

        public void Send(string userName, string message)
        {

        }

        public void Disconnect()
        {

        }
    }
}
