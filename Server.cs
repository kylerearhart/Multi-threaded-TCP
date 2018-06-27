using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Server_TCP
{
    class Program
    {/*
        The purpose of this program is to act as a TCP server
        in which multiple TCP clients can connect. It utilizes
        multithreading to process each client request. Once a client
        connects, the client can send a message to the server or send
        over 1500 bytes to test data fragmentation. The server
        will respond to the client confirming the message was received.
     */
        static void Main(string[] args)
        {
            int port = 11000;
            bool running = true;
            TcpListener listen;
            TcpClient tcp;
            int count = 0;

            listen = new TcpListener(IPAddress.Any, port);
            listen.Start();
            Console.WriteLine("MULTITHREADED SERVER STARTED");

            while(running)
            {
                Console.Write("\nWaiting for client connections...");
                tcp = listen.AcceptTcpClient();
                count++;
                Console.WriteLine("Client {0} connected at " + DateTime.Now.ToString("h:mm:ss tt"), count);
                Thread t = new Thread(() => processClient(tcp, count));
                t.Start();
            }
        }

        private static void processClient(object argument, int count)
        {// method to handle separate client requests

            TcpClient tcp = (TcpClient)argument;
            StreamReader reader = new StreamReader(tcp.GetStream());
            StreamWriter writer = new StreamWriter(tcp.GetStream());
            string receiveData = "";
            bool running = true;

            
            while (running)
            {
                try
                {
                    receiveData = reader.ReadLine();

                    if (receiveData != "")
                    {
                        Console.WriteLine("\n\nFrom client {0}: {1}", count, receiveData);

                        Console.WriteLine("Sending confirmation to client {0} that the message was received.\n", count);
                        writer.WriteLine(receiveData);
                        writer.Flush();
                    }
                }

                catch (Exception)
                {
                    Console.Write("Connection Interrupted...");
                    break;
                }
            }

            reader.Close();
            writer.Close();
            tcp.Close();
            Console.WriteLine("Client {0} connection has been closed.", count);
        }
    }
}
