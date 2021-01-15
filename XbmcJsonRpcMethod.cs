// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcJsonRpcMethod
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;

namespace XBMC.JsonRpc
{
  public class XbmcJsonRpcMethod
  {
    private string name;
    private string description;
    private string permission;
    private bool executable;

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public string Description
    {
      get
      {
        return this.description;
      }
    }

    public string Permission
    {
      get
      {
        return this.permission;
      }
    }

    public bool Executable
    {
      get
      {
        return this.executable;
      }
    }

    private XbmcJsonRpcMethod(string name, string description, string permission, bool executable)
    {
      this.name = name;
      this.description = description;
      this.permission = permission;
      this.executable = executable;
    }

    internal static XbmcJsonRpcMethod FromJson(JObject obj)
    {
      if (obj == null)
        return (XbmcJsonRpcMethod) null;
      return new XbmcJsonRpcMethod((string) obj["command"], (string) obj["description"], (string) obj["permission"], (bool) obj["executable"]);
    }
  }
}
