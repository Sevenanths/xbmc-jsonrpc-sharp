// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcMediaLibrary
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcMediaLibrary : XbmcJsonRpcNamespace
  {
    private string libraryName;

    protected XbmcMediaLibrary(string libraryName, JsonRpcClient client)
      : base(client)
    {
      if (string.IsNullOrEmpty(libraryName))
        throw new ArgumentException();
      this.libraryName = libraryName;
    }

    public virtual bool ScanForContent()
    {
      this.client.LogMessage("Xbmc" + this.libraryName + ".ScanForContent()");
      return this.client.Call(this.libraryName + ".ScanForContent") != null;
    }
  }
}
