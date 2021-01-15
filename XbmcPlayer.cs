// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcPlayer
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace XBMC.JsonRpc
{
  public class XbmcPlayer : XbmcJsonRpcNamespace
  {
    private XbmcAudioPlayer audio;
    private XbmcVideoPlayer video;
    private XbmcPicturePlayer pictures;

    public XbmcAudioPlayer Audio
    {
      get
      {
        bool video;
        bool audio;
        bool picture;
        int id;
        if (!this.GetActivePlayers(out video, out audio, out picture, out id) || !audio)
          return (XbmcAudioPlayer) null;
        return this.audio;
      }
    }

    public XbmcVideoPlayer Video
    {
      get
      {
        bool video;
        bool audio;
        bool picture;
        int id;
        if (!this.GetActivePlayers(out video, out audio, out picture, out id) || !video)
          return (XbmcVideoPlayer) null;
        return this.video;
      }
    }

    public XbmcPicturePlayer Pictures
    {
      get
      {
        bool video;
        bool audio;
        bool picture;
        int id;
        if (!this.GetActivePlayers(out video, out audio, out picture, out id) || !picture)
          return (XbmcPicturePlayer) null;
        return this.pictures;
      }
    }

    public event EventHandler<XbmcPlayerPlaybackChangedEventArgs> PlaybackStarted;

    public event EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs> PlaybackPaused;

    public event EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs> PlaybackResumed;

    public event EventHandler PlaybackStopped;

    public event EventHandler PlaybackEnded;

    public event EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs> PlaybackSeek;

    public event EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs> PlaybackSeekChapter;

    public event EventHandler<XbmcPlayerPlaybackSpeedChangedEventArgs> PlaybackSpeedChanged;

    internal XbmcPlayer(JsonRpcClient client)
      : base(client)
    {
      this.audio = new XbmcAudioPlayer(client);
      this.video = new XbmcVideoPlayer(client);
      this.pictures = new XbmcPicturePlayer(client);
    }

    public bool GetActivePlayers(out bool video, out bool audio, out bool picture, out int id)
    {
      this.client.LogMessage("XbmcPlayer.GetActivePlayers()");
      video = false;
      audio = false;
      picture = false;
      id = -1;
      JObject jobject = this.client.Call("Player.GetActivePlayers") as JObject;
      if (jobject == null)
      {
        this.client.LogErrorMessage("Player.GetActivePlayers(): Invalid response");
        return false;
      }
      id = (int) jobject["playerid"];
      switch ((string) jobject["type"])
      {
        case nameof (video):
          video = true;
          break;
        case nameof (audio):
          audio = true;
          break;
        case nameof (picture):
          picture = true;
          break;
      }
      return true;
    }

    internal async Task OnPlaybackStarted()
    {
      if (this.PlaybackStarted == null)
        return;
      
      this.client.LogErrorMessage("Playback has started, you silly goose!");
      await Task.Delay(500);
      XbmcMediaPlayer activePlayer = this.getActivePlayer();
      if (activePlayer == null)
      {
                this.client.LogErrorMessage("Though playback has started, I couldn't find an active player. Weird");
                return;
      }
        
      this.PlaybackStarted((object) this, new XbmcPlayerPlaybackChangedEventArgs(activePlayer));
    }

    internal void OnPlaybackPaused()
    {
      if (this.PlaybackPaused == null)
        return;
      TimeSpan current;
      TimeSpan total;
      XbmcMediaPlayer progress = this.getProgress(out current, out total);
      if (progress == null)
        return;
      this.PlaybackPaused((object) this, new XbmcPlayerPlaybackPositionChangedEventArgs(progress, current, total));
    }

    internal void OnPlaybackResumed()
    {
      if (this.PlaybackResumed == null)
        return;
      XbmcMediaPlayer activePlayer = this.getActivePlayer();
            if (activePlayer == null)
            {
                this.client.LogErrorMessage("Though playback has resumed, I couldn't find an active player. Weird");
                return;
            }

      TimeSpan currentPosition = new TimeSpan();
      TimeSpan totalLength = new TimeSpan();
      if (activePlayer is XbmcVideoPlayer)
      {
        int time1 = (int) ((XbmcVideoPlayer) activePlayer).GetTime(out currentPosition, out totalLength);
      }
      else if (activePlayer is XbmcAudioPlayer)
      {
        int time2 = (int) ((XbmcAudioPlayer) activePlayer).GetTime(out currentPosition, out totalLength);
      }
      this.PlaybackResumed((object) this, new XbmcPlayerPlaybackPositionChangedEventArgs(activePlayer, currentPosition, totalLength));
    }

    internal void OnPlaybackStopped()
    {
      if (this.PlaybackStopped == null)
        return;
      this.PlaybackStopped((object) this, (EventArgs) null);
    }

    internal void OnPlaybackEnded()
    {
      if (this.PlaybackEnded == null)
        return;
      this.PlaybackEnded((object) this, (EventArgs) null);
    }

    internal void OnPlaybackSeek()
    {
      if (this.PlaybackSeek == null)
        return;
      TimeSpan current;
      TimeSpan total;
      XbmcMediaPlayer progress = this.getProgress(out current, out total);
      if (progress == null)
        return;
      this.PlaybackSeek((object) this, new XbmcPlayerPlaybackPositionChangedEventArgs(progress, current, total));
    }

    internal void OnPlaybackSeekChapter()
    {
      if (this.PlaybackSeekChapter == null)
        return;
      TimeSpan current;
      TimeSpan total;
      XbmcMediaPlayer progress = this.getProgress(out current, out total);
      if (progress == null)
        return;
      this.PlaybackSeekChapter((object) this, new XbmcPlayerPlaybackPositionChangedEventArgs(progress, current, total));
    }

    internal void OnPlaybackSpeedChanged()
    {
      if (this.PlaybackSpeedChanged == null)
        return;
      TimeSpan current;
      TimeSpan total;
      XbmcMediaPlayer progress = this.getProgress(out current, out total);
      if (progress == null)
        return;
      this.PlaybackSpeedChanged((object) this, new XbmcPlayerPlaybackSpeedChangedEventArgs(progress, current, total, progress.Speed));
    }

    private XbmcMediaPlayer getActivePlayer()
    {
      bool video;
      bool audio;
      bool picture;
      int id;
      if (!this.GetActivePlayers(out video, out audio, out picture, out id))
        return (XbmcMediaPlayer) null;
      if (video)
        return (XbmcMediaPlayer) this.video;
      if (audio)
        return (XbmcMediaPlayer) this.audio;
      if (picture)
        return (XbmcMediaPlayer) this.pictures;
      return (XbmcMediaPlayer) null;
    }

    private XbmcMediaPlayer getProgress(out TimeSpan current, out TimeSpan total)
    {
      current = new TimeSpan();
      total = new TimeSpan();
      XbmcMediaPlayer activePlayer = this.getActivePlayer();
      if (activePlayer == null)
        return (XbmcMediaPlayer) null;
      if (activePlayer is XbmcVideoPlayer)
      {
        int time1 = (int) ((XbmcVideoPlayer) activePlayer).GetTime(out current, out total);
      }
      else if (activePlayer is XbmcAudioPlayer)
      {
        int time2 = (int) ((XbmcAudioPlayer) activePlayer).GetTime(out current, out total);
      }
      return activePlayer;
    }
  }
}
