// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcVideoPlaylist
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcVideoPlaylist : XbmcMediaPlaylist<XbmcVideo>
  {
    internal XbmcVideoPlaylist(JsonRpcClient client)
      : base("Playlist", client, 1)
    {
    }

    public override XbmcVideo GetCurrentItem()
    {
      return this.GetCurrentItem((string[]) null);
    }

    public override XbmcVideo GetCurrentItem(string[] fields)
    {
      this.client.LogMessage("XbmcVideoPlaylist.GetCurrentItem()");
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
        this.client.LogErrorMessage("XbmcVideoPlaylist.GetCurrentItem(): Invalid response");
        return (XbmcVideo) null;
      }
      JObject jobject2 = (JObject) jobject1["item"];
      this.client.LogMessage("Trying to identify a video playlist item from JSON");
      XbmcVideo xbmcVideo = XbmcVideo.FromJson(jobject2, this.client);
      if (xbmcVideo == null)
        this.client.LogMessage("Result is null!!!");
      return xbmcVideo;
    }

    public override XbmcPlaylist<XbmcVideo> GetItems(params string[] fields)
    {
      return this.GetItems(-1, -1, fields);
    }

    public override XbmcPlaylist<XbmcVideo> GetItems(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoPlaylist.GetItems()");
      JObject items = this.getItems(fields, XbmcMedia.Fields, start, end);
      if (items == null || items["items"] == null)
      {
        this.client.LogErrorMessage("Playlist.GetItems(): Invalid response");
        return (XbmcPlaylist<XbmcVideo>) null;
      }
      XbmcPlaylist<XbmcVideo> xbmcPlaylist = XbmcPlaylist<XbmcVideo>.FromJson(items, this.client);
      foreach (JObject jobject in (IEnumerable<JToken>) items["items"])
        xbmcPlaylist.Add(XbmcVideo.FromJson(jobject, this.client));
      return xbmcPlaylist;
    }

    public bool Add(XbmcVideo video)
    {
      this.client.LogMessage("XbmcVideoPlaylist.Add()");
      if (video == null)
        throw new ArgumentNullException(nameof (video));
      if (string.IsNullOrEmpty(video.File))
        throw new ArgumentException("The given video has no file assigned to it.");
      return this.Add(video.File);
    }
  }
}
