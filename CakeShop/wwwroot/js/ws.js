var socket = new WebSocket("ws://localhost:11911"); // Replace with your server URL

socket.onopen = function () {
    console.log("WebSocket connection established.");
    // You can send an initial message to the server here if needed
    // Example: socket.send("Hello, server!");
};

socket.onmessage = function (event) {
    console.log("Received message from server:", event.data);
    // Handle the received message from the server
};

socket.onclose = function () {
    console.log("WebSocket connection closed.");
};

// Check the connection status every 5 seconds
setInterval(function () {
    switch (socket.readyState) {
        case WebSocket.CONNECTING:
            console.log("WebSocket connection is connecting...");
            break;
        case WebSocket.OPEN:
            console.log("WebSocket connection is open.");
            break;
        case WebSocket.CLOSING:
            console.log("WebSocket connection is closing...");
            break;
        case WebSocket.CLOSED:
            console.log("WebSocket connection is closed.");
            break;
        default:
            console.log("WebSocket connection status unknown.");
            break;
    }
}, 5000);