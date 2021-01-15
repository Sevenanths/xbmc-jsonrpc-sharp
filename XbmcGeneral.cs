// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcGeneral
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcGeneral : XbmcJsonRpcNamespace
  {
    private XbmcSystem system;

    public string BuildVersion
    {
      get
      {
        this.client.LogMessage("XbmcGeneral.BuildVersion");
        return this.system.GetInfoLabel("System.BuildVersion");
      }
    }

    public DateTime BuildDate
    {
      get
      {
        this.client.LogMessage("XbmcGeneral.BuildDate");
        object infoLabel = (object) this.system.GetInfoLabel("System.BuildDate");
        if (infoLabel != null)
          return DateTime.Parse((string) infoLabel);
        return DateTime.Now;
      }
    }

    internal XbmcGeneral(JsonRpcClient client)
      : base(client)
    {
      this.system = new XbmcSystem(client);
    }

    public int GetVolume()
    {
      this.client.LogMessage("XbmcGeneral.GetVolume()");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("properties", (object[]) new string[2]
      {
        "volume",
        "muted"
      }));
      JObject jobject2 = this.client.Call("Application.GetProperties", (object) jobject1) as JObject;
      if (jobject2 != null)
        return (int) jobject2["volume"];
      this.client.LogErrorMessage("Application.GetProperties(): Invalid response");
      return -1;
    }

    public int GetMuted()
    {
      this.client.LogMessage("XbmcGeneral.GetMuted()");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("properties", (object[]) new string[2]
      {
        "volume",
        "muted"
      }));
      JObject jobject2 = this.client.Call("Application.GetProperties", (object) jobject1) as JObject;
      if (jobject2 == null)
      {
        this.client.LogErrorMessage("Application.GetProperties(): Invalid response");
        return -1;
      }
      return !(bool) jobject2["muted"] ? 0 : 1;
    }

    public bool Log(string message)
    {
      this.client.LogMessage("XbmcGeneral.Log(message)");
      if (string.IsNullOrEmpty(message))
        throw new ArgumentException();
      return this.client.Call("XBMC.Log", (object) message) != null;
    }

    public bool Log(string message, XbmcLogLevel logLevel)
    {
      this.client.LogMessage("XbmcGeneral.Log(message, level)");
      if (string.IsNullOrEmpty(message))
        throw new ArgumentException();
      JObject jobject = new JObject();
      jobject.Add((object) new JProperty(nameof (message), (object) message));
      jobject.Add((object) new JProperty("level", (object) logLevel.ToString().ToLowerInvariant()));
      return this.client.Call("XBMC.Log", (object) jobject) != null;
    }

    public bool Quit()
    {
      this.client.LogMessage("XbmcGeneral.Quit()");
      return this.client.Call("XBMC.Quit") != null;
    }
  }
}
