using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

#if WINDOWS_UWP
using Windows.Networking;
using Windows.Networking.Sockets;
#endif


namespace Cineca
{
    public class DataExchangeClass
    {
        public string type { get; set; }
        public double[] data { get; set; }
    }

    public class CinecaClient : MonoBehaviour
    {
        // IP address of the server
        public string IpAddress = "";

        // Portno of the server
        public string PortNumber = "5000";

        // time to wait between one data transfer and another one
        public int WaitTimeInSeconds = 5;
        
        // continue sending data to the server
        private bool continueSendingData = false;

        // coroutine: send data to the server each tot seconds
        private Coroutine sendDataCoroutine = null;

        // disposed object?
        private bool disposed = false;

        // Output Text
        public TextMesh outputText = null;

        // generate a position to send to the server
        public DataExchangeClass GetPosition( )
        {
            Vector3 orientation = transform.rotation.eulerAngles;
            Vector3 position = transform.position;

            DataExchangeClass dec = new DataExchangeClass( )
            {
                type = "position",
                data = new double[] {
                    position.x,
                    position.y,
                    position.z,
                    orientation.x,
                    orientation.y,
                    orientation.z
                }
            };

            return dec;
        }

#if WINDOWS_UWP
        public void sendData(StreamSocket sk, string jsonEncoded, HostName hn, string portno)
        {
            Console.WriteLine("Connecting...");
            sk.ConnectAsync(hn, portno).AsTask().GetAwaiter().GetResult();
            var outsk = new StreamWriter(sk.OutputStream.AsStreamForWrite());

            Console.WriteLine($"Sending data...");
            outsk.Write(jsonEncoded);
            outsk.Flush();

            Console.WriteLine("Closing connection...");
            outsk.Dispose();
            sk.Dispose();
        }
#else
        public void sendData()
        {
            Debug.Log("Not in UWP");
        }
#endif

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // EVENT send data to the server
        public void StartSendingData()
        {
            if (disposed) return;

            continueSendingData = true;
            sendDataCoroutine = StartCoroutine(SendDataCycle());
        }

        // EVENT stop sending data to the server
        public void StopSendingData()
        {
            if (disposed) return;

            continueSendingData = false;
            disposed = true;
        }

        // write output to OutputText
        private void printOutputText( string msg )
        {
            Debug.Log(msg);
            if (outputText != null)
                outputText.text = msg; // overwrite
        }

        // send data in cycle
        private IEnumerator SendDataCycle()
        {
            printOutputText("SESSION START: waiting to start...");
            yield return new WaitForSeconds(WaitTimeInSeconds);

#if WINDOWS_UWP
            var sk = new StreamSocket();
            var hn = new HostName(IpAddress);           
#endif
            int i = 0;
            while (continueSendingData)
            {

                DataExchangeClass dec = GetPosition();
                string jsonEncoded = JsonSerializer.Serialize(dec);

#if WINDOWS_UWP
                sendData(sk, jsonEncoded, hn, PortNumber);
#else
                Debug.Log($"Sending data (not in UWP)... {jsonEncoded}");
#endif
                ++i;
                printOutputText($"Sent position no. [{i}] ; waiting...");
                yield return new WaitForSeconds(WaitTimeInSeconds);
            }

            printOutputText("STOP SESSION : sending dump...");
            DataExchangeClass decDump = new DataExchangeClass()
            {
                type = "dump"
            };
            string jsonDump = JsonSerializer.Serialize(decDump);

#if WINDOWS_UWP
            var skdp = new StreamSocket();
            sendData(skdp, jsonDump, hn, PortNumber);
            skdp.Dispose();
#else
            Debug.Log($"Sending dump command (not in UWP)... {jsonDump}");
#endif
            printOutputText("Done!");
        }
    }
}
