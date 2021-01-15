// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcTvSeason
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcTvSeason : XbmcVideo
  {
    private new int Id
    {
      get
      {
        return -1;
      }
    }

    private new int Year
    {
      get
      {
        return -1;
      }
    }

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

    private new string Plot
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

    private XbmcTvSeason(
      string thumbnail,
      string fanart,
      string title,
      string genre,
      double rating,
      string showTitle,
      int season,
      int episodeCount,
      int playCount,
      string studio,
      string mpaa)
      : base(-1, thumbnail, fanart, title, genre, 0, rating, playCount, studio, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, -1, string.Empty, mpaa, showTitle, season, episodeCount, string.Empty, string.Empty, string.Empty, string.Empty)
    {
      if (string.IsNullOrEmpty(showTitle))
        throw new ArgumentException(nameof (showTitle));
      if (season < 0)
        throw new ArgumentException(nameof (season));
    }

    internal static XbmcTvSeason FromJson(JObject obj)
    {
      return XbmcTvSeason.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcTvSeason FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        return (XbmcTvSeason) null;
      try
      {
        return new XbmcTvSeason(JsonRpcClient.GetField<string>(obj, "thumbnail"), JsonRpcClient.GetField<string>(obj, "fanart"), JsonRpcClient.GetField<string>(obj, "title"), JsonRpcClient.GetField<string>(obj, "genre", string.Empty), JsonRpcClient.GetField<double>(obj, "rating"), JsonRpcClient.GetField<string>(obj, "showtitle"), JsonRpcClient.GetField<int>(obj, "season"), JsonRpcClient.GetField<int>(obj, "episode"), JsonRpcClient.GetField<int>(obj, "playcount"), JsonRpcClient.GetField<string>(obj, "studio", string.Empty), JsonRpcClient.GetField<string>(obj, "mpaa", string.Empty));
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcTvSeason.FromJson()!!!", ex);
        return (XbmcTvSeason) null;
      }
    }
  }
}
