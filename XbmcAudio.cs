// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcAudio
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcAudio : XbmcPlayable
  {
    protected string artist;

    public string Artist
    {
      get
      {
        return this.artist;
      }
    }

    protected XbmcAudio(
      int id,
      string thumbnail,
      string fanart,
      string title,
      string artist,
      string genre,
      int year,
      int rating)
      : base(id, thumbnail, fanart, title, genre, year, (double) rating)
    {
      if (string.IsNullOrEmpty(artist))
        throw new ArgumentException(nameof (artist));
      this.artist = artist;
    }
  }
}
