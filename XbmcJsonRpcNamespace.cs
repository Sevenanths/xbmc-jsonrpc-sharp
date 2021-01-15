// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcJsonRpcNamespace
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcJsonRpcNamespace
  {
    protected JsonRpcClient client;

    protected XbmcJsonRpcNamespace(JsonRpcClient client)
    {
      if (client == null)
        throw new ArgumentNullException(nameof (client));
      this.client = client;
    }

    protected TType getInfo<TType>(string label)
    {
      return this.getInfo<TType>(label, default (TType));
    }

    protected TType getInfo<TType>(string label, TType defaultValue)
    {
      this.client.LogMessage("XBMC.GetInfoLabels(" + label + ")");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("labels", (object[]) new string[1]
      {
        label
      }));
      JObject jobject2 = this.client.Call("XBMC.GetInfoLabels", (object) jobject1) as JObject;
      if (jobject2 != null && jobject2[label] != null)
        return JsonRpcClient.GetField<TType>(jobject2, label, defaultValue);
      this.client.LogErrorMessage("XBMC.GetInfoLabels(" + label + "): Invalid response");
      return defaultValue;
    }
  }
}
