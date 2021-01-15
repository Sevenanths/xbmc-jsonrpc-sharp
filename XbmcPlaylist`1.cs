// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcPlaylist`1
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcPlaylist<TMediaType> where TMediaType : XbmcMedia
  {
    private int current;
    private int start;
    private int total;
    private List<TMediaType> items;

    public int Current
    {
      get
      {
        return this.current;
      }
    }

    public int Start
    {
      get
      {
        return this.start;
      }
    }

    public int End
    {
      get
      {
        return this.start + this.Count;
      }
    }

    public int Total
    {
      get
      {
        return this.total;
      }
    }

    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    public ICollection<TMediaType> Items
    {
      get
      {
        return (ICollection<TMediaType>) this.items;
      }
    }

    private XbmcPlaylist(int current, int start, int total)
    {
      this.current = current;
      this.start = start;
      this.total = total;
      this.items = new List<TMediaType>();
    }

    internal void Add(TMediaType item)
    {
      if ((object) item == null)
        return;
      this.items.Add(item);
    }

    internal static XbmcPlaylist<TMediaType> FromJson(JObject obj)
    {
      return XbmcPlaylist<TMediaType>.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcPlaylist<TMediaType> FromJson(JObject obj, JsonRpcClient logger)
    {
      try
      {
        return new XbmcPlaylist<TMediaType>((int) obj["current"], (int) obj["start"], (int) obj["total"]);
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcPlaylist.FromJson()!!!", ex);
        return (XbmcPlaylist<TMediaType>) null;
      }
    }
  }
}
