// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.InvalidJsonRpcResponseException
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System;

namespace XBMC.JsonRpc
{
  public class InvalidJsonRpcResponseException : Exception
  {
    private string response;

    public string Response
    {
      get
      {
        return this.response;
      }
    }

    internal InvalidJsonRpcResponseException(string response)
      : this(response, (string) null)
    {
    }

    internal InvalidJsonRpcResponseException(string response, string message)
      : this(response, message, (Exception) null)
    {
    }

    internal InvalidJsonRpcResponseException(
      string response,
      string message,
      Exception innerException)
      : base(message, innerException)
    {
      this.response = response;
    }
  }
}
