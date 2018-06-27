using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client_TCP
{
    class Program
    {/*
        The purpose of this program is to act as a TCP client
        that connects to a TCP server. The client will be prompted
        to either enter a message or enter a number of bytes,
        (over 1500 to test data fragmentation), to send to the server. 
        The server will respond with confirmation if the data was received.
     */
        static void Main(string[] args)
        {
            int port = 11000;
            bool running = true;
            string option = "";
            string message = "";
            int bytes;
            byte[] frag;
            string responseData = "";
           

            TcpClient tcp = new TcpClient("host ip goes here", port);
            StreamReader reader = new StreamReader(tcp.GetStream());
            StreamWriter writer = new StreamWriter(tcp.GetStream());

            while(running)
            {
                try
                {
                    Console.WriteLine("To send a message, enter \"message\". To test fragmentation, enter \"frag\".");
                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "message":
                            Console.WriteLine("\nEnter a message to send to the server:\n");
                            message = Console.ReadLine();

                            writer.WriteLine(message);                          
                            writer.Flush();

                            responseData = reader.ReadLine();
                            Console.WriteLine("Server received the message: " + responseData + "\n");
                            break;

                        case "frag":
                            Console.WriteLine("\nEnter number of bytes to send. (Must be over 1500)");
                            bytes = Convert.ToInt32(Console.ReadLine());

                            frag = new byte[bytes];
                            for (int i = 0; i < bytes; i++)
                                frag[i] = 0x31;

                            writer.WriteLine(Encoding.ASCII.GetString(frag));
                            writer.Flush();

                            responseData = reader.ReadLine();
                            Console.WriteLine("Server received {0} bytes.\n", bytes);
                            break;

                        default:
                            Console.WriteLine("\nInvalid option.\n");
                            break;
                    }
 
                }

                catch(Exception)
                {
                    Console.WriteLine("\nUnable to send data. Server connection closed.");
                    Console.WriteLine("Press Enter to close.");
                    Console.ReadLine();
                    break;
                }
            }

            reader.Close();
            writer.Close();
            tcp.Close();
        }
    }
}
