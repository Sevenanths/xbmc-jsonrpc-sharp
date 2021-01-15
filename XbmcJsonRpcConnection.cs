// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcJsonRpcConnection
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Text;
using Coe.WebSocketWrapper;
using System.Net.WebSockets;

namespace XBMC.JsonRpc
{
    public class XbmcJsonRpcConnection : IDisposable
    {
        private const int AnnouncementPort = 9090;
        private const string AnnouncementEnd = "}}";
        private const string AnnouncementEndAlternative = "}\n}\n";
        private const string AnnouncementSender = "xbmc";
        private const string PingResponse = "pong";
        private bool disposed;
        private JsonRpcClient client;
        private Socket socket;
        private WebSocketWrapper websocket;
        private XbmcJsonRpc jsonRpc;
        private XbmcPlayer player;
        private XbmcSystem system;
        private XbmcGeneral xbmc;
        private XbmcFiles files;
        private XbmcPlaylist playlist;
        private XbmcLibrary library;
        private string address;
        private int port;
        private string username;
        private string password;

        public bool IsAlive
        {
            get
            {
                this.client.LogMessage("XbmcJsonRpcConnection.IsAlive");
                try
                {
                    if (this.socket != null)
                    {
                        if (this.socket.Connected)
                            goto label_5;
                    }
                    this.client.LogMessage("Result: Not alive (old system)");
                    return false;
                }
                catch (Exception ex)
                {
                    this.client.LogErrorMessage("Could not determine the state of the TCP socket", ex);
                    return false;
                }
            label_5:
                string strA = this.jsonRpc.Ping();
                if (string.IsNullOrEmpty(strA) || string.CompareOrdinal(strA, "pong") != 0)
                {
                    this.client.LogMessage("Result: Not alive (old system PONG)");
                    return false;
                }
                this.client.LogMessage("Result: Alive (old system)");
                return true;
            }
        }

        public bool IsAliveNew
        {
            get
            {
                this.client.LogMessage("XbmcJsonRpcConnection.IsAlive (new)");
                try
                {
                    if (this.websocket != null)
                    {
                        this.client.LogMessage("Websocket is initialised");
                        if (this.websocket._ws.State == WebSocketState.Open)
                        {
                            this.client.LogMessage("Opening a connection to XBMC");
                            goto label_5;
                        }
                    }

                    this.client.LogMessage("Result: Not alive (websocket is not open)");
                    return false;
                }
                catch (Exception ex)
                {
                    this.client.LogErrorMessage("Could not determine the state of the TCP socket", ex);
                    return false;
                }
            label_5:
                string strA = this.jsonRpc.Ping();
                if (string.IsNullOrEmpty(strA) || string.CompareOrdinal(strA, "pong") != 0)
                {
                    this.client.LogMessage("Result: Not alive (new system)");
                    return false;
                }
                this.client.LogMessage("Result: Alive");
                return true;
            }
        }
        public XbmcJsonRpc JsonRpc
        {
            get
            {
                return this.jsonRpc;
            }
        }

        public XbmcPlayer Player
        {
            get
            {
                return this.player;
            }
        }

        public XbmcSystem System
        {
            get
            {
                return this.system;
            }
        }

        public XbmcGeneral Xbmc
        {
            get
            {
                return this.xbmc;
            }
        }

        public XbmcFiles Files
        {
            get
            {
                return this.files;
            }
        }

        public XbmcPlaylist Playlist
        {
            get
            {
                return this.playlist;
            }
        }

        public XbmcLibrary Library
        {
            get
            {
                return this.library;
            }
        }

        public event EventHandler Connected;

        public event EventHandler Aborted;

        public event EventHandler<XbmcJsonRpcLogEventArgs> Log;

        public event EventHandler<XbmcJsonRpcLogErrorEventArgs> LogError;

        public XbmcJsonRpcConnection(Uri uri)
          : this(uri, (string)null, (string)null)
        {
        }

