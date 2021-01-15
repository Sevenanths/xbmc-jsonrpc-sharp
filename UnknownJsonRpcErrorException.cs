// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.UnknownJsonRpcErrorException
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class UnknownJsonRpcErrorException : Exception
  {
    internal UnknownJsonRpcErrorException()
    {
    }

    internal UnknownJsonRpcErrorException(string message)
      : base(message)
    {
    }

    internal UnknownJsonRpcErrorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
