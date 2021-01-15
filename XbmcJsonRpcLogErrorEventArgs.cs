// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcJsonRpcLogErrorEventArgs
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class XbmcJsonRpcLogErrorEventArgs : XbmcJsonRpcLogEventArgs
  {
    private Exception exception;

    public Exception Exception
    {
      get
      {
        return this.exception;
      }
    }

    public XbmcJsonRpcLogErrorEventArgs(string message)
      : base(message)
    {
    }

    public XbmcJsonRpcLogErrorEventArgs(string message, Exception exception)
      : base(message)
    {
      this.exception = exception;
    }
  }
}
