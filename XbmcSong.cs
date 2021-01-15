// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcSong
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcSong : XbmcAudio
  {
    private string file;
    private string album;
    private int track;
    private TimeSpan duration;
    private string comment;
    private string lyrics;

    public string File
    {
      get
      {
        return this.file;
      }
    }

    public string Album
    {
      get
      {
        return this.album;
      }
    }

    public int Track
    {
      get
      {
        return this.track;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        return this.duration;
      }
    }

    public string Comment
    {
      get
      {
        return this.comment;
      }
    }

    public string Lyrics
    {
      get
      {
        return this.lyrics;
      }
    }

    private XbmcSong(
      int id,
      string thumbnail,
      string fanart,
      string file,
      string title,
      string artist,
      string genre,
      int year,
      int rating,
      string album,
      int track,
      int duration,
      string comment,
      string lyrics)
      : base(id, thumbnail, fanart, title, artist, genre, year, rating)
    {
      if (string.IsNullOrEmpty(file))
        throw new ArgumentException(nameof (file));
      this.file = file;
      this.album = album;
      this.track = track;
      this.duration = TimeSpan.FromSeconds((double) duration);
      this.comment = comment;
      this.lyrics = lyrics;
    }

    internal static XbmcSong FromJson(JObject obj)
    {
      return XbmcSong.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcSong FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        return (XbmcSong) null;
      try
      {
        return new XbmcSong(JsonRpcClient.GetField<int>(obj, "songid"), JsonRpcClient.GetField<string>(obj, "thumbnail"), JsonRpcClient.GetField<string>(obj, "fanart"), JsonRpcClient.GetField<string>(obj, "file"), JsonRpcClient.GetField<string>(obj, "title"), JsonRpcClient.GetField<string>(obj, "artist"), JsonRpcClient.GetField<string>(obj, "genre", string.Empty), JsonRpcClient.GetField<int>(obj, "year"), JsonRpcClient.GetField<int>(obj, "rating"), JsonRpcClient.GetField<string>(obj, "album", string.Empty), JsonRpcClient.GetField<int>(obj, "track"), JsonRpcClient.GetField<int>(obj, "duration"), JsonRpcClient.GetField<string>(obj, "comment", string.Empty), JsonRpcClient.GetField<string>(obj, "lyrics", string.Empty));
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcSong.FromJson()!!!", ex);
        return (XbmcSong) null;
      }
    }
  }
}
