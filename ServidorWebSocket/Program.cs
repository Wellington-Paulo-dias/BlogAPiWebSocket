using System.Net.WebSockets;
using System.Net;
using System.Text;

public class Program
{
    static async Task Main(string[] args)
    {
        HttpListener httpListener = new();
        httpListener.Prefixes.Add("http://localhost:5001/ws/");
        httpListener.Start();
        Console.WriteLine("Servidor WebSocket iniciado. --ws://localhost:5001/ws/--");

        while (true)
        {
            var httpContext = await httpListener.GetContextAsync();

            if (httpContext.Request.IsWebSocketRequest)
            {
                var webSocketContext = await httpContext.AcceptWebSocketAsync(null);
                var webSocket = webSocketContext.WebSocket;

                _ = Task.Run(() => HandleConnectionAsync(webSocket));
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.Close();
            }
        }
    }
    public const string MessageReceived = "Message received";
    private static async Task HandleConnectionAsync(WebSocket webSocket)
    {
        byte[] buffer = new byte[1024 * 4];

        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (webSocket.State == WebSocketState.Open)
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine(message);

                // Envia confirmação de volta para o remetente
                var getMessage = Encoding.UTF8.GetBytes(MessageReceived);
                await webSocket.SendAsync(new ArraySegment<byte>(getMessage), 
                    WebSocketMessageType.Text, true, CancellationToken.None);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                    "Closing", 
                    CancellationToken.None);
            }

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
    }
}