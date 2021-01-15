// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcJsonRpc
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcJsonRpc : XbmcJsonRpcNamespace
  {
    internal XbmcJsonRpc(JsonRpcClient client)
      : base(client)
    {
    }

    public ICollection<XbmcJsonRpcMethod> Introspect()
    {
      return this.Introspect(true, true, true);
    }

    public ICollection<XbmcJsonRpcMethod> Introspect(
      bool getPermissions,
      bool getDescriptions,
      bool filterByTransport)
    {
      this.client.LogMessage("XbmcJsonRpc.Introspect()");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("getpermissions", (object) getPermissions));
      jobject1.Add((object) new JProperty(nameof (getDescriptions), (object) getDescriptions));
      jobject1.Add((object) new JProperty("filterbytransport", (object) filterByTransport));
      JObject jobject2 = this.client.Call("JSONRPC.Introspect", (object) jobject1) as JObject;
      List<XbmcJsonRpcMethod> xbmcJsonRpcMethodList = new List<XbmcJsonRpcMethod>();
      if (jobject2 == null || jobject2["commands"] == null)
      {
        this.client.LogErrorMessage("JSONRPC.Introspect: Invalid response");
        return (ICollection<XbmcJsonRpcMethod>) xbmcJsonRpcMethodList;
      }
      foreach (JObject jobject3 in (IEnumerable<JToken>) jobject2["commands"])
        xbmcJsonRpcMethodList.Add(XbmcJsonRpcMethod.FromJson(jobject3));
      return (ICollection<XbmcJsonRpcMethod>) xbmcJsonRpcMethodList;
    }

    public int Version()
    {
      this.client.LogMessage("XbmcJsonRpc.Version()");
      JObject jobject = this.client.Call("JSONRPC.Version") as JObject;
      if (jobject != null && jobject["version"] != null)
        return (int) jobject["version"][(object) "major"];
      this.client.LogErrorMessage("JSONRPC.Version: Invalid response");
      return -1;
    }

    public ICollection<string> Permission()
    {
      this.client.LogMessage("XbmcJsonRpc.Permission()");
      JObject jobject = this.client.Call("JSONRPC.Permission") as JObject;
      List<string> stringList = new List<string>();
      if (jobject["permission"] == null)
      {
        this.client.LogErrorMessage("JSONRPC.Permission: Invalid response");
        return (ICollection<string>) stringList;
      }
      foreach (JToken jtoken in (IEnumerable<JToken>) jobject["permission"])
      {
        string str = (string) jtoken;
        stringList.Add(str);
      }
      return (ICollection<string>) stringList;
    }

    public string Ping()
    {
      this.client.LogMessage("XbmcJsonRpc.Ping()");
      object obj = this.client.Call("JSONRPC.Ping");
      if (obj != null)
        return obj.ToString();
      this.client.LogErrorMessage("JSONRPC.Ping: Invalid response");
      return string.Empty;
    }

    public void Announce(string sender, string message)
    {
      this.Announce(sender, message, (object) null);
    }

    public void Announce(string sender, string message, object data)
    {
      this.client.LogMessage("XbmcJsonRpc.Announce()");
      JObject jobject = new JObject();
      jobject.Add((object) new JProperty(nameof (sender), (object) sender));
      jobject.Add((object) new JProperty(nameof (message), (object) message));
      if (data != null)
        jobject.Add((object) new JProperty(nameof (data), (object) message));
      this.client.Call("JSONRPC.NotifyAll", (object) jobject);
    }
  }
}
