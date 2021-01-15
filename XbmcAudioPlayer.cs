// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcAudioPlayer
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcAudioPlayer : XbmcMediaPlayer
  {
    public XbmcPlayerState State
    {
      get
      {
        return this.state;
      }
    }

    internal XbmcAudioPlayer(JsonRpcClient client)
      : base("AudioPlayer", "MusicPlayer", client, 0)
    {
    }

    public XbmcPlayerState GetTime(
      out TimeSpan currentPosition,
      out TimeSpan totalLength)
    {
      return this.getTime(out currentPosition, out totalLength);
    }

    public double GetPercentage()
    {
      return this.getPercentage();
    }

    public virtual int Bitrate
    {
      get
      {
        this.client.LogMessage("XbmcAudioPlayer.Bitrate");
        return this.getInfo<int>("MusicPlayer.BitRate");
      }
    }

    public virtual int Channels
    {
      get
      {
        this.client.LogMessage("XbmcAudioPlayer.Channels");
        return this.getInfo<int>("MusicPlayer.Channels");
      }
    }

    public virtual string Codec
    {
      get
      {
        this.client.LogMessage("XbmcAudioPlayer.Codec");
        return this.getInfo<string>("MusicPlayer.Codec");
      }
    }
  }
}
