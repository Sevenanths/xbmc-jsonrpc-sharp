// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcPlayerPlaybackPositionChangedEventArgs
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcPlayerPlaybackPositionChangedEventArgs : XbmcPlayerPlaybackChangedEventArgs
  {
    private TimeSpan position;
    private TimeSpan length;

    public TimeSpan Position
    {
      get
      {
        return this.position;
      }
    }

    public TimeSpan Length
    {
      get
      {
        return this.length;
      }
    }

    internal XbmcPlayerPlaybackPositionChangedEventArgs(
      XbmcMediaPlayer player,
      TimeSpan position,
      TimeSpan length)
      : base(player)
    {
      this.position = position;
      this.length = length;
    }
  }
}
