// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcMovie
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcMovie : XbmcVideo
  {
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

    private XbmcMovie(
      int id,
      string thumbnail,
      string fanart,
      string file,
      string title,
      string genre,
      int year,
      double rating,
      string director,
      string trailer,
      string tagline,
      string plot,
      string outline,
      string originalTitle,
      string lastPlayed,
      int duration,
      int playCount,
      string writer,
      string studio,
      string mpaa)
      : base(id, thumbnail, fanart, title, genre, year, rating, playCount, studio, file, director, trailer, tagline, plot, outline, originalTitle, lastPlayed, duration, writer, mpaa, string.Empty, -1, -1, string.Empty, string.Empty, string.Empty, string.Empty)
    {
      if (string.IsNullOrEmpty(file))
        throw new ArgumentException(nameof (file));
    }

    internal static XbmcMovie FromJson(JObject obj)
    {
      return XbmcMovie.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcMovie FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        return (XbmcMovie) null;
      try
      {
        return new XbmcMovie(JsonRpcClient.GetField<int>(obj, "movieid"), JsonRpcClient.GetField<string>(obj, "thumbnail"), JsonRpcClient.GetField<string>(obj, "fanart"), JsonRpcClient.GetField<string>(obj, "file"), JsonRpcClient.GetField<string>(obj, "title"), JsonRpcClient.GetField<string>(obj, "genre", string.Empty), JsonRpcClient.GetField<int>(obj, "year"), JsonRpcClient.GetField<double>(obj, "rating"), JsonRpcClient.GetField<string>(obj, "director", string.Empty), JsonRpcClient.GetField<string>(obj, "trailer", string.Empty), JsonRpcClient.GetField<string>(obj, "tagline", string.Empty), JsonRpcClient.GetField<string>(obj, "plot", string.Empty), JsonRpcClient.GetField<string>(obj, "plotoutline", string.Empty), JsonRpcClient.GetField<string>(obj, "originaltitle", string.Empty), JsonRpcClient.GetField<string>(obj, "lastplayed", string.Empty), JsonRpcClient.GetField<int>(obj, "duration"), JsonRpcClient.GetField<int>(obj, "playcount"), JsonRpcClient.GetField<string>(obj, "writer", string.Empty), JsonRpcClient.GetField<string>(obj, "studio", string.Empty), JsonRpcClient.GetField<string>(obj, "mpaa", string.Empty));
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcMovie.FromJson()!!!", ex);
        return (XbmcMovie) null;
      }
    }
  }
}
