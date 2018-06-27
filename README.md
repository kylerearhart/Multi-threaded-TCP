# Multi-threaded-TCP
This repo will include two applications written in C# that demonstrate a multi-threaded TCP server that can handle multiple client requests.

**Client ("Talker")**
>The client application will give users the option of sending a message to the server or declaring a certain number of bytes to send in order to test data fragmentation over the network. (Must be over 1500 bytes to observe fragmentation.)

**Server ("Listener")**
>The server application will be constantly listening for any client requests. Once the client(s) are connected, the server will accept the data sent by the client, either a message or a specific number of bytes. It will display whatever data was sent to the console and send a confirmation back to the client that the data was received.

*Side Note: *
In oder to observe data fragmentation, must run programs while running a network protocol analyzer, such as WireShark.
