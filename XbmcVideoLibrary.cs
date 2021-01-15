// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcVideoLibrary
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcVideoLibrary : XbmcMediaLibrary
  {
    internal XbmcVideoLibrary(JsonRpcClient client)
      : base("VideoLibrary", client)
    {
    }

    public ICollection<XbmcMovie> GetMovies(params string[] fields)
    {
      return this.GetMovies(-1, -1, fields);
    }

    public ICollection<XbmcMovie> GetMovies(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetMovies()");
      JObject jobject1 = new JObject();
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 2];
        strArray[0] = "movieid";
        strArray[1] = "title";
        Array.Copy((Array) fields, 0, (Array) strArray, 2, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetMovies", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["movies"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetMovies(): Invalid response");
        return (ICollection<XbmcMovie>) null;
      }
      List<XbmcMovie> xbmcMovieList = new List<XbmcMovie>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["movies"])
        xbmcMovieList.Add(XbmcMovie.FromJson(jobject4, this.client));
      return (ICollection<XbmcMovie>) xbmcMovieList;
    }

    public ICollection<XbmcTvShow> GetTvShows(params string[] fields)
    {
      return this.GetTvShows(-1, -1, fields);
    }

    public ICollection<XbmcTvShow> GetTvShows(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetTvShows()");
      JObject jobject1 = new JObject();
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 2];
        strArray[0] = "tvshowid";
        strArray[1] = "title";
        Array.Copy((Array) fields, 0, (Array) strArray, 2, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetTVShows", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["tvshows"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetTVShows(): Invalid response");
        return (ICollection<XbmcTvShow>) null;
      }
      List<XbmcTvShow> xbmcTvShowList = new List<XbmcTvShow>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["tvshows"])
        xbmcTvShowList.Add(XbmcTvShow.FromJson(jobject4, this.client));
      return (ICollection<XbmcTvShow>) xbmcTvShowList;
    }

    public ICollection<XbmcTvSeason> GetTvSeasons(
      XbmcTvShow tvshow,
      params string[] fields)
    {
      if (tvshow == null)
        throw new ArgumentNullException(nameof (tvshow));
      return this.getTvSeasons(tvshow.Id, -1, -1, fields);
    }

    public ICollection<XbmcTvSeason> GetTvSeasons(
      XbmcTvShow tvshow,
      int start,
      int end,
      params string[] fields)
    {
      if (tvshow == null)
        throw new ArgumentNullException(nameof (tvshow));
      return this.getTvSeasons(tvshow.Id, start, end, fields);
    }

    public ICollection<XbmcTvEpisode> GetTvEpisodes(
      XbmcTvShow tvshow,
      int season,
      params string[] fields)
    {
      if (tvshow == null)
        throw new ArgumentNullException(nameof (tvshow));
      return this.getTvEpisodes(tvshow.Id, season, -1, -1, fields);
    }

    public ICollection<XbmcTvEpisode> GetTvEpisodes(
      XbmcTvShow tvshow,
      int season,
      int start,
      int end,
      params string[] fields)
    {
      if (tvshow == null)
        throw new ArgumentNullException(nameof (tvshow));
      return this.getTvEpisodes(tvshow.Id, season, start, end, fields);
    }

    public ICollection<XbmcMusicVideo> GetMusicVideos(
      params string[] fields)
    {
      return this.GetMusicVideos(-1, -1, -1, -1, fields);
    }

    public ICollection<XbmcMusicVideo> GetMusicVideos(
      int start,
      int end,
      params string[] fields)
    {
      return this.GetMusicVideos(-1, -1, start, end, fields);
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      XbmcArtist artist,
      XbmcAlbum album,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      if (album == null)
        throw new ArgumentNullException(nameof (album));
      return this.GetMusicVideos(artist.Id, album.Id, -1, -1, fields);
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      XbmcArtist artist,
      XbmcAlbum album,
      int start,
      int end,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      if (album == null)
        throw new ArgumentNullException(nameof (album));
      return this.GetMusicVideos(artist.Id, album.Id, start, end, fields);
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      int artistId,
      int albumId,
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetMusicVideos()");
      JObject jobject1 = new JObject();
      if (artistId >= 0)
        jobject1.Add((object) new JProperty("artistid", (object) artistId));
      if (albumId >= 0)
        jobject1.Add((object) new JProperty("albumid", (object) albumId));
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 4];
        strArray[0] = "musicvideoid";
        strArray[1] = "title";
        strArray[2] = "artist";
        strArray[3] = "album";
        Array.Copy((Array) fields, 0, (Array) strArray, 4, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetMusicVideos", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["musicvideos"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetMusicVideos(): Invalid response");
        return (ICollection<XbmcMusicVideo>) null;
      }
      List<XbmcMusicVideo> xbmcMusicVideoList = new List<XbmcMusicVideo>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["musicvideos"])
        xbmcMusicVideoList.Add(XbmcMusicVideo.FromJson(jobject4));
      return (ICollection<XbmcMusicVideo>) xbmcMusicVideoList;
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      XbmcArtist artist,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      return this.GetMusicVideos(artist.Id, -1, -1, -1, fields);
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      XbmcArtist artist,
      int start,
      int end,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      return this.GetMusicVideos(artist.Id, -1, start, end, fields);
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      XbmcAlbum album,
      params string[] fields)
    {
      if (album == null)
        throw new ArgumentNullException(nameof (album));
      return this.GetMusicVideos(-1, album.Id, -1, -1, fields);
    }

    private ICollection<XbmcMusicVideo> GetMusicVideos(
      XbmcAlbum album,
      int start,
      int end,
      params string[] fields)
    {
      if (album == null)
        throw new ArgumentNullException(nameof (album));
      return this.GetMusicVideos(-1, album.Id, start, end, fields);
    }

    public ICollection<XbmcMovie> GetRecentlyAddedMovies(params string[] fields)
    {
      return this.GetRecentlyAddedMovies(-1, -1, fields);
    }

    public ICollection<XbmcMovie> GetRecentlyAddedMovies(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetRecentlyAddedMovies()");
      JObject jobject1 = new JObject();
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 2];
        strArray[0] = "movieid";
        strArray[1] = "title";
        Array.Copy((Array) fields, 0, (Array) strArray, 2, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetRecentlyAddedMovies", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["movies"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetRecentlyAddedMovies(): Invalid response");
        return (ICollection<XbmcMovie>) null;
      }
      List<XbmcMovie> xbmcMovieList = new List<XbmcMovie>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["movies"])
        xbmcMovieList.Add(XbmcMovie.FromJson(jobject4, this.client));
      return (ICollection<XbmcMovie>) xbmcMovieList;
    }

    public ICollection<XbmcTvEpisode> GetRecentlyAddedTvEpisodes(
      params string[] fields)
    {
      return this.GetRecentlyAddedTvEpisodes(-1, -1, fields);
    }

    public ICollection<XbmcTvEpisode> GetRecentlyAddedTvEpisodes(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetRecentlyAddedTvEpisodes()");
      JObject jobject1 = new JObject();
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 5];
        strArray[0] = "id";
        strArray[1] = "title";
        strArray[2] = "season";
        strArray[3] = "episode";
        strArray[4] = "showtitle";
        Array.Copy((Array) fields, 0, (Array) strArray, 5, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetRecentlyAddedEpisodes", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["episodes"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetRecentlyAddedEpisodes(): Invalid response");
        return (ICollection<XbmcTvEpisode>) null;
      }
      List<XbmcTvEpisode> xbmcTvEpisodeList = new List<XbmcTvEpisode>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["episodes"])
        xbmcTvEpisodeList.Add(XbmcTvEpisode.FromJson(jobject4, this.client));
      return (ICollection<XbmcTvEpisode>) xbmcTvEpisodeList;
    }

    public ICollection<XbmcMusicVideo> GetRecentlyAddedMusicVideos(
      params string[] fields)
    {
      return this.GetRecentlyAddedMusicVideos(-1, -1, fields);
    }

    public ICollection<XbmcMusicVideo> GetRecentlyAddedMusicVideos(
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetRecentlyAddedMusicVideos()");
      JObject jobject1 = new JObject();
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 4];
        strArray[0] = "musicvideoid";
        strArray[1] = "title";
        strArray[2] = "artist";
        strArray[3] = "album";
        Array.Copy((Array) fields, 0, (Array) strArray, 4, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetRecentlyAddedMusicVideos", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["musicvideos"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetRecentlyAddedMusicVideos(): Invalid response");
        return (ICollection<XbmcMusicVideo>) null;
      }
      List<XbmcMusicVideo> xbmcMusicVideoList = new List<XbmcMusicVideo>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["musicvideos"])
        xbmcMusicVideoList.Add(XbmcMusicVideo.FromJson(jobject4));
      return (ICollection<XbmcMusicVideo>) xbmcMusicVideoList;
    }

    private ICollection<XbmcTvSeason> getTvSeasons(
      int tvshowId,
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetTvSeasons()");
      if (tvshowId < 0)
        throw new ArgumentException("Invalid TvShow Id (" + (object) tvshowId + ")");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("tvshowid", (object) tvshowId));
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 3];
        strArray[0] = "title";
        strArray[1] = "season";
        strArray[2] = "showtitle";
        Array.Copy((Array) fields, 0, (Array) strArray, 3, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetSeasons", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["seasons"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetSeasons(): Invalid response");
        return (ICollection<XbmcTvSeason>) null;
      }
      List<XbmcTvSeason> xbmcTvSeasonList = new List<XbmcTvSeason>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["seasons"])
        xbmcTvSeasonList.Add(XbmcTvSeason.FromJson(jobject4, this.client));
      return (ICollection<XbmcTvSeason>) xbmcTvSeasonList;
    }

    private ICollection<XbmcTvEpisode> getTvEpisodes(
      int tvshowId,
      int season,
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcVideoLibrary.GetTvEpisodes()");
      if (tvshowId < 0)
        throw new ArgumentException("Invalid TvShow Id (" + (object) tvshowId + ")");
      if (season < 0)
        throw new ArgumentException("Invalid Season (" + (object) season + ")");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("tvshowid", (object) tvshowId));
      jobject1.Add((object) new JProperty(nameof (season), (object) season));
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 5];
        strArray[0] = "id";
        strArray[1] = "title";
        strArray[2] = nameof (season);
        strArray[3] = "episode";
        strArray[4] = "showtitle";
        Array.Copy((Array) fields, 0, (Array) strArray, 5, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      JObject jobject2 = new JObject();
      if (start >= 0)
        jobject2.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject2.Add((object) new JProperty(nameof (end), (object) end));
      jobject1.Add((object) new JProperty("limits", (object) jobject2));
      JObject jobject3 = this.client.Call("VideoLibrary.GetEpisodes", (object) jobject1) as JObject;
      if (jobject3 == null || jobject3["episodes"] == null)
      {
        this.client.LogErrorMessage("VideoLibrary.GetEpisodes(): Invalid response");
        return (ICollection<XbmcTvEpisode>) null;
      }
      List<XbmcTvEpisode> xbmcTvEpisodeList = new List<XbmcTvEpisode>();
      foreach (JObject jobject4 in (IEnumerable<JToken>) jobject3["episodes"])
        xbmcTvEpisodeList.Add(XbmcTvEpisode.FromJson(jobject4, this.client));
      return (ICollection<XbmcTvEpisode>) xbmcTvEpisodeList;
    }
  }
}
