// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcPlayerPlaybackSpeedChangedEventArgs
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcPlayerPlaybackSpeedChangedEventArgs : XbmcPlayerPlaybackPositionChangedEventArgs
  {
    private int speed;

    public int Speed
    {
      get
      {
        return this.speed;
      }
    }

    internal XbmcPlayerPlaybackSpeedChangedEventArgs(
      XbmcMediaPlayer player,
      TimeSpan position,
      TimeSpan length,
      int speed)
      : base(player, position, length)
    {
      this.speed = speed;
    }
  }
}
