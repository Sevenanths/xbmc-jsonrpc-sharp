// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcFiles
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcFiles : XbmcJsonRpcNamespace
  {
    internal XbmcFiles(JsonRpcClient client)
      : base(client)
    {
    }

    public ICollection<XbmcFileSource> GetSources(XbmcMediaType mediaType)
    {
      this.client.LogMessage("XbmcFiles.GetSources(" + (object) mediaType + ")");
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("media", (object) XbmcFiles.getMediaType(mediaType)));
      JObject jobject2 = this.client.Call("Files.GetSources", (object) jobject1) as JObject;
      List<XbmcFileSource> xbmcFileSourceList = new List<XbmcFileSource>();
      if (jobject2 == null || jobject2["shares"] == null)
      {
        this.client.LogErrorMessage("Files.GetSources(" + XbmcFiles.getMediaType(mediaType) + "): Invalid response");
        return (ICollection<XbmcFileSource>) xbmcFileSourceList;
      }
      foreach (JObject jobject3 in (IEnumerable<JToken>) jobject2["shares"])
      {
        XbmcFileSource xbmcFileSource = XbmcFileSource.FromJson(jobject3);
        if (xbmcFileSource != null)
          xbmcFileSourceList.Add(xbmcFileSource);
      }
      return (ICollection<XbmcFileSource>) xbmcFileSourceList;
    }

    public string GetDownloadUrl(string file)
    {
      this.client.LogMessage("XbmcFiles.GetDownloadUrl(" + file + ")");
      JObject jobject = this.client.Call("Files.Download", (object) file) as JObject;
      if (jobject != null && jobject["path"] != null)
        return (string) jobject["path"];
      this.client.LogErrorMessage("Files.Download(" + file + "): Invalid response");
      return (string) null;
    }

    public string GetDirectory(string directory, XbmcMediaType mediaType)
    {
      this.client.LogMessage("XbmcFiles.GetDirectory(" + directory + ", " + (object) mediaType + ")");
      if (string.IsNullOrEmpty(directory))
        throw new ArgumentException();
      JObject jobject = new JObject();
      jobject.Add((object) new JProperty(nameof (directory), (object) directory));
      jobject.Add((object) new JProperty("media", (object) XbmcFiles.getMediaType(mediaType)));
      object obj = this.client.Call("Files.GetDirectory", (object) jobject);
      if (obj != null)
        return (string) obj;
      this.client.LogErrorMessage("Files.GetDirectory(" + directory + ", " + XbmcFiles.getMediaType(mediaType) + "): Invalid response");
      return string.Empty;
    }

    private static string getMediaType(XbmcMediaType mediaType)
    {
      switch (mediaType)
      {
        case XbmcMediaType.Video:
          return "video";
        case XbmcMediaType.Audio:
          return "music";
        case XbmcMediaType.Picture:
          return "pictures";
        case XbmcMediaType.File:
          return "files";
        default:
          return (string) null;
      }
    }
  }
}
