// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcFileSource
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web;

namespace XBMC.JsonRpc
{
  public class XbmcFileSource
  {
    private const string MultiPath = "multipath://";
    private string label;
    private ICollection<string> paths;
    private string fanart;

    public string Label
    {
      get
      {
        return this.label;
      }
    }

    public ICollection<string> Paths
    {
      get
      {
        return this.paths;
      }
    }

    public string Fanart
    {
      get
      {
        return this.fanart;
      }
    }

    private XbmcFileSource(string label, string paths, string fanart)
    {
      this.label = label;
      this.paths = (ICollection<string>) new List<string>();
      this.fanart = fanart;
      if (paths.StartsWith("multipath://"))
      {
        string str1 = HttpUtility.UrlDecode(paths.Remove(0, "multipath://".Length));
        char[] chArray = new char[1]{ '/' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (!string.IsNullOrEmpty(str2))
            this.paths.Add(str2);
        }
      }
      else
        this.paths.Add(paths);
    }

    internal XbmcFileSource(string label, ICollection<string> paths, string fanart)
    {
      this.label = label;
      this.paths = paths;
      this.fanart = fanart;
    }

    internal static XbmcFileSource FromJson(JObject obj)
    {
      if (obj == null)
        return (XbmcFileSource) null;
      return new XbmcFileSource((string) obj["label"], (string) obj["file"], (string) obj["fanart"]);
    }
  }
}
