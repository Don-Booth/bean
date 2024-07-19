using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Bean.Core.Common
{
    class HeartRate
    {
        
        //System.Threading.CancellationToken cts = new System.Threading.CancellationToken();

        internal static async Task Connect()
        {
            try
            {
                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                using (ClientWebSocket Client = new ClientWebSocket())
                {
                    //Client.Options.AddSubProtocol("Tls12");                    
                    Uri serverURI = new Uri(Bean.Data.General.HeartRateWebSocketURI);

                    await Client.ConnectAsync(serverURI, CancellationToken.None);

                    while (true)
                    {
                        if (Client.State == WebSocketState.Open)
                        {
                            Console.WriteLine($"[WS][connect]: Connected");
                            ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                            WebSocketReceiveResult result = await Client.ReceiveAsync(bytesReceived, CancellationToken.None);
                            //Console.WriteLine($"{Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count)}");

                            JObject root = JObject.Parse(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));

                            foreach (KeyValuePair<String, JToken> app in root)
                            {
                                var appName = app.Key;

                                switch(appName)
                                {
                                    case "data":
                                        {
                                            var heartratevalue = (String)app.Value["heartRate"];
                                            //Console.WriteLine(heartratevalue);
                                            int intResult = int.Parse(heartratevalue.ToString());
                                            break;
                                        }
                                    case "timestamp":
                                        {
                                            var timestamp = (string)app.Value;
                                            //Console.WriteLine(timestamp);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[WS][disconnect]: Not Connected");
                            break;
                        }
                    }
                }
            }
            catch (WebSocketException wex)
            {
                Console.WriteLine($"HeartRate Error] {wex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HeartRate Error] TYPE: {ex.GetType().Name} - MESSAGE: {ex.Message}");
            }
        }

        //public async Task HeartRateConnect()
        //{
        //    using (var cws = new ClientWebSocket())
        //    {

        //    }
        //}

        //internal async void HeartRateConnect()
        //{
        //    try
        //    {
        //        using (ClientWebSocket ws = new ClientWebSocket())
        //        {
        //            try
        //            {
        //                //Uri serverUri = new Uri("wss://ramiel2.pulsoid.net/listen/1512d11b-b9aa-4080-8b7a-3b61387c4248");
        //                Uri serverUri = new Uri("wss://echo.websocket.org");
        //                await ws.ConnectAsync(serverUri, CancellationToken.None);
        //                while (ws.State == WebSocketState.Open)
        //                {
        //                    //Console.Write("Input message ('exit' to exit): ");
        //                    //string msg = Console.ReadLine();
        //                    //if (msg == "exit")
        //                    //{
        //                    //    break;
        //                    //}

        //                    //ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
        //                    //await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        //                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
        //                    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
        //                    Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
        //                }
        //            }
        //            catch (Exception ex2)
        //            {
        //                Console.WriteLine($"HeartRate Exception2] {ex2.Message}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"HeartRate Exception] {ex.Message}");
        //    }
        //}

        #region OLD CODE
        //public async void HeartRateConnect()
        //{
        //    try
        //    {
        //        int intResult = 0;

        //        //using (var ws = new WebSocket("wss://ramiel2.pulsoid.net/listen/1512d11b-b9aa-4080-8b7a-3b61387c4248"))
        //        using (var ws = new WebSocket("wss://echo.websocket.org"))
        //        {
        //            ws.Log.Output = (data, s) => Ws_OnError_Override(data, s); // Send all logging data to an override method so we can handle Fatal errors (WTF IS THIS NECESSARY FOR??)
        //            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12; // Set SSL protocol
        //            //ws.SslConfiguration.ServerCertificateValidationCallback += System.Security.Authentication.da
        //            ws.EmitOnPing = true; // Show ping messages
        //            ws.Log.Level = LogLevel.Debug; // Set logging level
        //            //ws.Log.Output = (data, s) => { Debug.WriteLine(data); }; // Force debug data into consoel only.
        //            //ws.Log.Output = (_, __) => { }; // Disable logging completely

        //            ws.OnOpen += Ws_OnOpen;
        //            ws.OnMessage += Ws_OnMessage;
        //            ws.OnError += Ws_OnError; // This will likely never be called due to the Log.Output setting above but we leave it here just in case.

        //            //ws.Connect();
        //            ws.ConnectAsync();

        //            //Console.ReadKey(true);

        //            if (intResult > 0)
        //            {
        //                ws.CloseAsync();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"HeartRate Error: {ex.Message} = {ex.InnerException}");
        //    }
        //}

        //private static void Ws_OnError_Override(LogData data, string s)
        //{
        //    Console.WriteLine($"Heartbeat Override: {data.Date}] {data.Level}|{data.Message}");
        //}

        //private static void Ws_OnError(object sender, ErrorEventArgs e)
        //{
        //    if (e.Exception == null)
        //    {
        //        Console.WriteLine($"Heartbeat Failure: {e.Message}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Heartbeat Failure: { e.Exception}");
        //    }
        //}

        //private static void Ws_OnMessage(object sender, MessageEventArgs e)
        //{
        //    //int intResult = 0;

        //    try
        //    {

        //        if (e.IsText)
        //        {
        //            Console.WriteLine($"Heartbeat says: {e.Data}");
        //            //var objects = JRaw.Parse(e.Data);
        //            //var objects = JArray.Parse(e.Data);
        //            JObject root = JObject.Parse(e.Data);

        //            //foreach (JObject root in objects)
        //            //{
        //            foreach (KeyValuePair<String, JToken> app in root)
        //            {
        //                var appName = app.Key;
        //                Console.WriteLine(appName);

        //                if (appName == "data")
        //                {
        //                    var heartratevalue = (String)app.Value["heartRate"];
        //                    //var value = (String)app.Value["Value"];
        //                    Console.WriteLine(heartratevalue);
        //                    Console.WriteLine("\n");
        //                    int intResult = int.Parse(heartratevalue.ToString());
        //                    break;
        //                }
        //            }
        //            //}
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Return data was not Text.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Heartbeat processing error] {ex.Message}");
        //    }

        //    //intResult = int.Parse(e.Data);

        //    //await Task.Delay(5000);
        //}

        //private static void Ws_OnOpen(object sender, EventArgs e)
        //{
        //    Console.WriteLine($"Heartbeat websocket open");
        //}
        #endregion
    }
}