        public XbmcJsonRpcConnection(Uri uri, string username, string password)
        {
            this.client = new JsonRpcClient(uri, username, password);
            this.client.Log += new EventHandler<XbmcJsonRpcLogEventArgs>(this.onLog);
            this.client.LogError += new EventHandler<XbmcJsonRpcLogErrorEventArgs>(this.onLogError);
            this.jsonRpc = new XbmcJsonRpc(this.client);
            this.player = new XbmcPlayer(this.client);
            this.system = new XbmcSystem(this.client);
            this.xbmc = new XbmcGeneral(this.client);
            this.files = new XbmcFiles(this.client);
            this.playlist = new XbmcPlaylist(this.client);
            this.library = new XbmcLibrary(this.client);
        }

        public XbmcJsonRpcConnection(string address, int port)
          : this(address, port, (string)null, (string)null)
        {
        }

        public XbmcJsonRpcConnection(string address, int port, string username, string password)
          : this(new Uri("http://" + address + ":" + (object)port + "/jsonrpc"), username, password)
        {
            this.address = address;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        public bool Open()
        {
            return this.Open(9090);
        }

        public bool Open(int jsonPort)
        {
            this.client.LogMessage("Opening a connection to XBMC");
            try
            {
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.socket.Connect(this.client.Uri.Host, jsonPort);
                if (!this.IsAlive)
                {
                    this.Close();
                    return false;
                }
                this.openWebSocket();
                this.onConnected();
                this.receive(new SocketStateObject());
            }
            catch (Exception ex)
            {
                this.client.LogErrorMessage("Could not open a connection to XBMC", ex);
                return false;
            }

            return true;
        }

        public void openWebSocket()
        {
            this.client.LogMessage("Opening a connection: ws://" + this.username + ":" + this.password + "@" + this.address + ":9090/jsonrpc");
            this.websocket = WebSocketWrapper.Create("ws://" + this.username + ":" + this.password + "@" + this.address + ":9090/jsonrpc");
            this.websocket.OnMessage((string message, WebSocketWrapper wrapper) => { this.onAnnouncement(message); });
            this.websocket.Connect();
        }

        public bool OpenNew(int jsonPort)
        {
            this.client.LogMessage("Opening a connection to XBMC (websocket)");

            try
            {
                this.openWebSocket();

                if (!this.IsAliveNew)
                {
                    this.Close();
                    return false;
                }

                this.onConnected();
                this.receiveNew(new SocketStateObject());
            }
            catch (Exception ex)
            {
                this.client.LogErrorMessage("Could not open a connection to XBMC", ex);
                return false;
            }

            return true;
        }

        public void Close()
        {
            this.client.LogMessage("Closing the connection");
            try
            {
                if (this.socket == null || !this.socket.Connected)
                    return;
                this.socket.Disconnect(false);
            }
            catch (Exception ex)
            {
                this.client.LogErrorMessage("Could not disconnect from the TCP socket", ex);
            }
        }

        public void Dispose()
        {
            if (this.disposed)
                return;
            try
            {
                lock (this.socket)
                {
                    this.Close();
                    this.socket.Close();
                }
            }
            catch (Exception ex)
            {
                this.client.LogErrorMessage("Could not close the TCP socket", ex);
            }
            finally
            {
                this.disposed = true;
            }
            GC.SuppressFinalize((object)this);
        }

        private void onConnected()
        {
            if (this.Connected == null)
                return;
            this.Connected((object)this, (EventArgs)null);
        }

        private void onAborted()
        {
            if (this.Aborted == null)
                return;
            this.Aborted((object)this, (EventArgs)null);
        }

        private void onAnnouncement(string data)
        {
            this.client.LogMessage("JSON RPC Notification received OVER WEBSOCKET: " + data);

            JObject jobject1 = (JObject)null;
            try
            {
                jobject1 = JObject.Parse(data);
            }
            catch (Exception ex)
            {
            }
            if (jobject1 == null)
                return;
            JObject jobject2 = jobject1["params"] as JObject;
            if (jobject1["method"] == null || jobject2 == null || (jobject2["sender"] == null || string.CompareOrdinal((string)jobject2["sender"], "xbmc") != 0))
            {
                this.client.LogErrorMessage("Wrong format of announcement");
            }
            else
            {
                string strA = (string)jobject1["method"];
                if (string.CompareOrdinal(strA, "Player.OnPlay") == 0)
                    this.player.OnPlaybackStarted();
                else if (string.CompareOrdinal(strA, "Player.OnPause") == 0)
                    this.player.OnPlaybackPaused();
                else if (string.CompareOrdinal(strA, "Player.OnResume") == 0)
                    this.player.OnPlaybackResumed();
                else if (string.CompareOrdinal(strA, "Player.OnStop") == 0)
                    this.player.OnPlaybackStopped();
                else if (string.CompareOrdinal(strA, "Player.OnSeek") == 0)
                    this.player.OnPlaybackSeek();
                else if (string.CompareOrdinal(strA, "Player.OnSpeedChanged") == 0)
                    this.player.OnPlaybackSpeedChanged();
                else if (string.CompareOrdinal(strA, "System.OnQuit") == 0)
                {
                    this.Close();
                    this.onAborted();
                }
                else if (string.CompareOrdinal(strA, "System.OnSleep") == 0)
                {
                    this.Close();
                    this.system.OnSleep();
                }
                else if (string.CompareOrdinal(strA, "System.OnWake") == 0)
                    this.system.OnWake();
                else if (string.CompareOrdinal(strA, "System.OnLowBattery") == 0)
                {
                    this.system.OnLowBattery();
                }
                else
                {
                    if (string.CompareOrdinal(strA, "VideoLibrary.OnUpdate") == 0 || string.CompareOrdinal(strA, "VideoLibrary.OnRemove") == 0 || string.CompareOrdinal(strA, "GUI.OnScreensaverActivated") == 0)
                        return;
                    string.CompareOrdinal(strA, "GUI.OnScreensaverDeactivated");
                }
            }
        }

        private void receiveAnnouncements(IAsyncResult result)
        {
            if (this.disposed)
                return;
            lock (this.socket)
            {
                SocketStateObject asyncState = result.AsyncState as SocketStateObject;
                if (asyncState == null || this.socket == null || !this.socket.Connected)
                    return;
                int count = 0;
                try
                {
                    count = this.socket.EndReceive(result);
                }
                catch (Exception ex)
                {
                    this.client.LogErrorMessage("Could not read the TCP socket", ex);
                    this.Close();
                    this.onAborted();
                }
                if (count > 0)
                {
                    asyncState.Builder.Append(Encoding.UTF8.GetString(asyncState.Buffer, 0, count));
                    this.receive(asyncState);
                }
                string str = asyncState.Builder.ToString();
                if (str.Length > 0 && str.Contains("}}") || str.Contains("}\n}\n"))
                {
                    this.client.LogMessage("JSON RPC Notification received: " + str);
                    int num = str.LastIndexOf("}}");
                    int length = num >= 0 ? num + "}}".Length : str.LastIndexOf("}\n}\n") + "}\n}\n".Length;
                    asyncState.Builder.Remove(0, length);
                    this.onAnnouncement(str.Substring(0, length));
                }
                this.receive(asyncState);
            }
        }

        private void receive(SocketStateObject state)
        {
            if (state == null || this.socket == null)
                return;
            if (!this.socket.Connected)
                return;
            try
            {
                this.socket.BeginReceive(state.Buffer, 0, 1024, SocketFlags.None, new AsyncCallback(this.receiveAnnouncements), (object)state);
            }
            catch (Exception ex)
            {
                this.client.LogErrorMessage("Could not start receiving from the TCP socket", ex);
                this.Close();
                this.onAborted();
            }
        }

        private void receiveNew(SocketStateObject state)
        {
            if (state == null || this.websocket == null)
                return;
            if (this.websocket._ws.State != WebSocketState.Open)
                return;

            this.client.LogMessage("I guess we're fetching JSON commands now lol");
        }

        private void onLog(object sender, XbmcJsonRpcLogEventArgs e)
        {
            if (this.Log == null)
                return;
            this.Log((object)this, e);
        }

        private void onLogError(object sender, XbmcJsonRpcLogErrorEventArgs e)
        {
            if (this.LogError == null)
                return;
            this.LogError((object)this, e);
        }
    }
}
