// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcAudioPlaylist
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcAudioPlaylist : XbmcMediaPlaylist<XbmcSong>
  {
    internal XbmcAudioPlaylist(JsonRpcClient client)
      : base("AudioPlaylist", client, 0)
    {
    }

    public override XbmcSong GetCurrentItem()
    {
      return this.GetCurrentItem((string[]) null);
    }

    public override XbmcSong GetCurrentItem(string[] fields)
    {
      this.client.LogMessage("XbmcAudioPlaylist.GetCurrentItem()");
      JObject jobject1 = this.client.Call("Player.GetItem", (object) new JObject()
      {
        {
          "playerid",
          (JToken) this.id
        },
        {
          "properties",
          (JToken) new JArray(fields != null ? (object[]) fields : (object[]) XbmcMedia.Fields)
        }
      }) as JObject;
      if (jobject1 == null || jobject1["item"] == null)
      {
        this.client.LogErrorMessage("Playlist.GetCurrentItem(): Invalid response");
        return (XbmcSong) null;
      }
      JObject jobject2 = (JObject) jobject1["item"];
      this.client.LogMessage("Trying to identify an audio playlist item from JSON");
      XbmcSong xbmcSong = XbmcSong.FromJson(jobject2, this.client);
      if (xbmcSong == null)
        this.client.LogMessage("Result is null!!!");
      return xbmcSong;
    }

    public override XbmcPlaylist<XbmcSong> GetItems(params string[] fields)
    {
      return this.GetItems(-1, -1, fields);
    }

    public override XbmcPlaylist<XbmcSong> GetItems(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcAudioPlaylist.GetItems()");
      JObject items = this.getItems(fields, XbmcMedia.Fields, start, end);
      if (items == null || items["result"] == null || ((JObject) items["result"])["items"] == null)
      {
        this.client.LogErrorMessage("Playlist.GetItems(): Invalid response");
        return (XbmcPlaylist<XbmcSong>) null;
      }
      XbmcPlaylist<XbmcSong> xbmcPlaylist = XbmcPlaylist<XbmcSong>.FromJson((JObject) items["result"], this.client);
      foreach (JObject jobject in (IEnumerable<JToken>) items["items"])
        xbmcPlaylist.Add(XbmcSong.FromJson(jobject, this.client));
      return xbmcPlaylist;
    }

    public bool Add(XbmcSong song)
    {
      this.client.LogMessage("XbmcAudioPlaylist.Add()");
      if (song == null)
        throw new ArgumentNullException(nameof (song));
      if (string.IsNullOrEmpty(song.File))
        throw new ArgumentException("The given song has no file assigned to it.");
      return this.Add(song.File);
    }
  }
}
