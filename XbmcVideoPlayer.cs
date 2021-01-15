// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcVideoPlayer
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcVideoPlayer : XbmcMediaPlayer
  {
    public XbmcPlayerState State
    {
      get
      {
        return this.state;
      }
    }

    internal XbmcVideoPlayer(JsonRpcClient client)
      : base("VideoPlayer", client, 1)
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

    public virtual string VideoCodec
    {
      get
      {
        this.client.LogMessage("XbmcVideoPlayer.VideoCodec");
        return this.getInfo<string>("VideoPlayer.VideoCodec");
      }
    }

    public virtual int AudioChannels
    {
      get
      {
        this.client.LogMessage("XbmcVideoPlayer.AudioChannels");
        return this.getInfo<int>("VideoPlayer.AudioChannels");
      }
    }

    public virtual string AudioCodec
    {
      get
      {
        this.client.LogMessage("XbmcVideoPlayer.AudioCodec");
        return this.getInfo<string>("VideoPlayer.AudioCodec");
      }
    }
  }
}
