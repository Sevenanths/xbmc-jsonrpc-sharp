// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcMediaPlaylist`1
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public abstract class XbmcMediaPlaylist<TMediaType> : XbmcJsonRpcNamespace where TMediaType : XbmcPlayable
  {
    private string playlistName;
    protected int id;

    protected XbmcMediaPlaylist(string playlistName, JsonRpcClient client, int id)
      : base(client)
    {
      if (string.IsNullOrEmpty(playlistName))
        throw new ArgumentException();
      this.playlistName = playlistName;
      this.id = id;
    }

    public virtual bool Play()
    {
      this.client.LogMessage("XbmcMediaPlaylist.Play()");
      return this.client.Call(this.playlistName + ".Play") != null;
    }

    public virtual bool Play(int itemIndex)
    {
      this.client.LogMessage("XbmcMediaPlaylist.(" + (object) itemIndex + ")");
      return this.client.Call(this.playlistName + ".Play", (object) itemIndex) != null;
    }

    public virtual bool SkipPrevious()
    {
      this.client.LogMessage("XbmcMediaPlaylist.SkipPrevious()");
      return this.client.Call(this.playlistName + ".SkipPrevious") != null;
    }

    public virtual bool SkipNext()
    {
      this.client.LogMessage("XbmcMediaPlaylist.SkipNext()");
      return this.client.Call(this.playlistName + ".SkipNext") != null;
    }

    public abstract TMediaType GetCurrentItem(string[] fields);

    public abstract TMediaType GetCurrentItem();

    public abstract XbmcPlaylist<TMediaType> GetItems(params string[] fields);

    public abstract XbmcPlaylist<TMediaType> GetItems(
      int start,
      int end,
      params string[] fields);

    public virtual bool Add(string file)
    {
      this.client.LogMessage("XbmcMediaPlaylist.Add(" + file + ")");
      if (string.IsNullOrEmpty(file))
        throw new ArgumentException(nameof (file));
      JObject jobject = new JObject();
      jobject.Add((object) new JProperty(nameof (file), (object) file));
      return this.client.Call("Playlist.Add", (object) jobject) != null;
    }

    public virtual bool Clear()
    {
      this.client.LogMessage("XbmcMediaPlaylist.Clear()");
      return this.client.Call("Playlist.Clear") != null;
    }

    public virtual bool Shuffle()
    {
      this.client.LogMessage("XbmcMediaPlaylist.Shuffle()");
      return this.client.Call(this.playlistName + ".Shuffle") != null;
    }

    public virtual bool UnShuffle()
    {
      this.client.LogMessage("XbmcMediaPlaylist.UnShuffle()");
      return this.client.Call(this.playlistName + ".UnShuffle") != null;
    }

    protected JObject getItems(string[] fields, string[] defaultFields, int start, int end)
    {
      JObject jobject1 = new JObject();
      if (fields != null && fields.Length > 0)
        jobject1.Add((object) new JProperty("properties", (object[]) fields));
      else
        jobject1.Add((object) new JProperty("properties", (object[]) defaultFields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      jobject1.Add((object) new JProperty("playlistid", (object) this.id));
      return this.client.Call("Playlist.GetItems", (object) jobject1) as JObject;
    }
  }
}
