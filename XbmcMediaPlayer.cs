// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcMediaPlayer
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;

namespace XBMC.JsonRpc
{
  public class XbmcMediaPlayer : XbmcJsonRpcNamespace
  {
    private int id;
    private string playerName;
    private string infoLabelName;

    protected XbmcPlayerState state
    {
      get
      {
        this.client.LogMessage("XbmcMediaPlayer.State");
        return this.parsePlayerState(this.getPlayerProperties((object) "speed", (object) "partymode"));
      }
    }

    protected XbmcMediaPlayer(string playerName, JsonRpcClient client, int id)
      : this(playerName, (string) null, client, id)
    {
    }

    protected XbmcMediaPlayer(
      string playerName,
      string infoLabelName,
      JsonRpcClient client,
      int id)
      : base(client)
    {
      if (string.IsNullOrEmpty(playerName))
        throw new ArgumentException();
      if (string.IsNullOrEmpty(infoLabelName))
        infoLabelName = playerName;
      this.playerName = playerName;
      this.infoLabelName = infoLabelName;
      int num;
      if (id != -1)
        num = id;
      else
        id = num = 1;
      this.id = num;
    }

    public virtual int Speed
    {
      get
      {
        this.client.LogMessage("XbmcMediaPlayer.Speed");
        return this.getPlaySpeed();
      }
    }

    public virtual bool Random
    {
      get
      {
        this.client.LogMessage("XbmcMediaPlayer.Random");
        JObject playerProperties = this.getPlayerProperties((object) "shuffled");
        if (playerProperties != null && playerProperties["shuffled"] != null)
          return (bool) playerProperties["shuffled"];
        return false;
      }
    }

    public virtual XbmcRepeatTypes Repeat
    {
      get
      {
        this.client.LogMessage("XbmcMediaPlayer.Repeat");
        JObject playerProperties = this.getPlayerProperties((object) nameof (Repeat));
        if (playerProperties != null && playerProperties[nameof (Repeat)] != null)
        {
          if ((string) playerProperties[nameof (Repeat)] == "One")
            return XbmcRepeatTypes.One;
          if ((string) playerProperties[nameof (Repeat)] == "All")
            return XbmcRepeatTypes.All;
        }
        return XbmcRepeatTypes.Off;
      }
    }

    protected XbmcPlayerState getTime(
      out TimeSpan currentPosition,
      out TimeSpan totalLength)
    {
      this.client.LogMessage("Xbmc" + this.playerName + ".GetTime()");
      currentPosition = new TimeSpan();
      totalLength = new TimeSpan();
      JObject playerProperties = this.getPlayerProperties((object) "time", (object) "totaltime", (object) "speed");
      if (playerProperties != null && playerProperties["time"] != null && (playerProperties["totaltime"] != null && playerProperties["speed"] != null))
      {
        JObject jobject1 = playerProperties["time"] as JObject;
        JObject jobject2 = playerProperties["totaltime"] as JObject;
        int num = (int) playerProperties["speed"];
        currentPosition = new TimeSpan((int) jobject1["hours"], (int) jobject1["minutes"], (int) jobject1["seconds"]);
        totalLength = new TimeSpan((int) jobject2["hours"], (int) jobject2["minutes"], (int) jobject2["seconds"]);
        return num > 0 ? XbmcPlayerState.Playing : XbmcPlayerState.Paused;
      }
      this.client.LogErrorMessage("Xbmc" + this.playerName + ".GetTime(): Invalid response");
      return XbmcPlayerState.Unavailable;
    }

    protected double getPercentage()
    {
      this.client.LogMessage("Xbmc" + this.playerName + ".GetPercentage()");
      JObject jobject = new JObject()
      {
        {
          "playerid",
          (JToken) this.id
        },
        {
          "properties",
          (JToken) new JArray((object) "percentage")
        }
      };
      JObject playerProperties = this.getPlayerProperties((object) "percentage");
      if (playerProperties != null || playerProperties["percentage"] != null)
        return (double) playerProperties["percentage"];
      this.client.LogErrorMessage(this.playerName + ".GetPercentage(): Invalid response");
      return -1.0;
    }

    protected JObject getPlayerProperties(params object[] properties)
    {
      return this.client.Call("Player.GetProperties", (object) new JObject()
      {
        {
          "playerid",
          (JToken) this.id
        },
        {
          nameof (properties),
          (JToken) new JArray(properties)
        }
      }) as JObject;
    }

    protected int getPlaySpeed()
    {
      JObject playerProperties = this.getPlayerProperties((object) "speed");
      if (playerProperties != null && playerProperties["speed"] != null)
        return (int) playerProperties["speed"];
      return 0;
    }

    protected XbmcPlayerState parsePlayerState(JObject obj)
    {
      if (obj == null || obj["speed"] == null || obj["partymode"] == null)
        return XbmcPlayerState.Unavailable;
      XbmcPlayerState xbmcPlayerState = (int) obj["speed"] <= 0 ? XbmcPlayerState.Paused : XbmcPlayerState.Playing;
      if ((bool) obj["partymode"])
        xbmcPlayerState |= XbmcPlayerState.PartyMode;
      return xbmcPlayerState;
    }
  }
}
