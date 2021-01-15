// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcMedia
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

namespace XBMC.JsonRpc
{
  public class XbmcMedia
  {
    protected static string[] fields = new string[47]
    {
      "title",
      "artist",
      "albumartist",
      "genre",
      "year",
      "rating",
      "album",
      "track",
      "duration",
      "comment",
      "lyrics",
      "musicbrainztrackid",
      "musicbrainzartistid",
      "musicbrainzalbumid",
      "musicbrainzalbumartistid",
      "playcount",
      nameof (fanart),
      "director",
      "trailer",
      "tagline",
      "plot",
      "plotoutline",
      "originaltitle",
      "lastplayed",
      "writer",
      "studio",
      "mpaa",
      "country",
      "imdbnumber",
      "premiered",
      "productioncode",
      "runtime",
      "set",
      "showlink",
      "streamdetails",
      "top250",
      "votes",
      "firstaired",
      "season",
      "episode",
      "showtitle",
      nameof (thumbnail),
      "file",
      "artistid",
      "albumid",
      "tvshowid",
      "setid"
    };
    private int id;
    private string thumbnail;
    private string fanart;

    internal static string[] Fields
    {
      get
      {
        if (XbmcMedia.fields == null)
          return new string[0];
        return XbmcMedia.fields;
      }
    }

    public virtual int Id
    {
      get
      {
        return this.id;
      }
    }

    public virtual string Thumbnail
    {
      get
      {
        return this.thumbnail;
      }
    }

    public virtual string Fanart
    {
      get
      {
        return this.fanart;
      }
    }

    protected XbmcMedia(int id, string thumbnail, string fanart)
    {
      this.id = id;
      this.thumbnail = thumbnail != null ? thumbnail : string.Empty;
      this.fanart = fanart != null ? fanart : string.Empty;
    }
  }
}
