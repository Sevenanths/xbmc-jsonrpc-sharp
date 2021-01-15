// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcPlaylist
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcPlaylist : XbmcJsonRpcNamespace
  {
    private XbmcAudioPlaylist audio;
    private XbmcVideoPlaylist video;

    public XbmcAudioPlaylist Audio
    {
      get
      {
        return this.audio;
      }
    }

    public XbmcVideoPlaylist Video
    {
      get
      {
        return this.video;
      }
    }

    public event EventHandler ItemQueued;

    internal XbmcPlaylist(JsonRpcClient client)
      : base(client)
    {
      this.audio = new XbmcAudioPlaylist(client);
      this.video = new XbmcVideoPlaylist(client);
    }

    public bool Create(string playlist)
    {
      this.client.LogMessage("XbmcPlaylist.Create(" + playlist + ")");
      if (string.IsNullOrEmpty(playlist))
        return false;
      return this.client.Call("Playlist.Create", (object) this.getPlaylistArgument(playlist)) != null;
    }

    public bool Destroy(string playlist)
    {
      this.client.LogMessage("XbmcPlaylist.Destroy(" + playlist + ")");
      if (string.IsNullOrEmpty(playlist))
        return false;
      return this.client.Call("Playlist.Destroy", (object) this.getPlaylistArgument(playlist)) != null;
    }

    [Obsolete("Use XbmcAudioPlaylist.GetItems() or XbmcVideoPlaylist.GetItems()", true)]
    public bool GetItems()
    {
      this.client.LogErrorMessage("XbmcPlaylist.GetItems() is obsolete");
      throw new NotSupportedException();
    }

    [Obsolete("Use XbmcAudioPlaylist.GetItems() or XbmcVideoPlaylist.GetItems()", true)]
    public bool GetItems(string playlist)
    {
      this.client.LogErrorMessage("XbmcPlaylist.GetItems(playlist) is obsolete");
      throw new NotSupportedException();
    }

    [Obsolete("Use XbmcAudioPlaylist.Add() or XbmcVideoPlaylist.Add()", true)]
    public bool Add(string playlist, string file)
    {
      this.client.LogErrorMessage("XbmcPlaylist.Add() is obsolete");
      throw new NotSupportedException();
    }

    [Obsolete("Use XbmcAudioPlaylist.Remove() or XbmcVideoPlaylist.Remove()", true)]
    public bool Remove(string playlist, int item)
    {
      this.client.LogErrorMessage("XbmcPlaylist.Remove() is obsolete");
      throw new NotSupportedException();
    }

    [Obsolete("Use XbmcAudioPlaylist.Swap() or XbmcVideoPlaylist.Swap()", true)]
    public bool Swap(string playlist, int item1, int item2)
    {
      this.client.LogErrorMessage("XbmcPlaylist.Swap() is obsolete");
      throw new NotSupportedException();
    }

    public bool Shuffle(string playlist)
    {
      this.client.LogMessage("XbmcPlaylist.Shuffle(" + playlist + ")");
      if (string.IsNullOrEmpty(playlist))
        return false;
      return this.client.Call("Playlist.Shuffle", (object) this.getPlaylistArgument(playlist)) != null;
    }

    public bool UnShuffle(string playlist)
    {
      this.client.LogMessage("XbmcPlaylist.UnShuffle(" + playlist + ")");
      if (string.IsNullOrEmpty(playlist))
        return false;
      return this.client.Call("Playlist.UnShuffle", (object) this.getPlaylistArgument(playlist)) != null;
    }

    public virtual int Position
    {
      get
      {
        this.client.LogMessage("XbmcPlaylist.Position");
        return this.getInfo<int>("Playlist.Position", -1);
      }
    }

    public virtual int Length
    {
      get
      {
        this.client.LogMessage("XbmcPlaylist.Length");
        return this.getInfo<int>("Playlist.Length", -1);
      }
    }

    public virtual bool Random
    {
      get
      {
        this.client.LogMessage("XbmcPlaylist.Random");
        return this.getInfo<string>("Playlist.Random") == nameof (Random);
      }
    }

    public virtual XbmcRepeatTypes Repeat
    {
      get
      {
        this.client.LogMessage("XbmcPlaylist.Repeat");
        string info = this.getInfo<string>("Playlist.Repeat");
        if (info == "One")
          return XbmcRepeatTypes.One;
        return info == "All" ? XbmcRepeatTypes.All : XbmcRepeatTypes.Off;
      }
    }

    internal void OnItemQueued()
    {
      if (this.ItemQueued == null)
        return;
      this.ItemQueued((object) this, (EventArgs) null);
    }

    private JObject getPlaylistArgument(string playlist)
    {
      JObject jobject = new JObject();
      jobject.Add((object) new JProperty(nameof (playlist), (object) playlist));
      return jobject;
    }
  }
}
