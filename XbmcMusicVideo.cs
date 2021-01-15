// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcMusicVideo
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcMusicVideo : XbmcVideo
  {
    private new string Trailer
    {
      get
      {
        return string.Empty;
      }
    }

    private new string Tagline
    {
      get
      {
        return string.Empty;
      }
    }

    private new string Outline
    {
      get
      {
        return string.Empty;
      }
    }

    private new string OriginalTitle
    {
      get
      {
        return string.Empty;
      }
    }

    private new string Writer
    {
      get
      {
        return string.Empty;
      }
    }

    private new string Mpaa
    {
      get
      {
        return string.Empty;
      }
    }

    private new string ShowTitle
    {
      get
      {
        return string.Empty;
      }
    }

    private new int Season
    {
      get
      {
        return -1;
      }
    }

    private new int Episodes
    {
      get
      {
        return -1;
      }
    }

    private new DateTime Premiered
    {
      get
      {
        return new DateTime();
      }
    }

    private new DateTime FirstAired
    {
      get
      {
        return new DateTime();
      }
    }

    private XbmcMusicVideo(
      int id,
      string thumbnail,
      string fanart,
      string file,
      string title,
      string genre,
      int year,
      double rating,
      string director,
      string plot,
      string lastPlayed,
      int duration,
      int playCount,
      string studio,
      string artist,
      string album)
      : base(id, thumbnail, fanart, title, genre, year, rating, playCount, studio, file, director, string.Empty, string.Empty, plot, string.Empty, string.Empty, lastPlayed, duration, string.Empty, string.Empty, string.Empty, -1, -1, string.Empty, string.Empty, artist, album)
    {
      if (string.IsNullOrEmpty(file))
        throw new ArgumentException(nameof (file));
      if (string.IsNullOrEmpty(artist))
        throw new ArgumentException(nameof (artist));
    }

    internal static XbmcMusicVideo FromJson(JObject obj)
    {
      return XbmcMusicVideo.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcMusicVideo FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      try
      {
        return new XbmcMusicVideo(JsonRpcClient.GetField<int>(obj, "musicvideoid"), JsonRpcClient.GetField<string>(obj, "thumbnail"), JsonRpcClient.GetField<string>(obj, "fanart"), JsonRpcClient.GetField<string>(obj, "file"), JsonRpcClient.GetField<string>(obj, "title"), JsonRpcClient.GetField<string>(obj, "genre", string.Empty), JsonRpcClient.GetField<int>(obj, "year"), JsonRpcClient.GetField<double>(obj, "rating"), JsonRpcClient.GetField<string>(obj, "director", string.Empty), JsonRpcClient.GetField<string>(obj, "plot", string.Empty), JsonRpcClient.GetField<string>(obj, "lastplayed", string.Empty), JsonRpcClient.GetField<int>(obj, "duration"), JsonRpcClient.GetField<int>(obj, "playcount"), JsonRpcClient.GetField<string>(obj, "studio", string.Empty), JsonRpcClient.GetField<string>(obj, "artist"), JsonRpcClient.GetField<string>(obj, "album", string.Empty));
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcMusicVideo.FromJson()!!!", ex);
        return (XbmcMusicVideo) null;
      }
    }
  }
}
