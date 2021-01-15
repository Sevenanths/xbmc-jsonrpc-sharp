// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcPlayable
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcPlayable : XbmcMedia
  {
    protected string title;
    protected string genre;
    protected int year;
    protected double rating;

    public virtual string Title
    {
      get
      {
        return this.title;
      }
    }

    public virtual string Genre
    {
      get
      {
        return this.genre;
      }
    }

    public virtual int Year
    {
      get
      {
        return this.year;
      }
    }

    public virtual double Rating
    {
      get
      {
        return this.rating;
      }
    }

    protected XbmcPlayable(
      int id,
      string thumbnail,
      string fanart,
      string title,
      string genre,
      int year,
      double rating)
      : base(id, thumbnail, fanart)
    {
      if (string.IsNullOrEmpty(title))
        throw new ArgumentException(nameof (title));
      this.title = title;
      this.genre = genre;
      this.year = year;
      this.rating = rating;
    }
  }
}
