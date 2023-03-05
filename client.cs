using System;
using System.IO;
using System.Text.Json;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace UWPConsoleApp
{
    class DataExchangeClass
    {
        public string type { get; set; }
        public double[] data { get; set; }
    }
    class UWPConsoleApp
    {
        static public DataExchangeClass GetPosition( Random rd )
        {
            DataExchangeClass dec = new DataExchangeClass()
            {
                type = "position",
                data = new double[] { 
                    rd.NextDouble(),
                    rd.NextDouble(),
                    rd.NextDouble(),
                    rd.NextDouble(),
                    rd.NextDouble(),
                    rd.NextDouble()
                }
            };

            return dec;
        }

        static public void sendData( StreamSocket sk, string jsonEncoded, HostName hn, string portno )
        {
            Console.WriteLine("Connecting...");
            sk.ConnectAsync(hn, portno).AsTask().GetAwaiter().GetResult();
            var outsk = new StreamWriter(sk.OutputStream.AsStreamForWrite());

            Console.WriteLine($"Sending data...");
            outsk.Write(jsonEncoded);

            Console.WriteLine("Closing connection...");
            outsk.Dispose();
            sk.Dispose();
        }
        static void Main(string[] argv)
        {
            // random number generator
            Random rd = new Random();

            Console.Write("server IP address: ");
            string ipAddr = Console.ReadLine();

            Console.Write("server port number: ");
            string portno = Console.ReadLine();

            Console.Write("How many messages to send? ");
            var maxCount = int.Parse(Console.ReadLine());

            // socket
            var sk = new StreamSocket();
            
            var hn = new HostName(ipAddr);
            for ( int i=0; i<maxCount; ++i )
            {
                Console.WriteLine($"Generating position no.{i}...");
                DataExchangeClass dec = GetPosition(rd);
                string jsonEncoded = JsonSerializer.Serialize(dec);
                Console.WriteLine(jsonEncoded);

                sendData(sk, jsonEncoded, hn, portno);
            }

            Console.WriteLine("Closing service...");
            DataExchangeClass decDump = new DataExchangeClass()
            {
                type = "dump"
            };
            string jsonDump = JsonSerializer.Serialize(decDump);
            sendData(sk, jsonDump, hn, portno);

            Console.WriteLine("Done. Press a key to exit...");
            sk.Dispose();
            Console.ReadKey();
        }
    }
}