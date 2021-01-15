// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcVideo
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcVideo : XbmcPlayable
  {
    protected int playCount;
    protected string studio;
    protected string file;
    protected string director;
    protected string trailer;
    protected string tagline;
    protected string plot;
    protected string outline;
    protected string originalTitle;
    protected DateTime lastPlayed;
    protected TimeSpan duration;
    protected string writer;
    protected string mpaa;
    protected string showTitle;
    protected int season;
    protected int episodes;
    protected DateTime premiered;
    protected DateTime firstAired;
    protected string artist;
    protected string album;

    public virtual int PlayCount
    {
      get
      {
        return this.playCount;
      }
    }

    public virtual string Studio
    {
      get
      {
        return this.studio;
      }
    }

    public virtual string File
    {
      get
      {
        return this.file;
      }
    }

    public virtual string Director
    {
      get
      {
        return this.director;
      }
    }

    public virtual string Trailer
    {
      get
      {
        return this.trailer;
      }
    }

    public virtual string Tagline
    {
      get
      {
        return this.tagline;
      }
    }

    public virtual string Plot
    {
      get
      {
        return this.plot;
      }
    }

    public virtual string Outline
    {
      get
      {
        return this.outline;
      }
    }

    public virtual string OriginalTitle
    {
      get
      {
        return this.originalTitle;
      }
    }

    public virtual DateTime LastPlayed
    {
      get
      {
        return this.lastPlayed;
      }
    }

    public virtual TimeSpan Duration
    {
      get
      {
        return this.duration;
      }
    }

    public virtual string Writer
    {
      get
      {
        return this.writer;
      }
    }

    public virtual string Mpaa
    {
      get
      {
        return this.mpaa;
      }
    }

    public virtual string ShowTitle
    {
      get
      {
        return this.showTitle;
      }
    }

    public virtual int Season
    {
      get
      {
        return this.season;
      }
    }

    public virtual int Episodes
    {
      get
      {
        return this.episodes;
      }
    }

    public virtual DateTime Premiered
    {
      get
      {
        return this.premiered;
      }
    }

    public virtual DateTime FirstAired
    {
      get
      {
        return this.firstAired;
      }
    }

    public virtual string Artist
    {
      get
      {
        return this.artist;
      }
    }

    public virtual string Album
    {
      get
      {
        return this.album;
      }
    }

    protected XbmcVideo(
      int id,
      string thumbnail,
      string fanart,
      string title,
      string genre,
      int year,
      double rating,
      int playCount,
      string studio,
      string file,
      string director,
      string trailer,
      string tagline,
      string plot,
      string outline,
      string originalTitle,
      string lastPlayed,
      int duration,
      string writer,
      string mpaa,
      string showTitle,
      int season,
      int episodeCount,
      string premiered,
      string firstAired,
      string artist,
      string album)
      : base(id, thumbnail, fanart, title, genre, year, rating)
    {
      this.playCount = playCount;
      this.studio = studio;
      this.file = file;
      this.director = director;
      this.trailer = trailer;
      this.tagline = tagline;
      this.plot = plot;
      this.outline = outline;
      this.originalTitle = originalTitle;
      this.lastPlayed = !string.IsNullOrEmpty(lastPlayed) ? DateTime.Parse(lastPlayed) : new DateTime();
      this.duration = TimeSpan.FromSeconds((double) duration);
      this.writer = writer;
      this.mpaa = mpaa;
      this.showTitle = showTitle;
      this.firstAired = !string.IsNullOrEmpty(firstAired) ? DateTime.Parse(firstAired) : new DateTime();
      this.season = season;
      this.episodes = episodeCount;
      this.premiered = !string.IsNullOrEmpty(premiered) ? DateTime.Parse(premiered) : new DateTime();
      this.artist = artist;
      this.album = album;
    }

    internal static XbmcVideo FromJson(JObject obj)
    {
      return XbmcVideo.FromJson(obj, (JsonRpcClient) null);
    }

    internal static XbmcVideo FromJson(JObject obj, JsonRpcClient logger)
    {
      if (obj == null)
        return (XbmcVideo) null;
      try
      {
        string str = obj["type"] != null ? JsonRpcClient.GetField<string>(obj, "type") : "unknown";
        logger?.LogMessage("Trying to identify " + str);
        if ("episode" == str)
          return (XbmcVideo) XbmcTvEpisode.FromJson(obj, logger);
        if ("musicvideo" == str)
          return (XbmcVideo) XbmcMusicVideo.FromJson(obj, logger);
        if ("movie" == str || logger == null)
          return (XbmcVideo) XbmcMovie.FromJson(obj, logger);
        logger.LogMessage("Trying to identify Unhandled type of media as movie");
        return (XbmcVideo) XbmcMovie.FromJson(obj, logger);
      }
      catch (Exception ex)
      {
        logger?.LogErrorMessage("EXCEPTION in XbmcVideo.FromJson()!!!", ex);
        return (XbmcVideo) null;
      }
    }
  }
}
