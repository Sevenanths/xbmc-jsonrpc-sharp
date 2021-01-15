// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcLibrary
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

namespace XBMC.JsonRpc
{
  public class XbmcLibrary : XbmcJsonRpcNamespace
  {
    private XbmcAudioLibrary audio;
    private XbmcVideoLibrary video;

    public XbmcAudioLibrary Audio
    {
      get
      {
        return this.audio;
      }
    }

    public XbmcVideoLibrary Video
    {
      get
      {
        return this.video;
      }
    }

    internal XbmcLibrary(JsonRpcClient client)
      : base(client)
    {
      this.audio = new XbmcAudioLibrary(client);
      this.video = new XbmcVideoLibrary(client);
    }
  }
}
