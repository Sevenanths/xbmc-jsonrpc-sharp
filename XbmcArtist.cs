// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcArtist
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcArtist : XbmcMedia
  {
    private string name;

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    private XbmcArtist(int id, string name, string thumbnail, string fanart)
      : base(id, thumbnail, fanart)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException();
      this.name = name;
    }

    internal static XbmcArtist FromJson(JObject obj)
    {
      return XbmcArtist.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcArtist FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        return (XbmcArtist) null;
      try
      {
        return new XbmcArtist(JsonRpcClient.GetField<int>(obj, "artistid"), JsonRpcClient.GetField<string>(obj, "artist"), JsonRpcClient.GetField<string>(obj, "thumbnail"), JsonRpcClient.GetField<string>(obj, "fanart"));
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcArtist.FromJson()!!!", ex);
        return (XbmcArtist) null;
      }
    }
  }
}
