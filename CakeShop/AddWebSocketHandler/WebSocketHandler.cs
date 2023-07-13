using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace CakeShop.AddWebSocketHandler
{
    public class WebSocketHandler
    {
        private WebSocket _webSocket;

        public async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            _webSocket = webSocket;

            await ReceiveMessage();
        }

        private async Task ReceiveMessage()
        {
            var buffer = new byte[1024];
            WebSocketReceiveResult result;

            try
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        // Handle the received message
                        await HandleReceivedMessage(message);
                    }
                }
            }
            catch (WebSocketException ex)
            {
                // Handle WebSocket exception (e.g., connection closed unexpectedly)
                Console.WriteLine($"WebSocket exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task HandleReceivedMessage(string message)
        {
            // Handle the received message here or perform any desired logic
            // For example, you can send a response back to the client

            var responseMessage = $"Server received message: {message}";
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);

            try
            {
                await _webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (WebSocketException ex)
            {
                // Handle WebSocket exception (e.g., failed to send response)
                Console.WriteLine($"WebSocket exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
