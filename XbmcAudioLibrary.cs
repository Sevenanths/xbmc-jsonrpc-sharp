// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcAudioLibrary
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcAudioLibrary : XbmcMediaLibrary
  {
    internal XbmcAudioLibrary(JsonRpcClient client)
      : base("AudioLibrary", client)
    {
    }

    public ICollection<XbmcArtist> GetArtists()
    {
      return this.GetArtists(-1, -1);
    }

    public ICollection<XbmcArtist> GetArtists(int start, int end)
    {
      this.client.LogMessage("XbmcAudioLibrary.GetArtists()");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("fields", (object[]) XbmcMedia.Fields));
      if (start >= 0)
        jobject1.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject1.Add((object) new JProperty(nameof (end), (object) end));
      JObject jobject2 = this.client.Call("AudioLibrary.GetArtists", (object) jobject1) as JObject;
      if (jobject2 == null || jobject2["artists"] == null)
      {
        this.client.LogErrorMessage("AudioLibrary.GetArtists(): Invalid response");
        return (ICollection<XbmcArtist>) null;
      }
      List<XbmcArtist> xbmcArtistList = new List<XbmcArtist>();
      foreach (JObject jobject3 in (IEnumerable<JToken>) jobject2["artists"])
        xbmcArtistList.Add(XbmcArtist.FromJson(jobject3, this.client));
      return (ICollection<XbmcArtist>) xbmcArtistList;
    }

    public ICollection<XbmcAlbum> GetAlbums(params string[] fields)
    {
      return this.getAlbums(-1, -1, -1, -1, fields);
    }

    public ICollection<XbmcAlbum> GetAlbums(
      int start,
      int end,
      params string[] fields)
    {
      return this.getAlbums(-1, -1, start, end, fields);
    }

    public ICollection<XbmcAlbum> GetAlbums(
      XbmcArtist artist,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      return this.getAlbums(artist.Id, -1, -1, -1, fields);
    }

    public ICollection<XbmcAlbum> GetAlbums(
      XbmcArtist artist,
      int start,
      int end,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      return this.getAlbums(artist.Id, -1, start, end, fields);
    }

    public ICollection<XbmcAlbum> GetAlbums(int genreId, params string[] fields)
    {
      return this.getAlbums(-1, genreId, -1, -1, fields);
    }

    public ICollection<XbmcAlbum> GetAlbums(
      int genreId,
      int start,
      int end,
      params string[] fields)
    {
      return this.getAlbums(-1, genreId, start, end, fields);
    }

    public ICollection<XbmcSong> GetSongs(params string[] fields)
    {
      return this.getSongs(-1, -1, -1, -1, -1, fields);
    }

    public ICollection<XbmcSong> GetSongs(
      int start,
      int end,
      params string[] fields)
    {
      return this.getSongs(-1, -1, -1, start, end, fields);
    }

    public ICollection<XbmcSong> GetSongs(
      XbmcAlbum album,
      params string[] fields)
    {
      if (album == null)
        throw new ArgumentNullException(nameof (album));
      return this.getSongs(album.Id, -1, -1, -1, -1, fields);
    }

    public ICollection<XbmcSong> GetSongs(
      XbmcAlbum album,
      int start,
      int end,
      params string[] fields)
    {
      if (album == null)
        throw new ArgumentNullException(nameof (album));
      return this.getSongs(album.Id, -1, -1, start, end, fields);
    }

    public ICollection<XbmcSong> GetSongs(
      XbmcArtist artist,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      return this.getSongs(-1, artist.Id, -1, -1, -1, fields);
    }

    public ICollection<XbmcSong> GetSongs(
      XbmcArtist artist,
      int start,
      int end,
      params string[] fields)
    {
      if (artist == null)
        throw new ArgumentNullException(nameof (artist));
      return this.getSongs(-1, artist.Id, -1, start, end, fields);
    }

    public ICollection<XbmcSong> GetSongs(int genreId, params string[] fields)
    {
      return this.getSongs(-1, -1, genreId, -1, -1, fields);
    }

    public ICollection<XbmcSong> GetSongs(
      int genreId,
      int start,
      int end,
      params string[] fields)
    {
      return this.getSongs(-1, -1, genreId, start, end, fields);
    }

    private ICollection<XbmcSong> getSongs(
      int albumId,
      int artistId,
      int genreId,
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcAudioLibrary.GetSongs()");
      JObject jobject1 = new JObject();
      if (albumId >= 0)
        jobject1.Add((object) new JProperty("albumid", (object) albumId));
      if (artistId >= 0)
        jobject1.Add((object) new JProperty("artistid", (object) artistId));
      if (genreId >= 0)
        jobject1.Add((object) new JProperty("genreid", (object) genreId));
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 3];
        strArray[0] = "title";
        strArray[1] = "artist";
        strArray[2] = "album";
        Array.Copy((Array) fields, 0, (Array) strArray, 3, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcMedia.Fields));
      if (start >= 0)
        jobject1.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject1.Add((object) new JProperty(nameof (end), (object) end));
      JObject jobject2 = this.client.Call("AudioLibrary.GetSongs", (object) jobject1) as JObject;
      if (jobject2 == null || jobject2["songs"] == null)
      {
        this.client.LogErrorMessage("AudioLibrary.GetSongs(): Invalid response");
        return (ICollection<XbmcSong>) null;
      }
      List<XbmcSong> xbmcSongList = new List<XbmcSong>();
      foreach (JObject jobject3 in (IEnumerable<JToken>) jobject2["songs"])
        xbmcSongList.Add(XbmcSong.FromJson(jobject3, this.client));
      return (ICollection<XbmcSong>) xbmcSongList;
    }

    private ICollection<XbmcAlbum> getAlbums(
      int artistId,
      int genreId,
      int start,
      int end,
      params string[] fields)
    {
      this.client.LogMessage("XbmcAudioLibrary.GetAlbums()");
      JObject jobject1 = new JObject();
      if (artistId >= 0)
        jobject1.Add((object) new JProperty("artistid", (object) artistId));
      if (genreId >= 0)
        jobject1.Add((object) new JProperty("genreid", (object) genreId));
      if (fields != null && fields.Length > 0)
      {
        string[] strArray = new string[fields.Length + 2];
        strArray[0] = "albumid";
        strArray[1] = "album_artist";
        Array.Copy((Array) fields, 0, (Array) strArray, 2, fields.Length);
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) strArray));
      }
      else
        jobject1.Add((object) new JProperty(nameof (fields), (object[]) XbmcAlbum.Fields));
      if (start >= 0)
        jobject1.Add((object) new JProperty(nameof (start), (object) start));
      if (end >= 0)
        jobject1.Add((object) new JProperty(nameof (end), (object) end));
      JObject jobject2 = this.client.Call("AudioLibrary.GetAlbums", (object) jobject1) as JObject;
      if (jobject2 == null || jobject2["albums"] == null)
      {
        this.client.LogErrorMessage("AudioLibrary.GetAlbums(): Invalid response");
        return (ICollection<XbmcAlbum>) null;
      }
      List<XbmcAlbum> xbmcAlbumList = new List<XbmcAlbum>();
      foreach (JObject jobject3 in (IEnumerable<JToken>) jobject2["albums"])
        xbmcAlbumList.Add(XbmcAlbum.FromJson(jobject3, this.client));
      return (ICollection<XbmcAlbum>) xbmcAlbumList;
    }
  }
}
