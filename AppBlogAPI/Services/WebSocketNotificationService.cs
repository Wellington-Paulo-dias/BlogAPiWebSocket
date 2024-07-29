using System.Net.WebSockets;
using System.Text;

namespace AppBlogAPI.Services
{
    public class WebSocketNotificationService(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;
        private const string _messageReceived = "Message received";
        private const string _server = "ws://localhost:5001/ws/";

        public async Task SendMessageAsync(string message)
        {
            try
            {
                using var clientWebSocket = new ClientWebSocket();

                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                await clientWebSocket.ConnectAsync(new Uri(_server), cts.Token);

                var buffer = Encoding.UTF8.GetBytes(message);
                var segment = new ArraySegment<byte>(buffer);

                await clientWebSocket.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token);
                                
                buffer = new byte[1024];
                var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
                string acknowledgmentMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                if (acknowledgmentMessage == _messageReceived)
                {
                    Console.WriteLine("Notificação enviada e confirmada pelo servidor.");
                }

                await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", cts.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
