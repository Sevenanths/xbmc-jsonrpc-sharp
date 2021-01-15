// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.JsonRpcErrorException
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class JsonRpcErrorException : Exception
  {
    private int code;

    public int Code
    {
      get
      {
        return this.code;
      }
    }

    internal JsonRpcErrorException(int code, string message)
      : this(code, message, (Exception) null)
    {
    }

    internal JsonRpcErrorException(int code, string message, Exception innerException)
      : base(message, innerException)
    {
      this.code = code;
    }
  }
}
