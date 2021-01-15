// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcTvShow
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcTvShow : XbmcVideo
  {
    private new string File
    {
      get
      {
        return string.Empty;
      }
    }

    private new string Director
    {
      get
      {
        return string.Empty;
      }
    }

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

    private new DateTime LastPlayed
    {
      get
      {
        return new DateTime();
      }
    }

    private new TimeSpan Duration
    {
      get
      {
        return new TimeSpan();
      }
    }

    private new string Writer
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

    private new DateTime FirstAired
    {
      get
      {
        return new DateTime();
      }
    }

    private new string Artist
    {
      get
      {
        return string.Empty;
      }
    }

    private new string Album
    {
      get
      {
        return string.Empty;
      }
    }

    private XbmcTvShow(
      int id,
      string thumbnail,
      string fanart,
      string title,
      string genre,
      int year,
      double rating,
      string plot,
      int episodeCount,
      int playCount,
      string studio,
      string mpaa,
      string premiered)
      : base(id, thumbnail, fanart, title, genre, year, rating, playCount, studio, string.Empty, string.Empty, string.Empty, string.Empty, plot, string.Empty, string.Empty, string.Empty, -1, string.Empty, mpaa, string.Empty, -1, episodeCount, premiered, string.Empty, string.Empty, string.Empty)
    {
    }

    internal static XbmcTvShow FromJson(JObject obj)
    {
      return XbmcTvShow.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcTvShow FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        return (XbmcTvShow) null;
      try
      {
        return new XbmcTvShow(JsonRpcClient.GetField<int>(obj, "tvshowid"), JsonRpcClient.GetField<string>(obj, "thumbnail"), JsonRpcClient.GetField<string>(obj, "fanart"), JsonRpcClient.GetField<string>(obj, "title"), JsonRpcClient.GetField<string>(obj, "genre", string.Empty), JsonRpcClient.GetField<int>(obj, "year"), JsonRpcClient.GetField<double>(obj, "rating"), JsonRpcClient.GetField<string>(obj, "plot", string.Empty), JsonRpcClient.GetField<int>(obj, "episode"), JsonRpcClient.GetField<int>(obj, "playcount"), JsonRpcClient.GetField<string>(obj, "studio", string.Empty), JsonRpcClient.GetField<string>(obj, "mpaa", string.Empty), JsonRpcClient.GetField<string>(obj, "premiered", string.Empty));
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcTvShow.FromJson()!!!", ex);
        return (XbmcTvShow) null;
      }
    }
  }
}
