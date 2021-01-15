// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcJsonRpcLogEventArgs
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcJsonRpcLogEventArgs : EventArgs
  {
    private string message;

    public string Message
    {
      get
      {
        return this.message;
      }
    }

    public XbmcJsonRpcLogEventArgs(string message)
    {
      this.message = message;
    }
  }
}
