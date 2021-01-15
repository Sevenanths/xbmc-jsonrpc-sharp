// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.SocketStateObject
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using System.Text;

namespace XBMC.JsonRpc
{
  internal class SocketStateObject
  {
    public byte[] Buffer = new byte[1024];
    public StringBuilder Builder = new StringBuilder();
    public const int BufferSize = 1024;
  }
}
