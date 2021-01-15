// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.JsonRpcClient
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace XBMC.JsonRpc
{
  public class JsonRpcClient
  {
    private const int CallIdMaximum = 100000;
    private const string JsonResponseError = "error";
    private const string JsonResponseResult = "result";
    private int callId;
    private Uri uri;
    private string username;
    private string password;
    private int timeout;

    public event EventHandler<XbmcJsonRpcLogEventArgs> Log;

    public event EventHandler<XbmcJsonRpcLogErrorEventArgs> LogError;

    internal Uri Uri
    {
      get
      {
        return this.uri;
      }
    }

    internal string Username
    {
      get
      {
        return this.username;
      }
    }

    internal string Password
    {
      get
      {
        return this.password;
      }
    }

    internal int Timeout
    {
      get
      {
        return this.timeout;
      }
      set
      {
        if (value < 1000)
          value = 1000;
        this.timeout = value;
      }
    }

    public JsonRpcClient(Uri uri, string username, string password)
    {
      if (uri == (Uri) null)
        throw new ArgumentNullException(nameof (uri));
      this.uri = uri;
      this.username = username;
      this.password = password;
      this.timeout = 5000;
    }

    public object Call(string method)
    {
      return this.Call(method, (object) null);
    }

    public virtual object Call(string method, object args)
    {
      if (string.IsNullOrEmpty(method))
        throw new ArgumentException();
      this.LogMessage("Calling JSON RPC method \"" + method + "\"...");
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(this.uri);
        httpWebRequest.AllowWriteStreamBuffering = true;
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Credentials = (ICredentials) new NetworkCredential(this.username, this.password);
        httpWebRequest.KeepAlive = false;
        httpWebRequest.Method = "POST";
        httpWebRequest.Timeout = this.timeout;
        using (Stream requestStream = httpWebRequest.GetRequestStream())
        {
          using (StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.ASCII))
          {
            if (this.callId >= 100000)
              this.callId = 0;
            ++this.callId;
            JObject jobject = new JObject();
            jobject.Add((object) new JProperty("jsonrpc", (object) "2.0"));
            jobject.Add((object) new JProperty(nameof (method), (object) method));
            if (args != null)
              jobject.Add((object) new JProperty("params", args));
            jobject.Add((object) new JProperty("id", (object) this.callId));
            this.LogMessage("JSON RPC call: " + jobject.ToString());
            streamWriter.Write(jobject.ToString());
          }
        }
        using (WebResponse response = httpWebRequest.GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
              return this.parseResponse(reader);
          }
        }
      }
      catch (Exception ex)
      {
        this.LogErrorMessage("Error while calling JSON RPC method \"" + method + "\"", ex);
        return (object) null;
      }
    }

    public object Call(string method, IDictionary args)
    {
      return this.Call(method, (object) args);
    }

    public object Call(string method, params object[] args)
    {
      return this.Call(method, (object) args);
    }

    public static TType GetField<TType>(JObject obj, string field)
    {
      return JsonRpcClient.GetField<TType>(obj, field, default (TType));
    }

    public static TType GetField<TType>(JObject obj, string field, TType defaultValue)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (string.IsNullOrEmpty(field))
        throw new ArgumentException();
      try
      {
        return (TType) Convert.ChangeType(obj[field].Value<JValue>().Value, typeof (TType));
      }
      catch (Exception ex)
      {
        return defaultValue;
      }
    }

    internal void LogMessage(string message)
    {
      if (string.IsNullOrEmpty(message) || this.Log == null)
        return;
      this.Log((object) this, new XbmcJsonRpcLogEventArgs(message));
    }

    internal void LogErrorMessage(string message)
    {
      this.LogErrorMessage(message, (Exception) null);
    }

    internal void LogErrorMessage(string message, Exception exception)
    {
      if (string.IsNullOrEmpty(message) || this.LogError == null)
        return;
      this.LogError((object) this, new XbmcJsonRpcLogErrorEventArgs(message, exception));
    }

    private object parseResponse(StreamReader reader)
    {
      string end = reader.ReadToEnd();
      this.LogMessage("JSON RPC response: " + end);
      foreach (JProperty property in JObject.Parse(end).Properties())
      {
        if (string.CompareOrdinal(property.Name, "error") == 0)
          this.parseError(property.Value as JObject);
        if (string.CompareOrdinal(property.Name, "result") == 0)
        {
          if (property.Value.HasValues)
          {
            if (property.Value.Type == JTokenType.Array)
              return (object) (property.Value[(object) 0] as JObject);
            return (object) (property.Value as JObject);
          }
          if (property.Value.Type == JTokenType.Integer)
            return (object) (int) ((JToken) property.Value.Value<JValue>());
          if (property.Value.Type == JTokenType.Float)
            return (object) (float) ((JToken) property.Value.Value<JValue>());
          if (property.Value.Type == JTokenType.String)
            return (object) property.Value.Value<JValue>().Value.ToString();
          if (property.Value.Type != JTokenType.Array)
            return (object) property.Value.Value<JValue>();
          property.Value.Value<JArray>();
          return (object) property.Value.Value<JArray>();
        }
      }
      this.LogErrorMessage("Invalid JSON RPC response: " + end);
      throw new InvalidJsonRpcResponseException(end);
    }

    private void parseError(JObject error)
    {
      this.LogErrorMessage("JSON RPC error received: " + (object) error != null ? error.ToString() : "unknown");
      if (error == null)
        throw new UnknownJsonRpcErrorException();
      throw new JsonRpcErrorException(JsonRpcClient.GetField<int>(error, "code"), JsonRpcClient.GetField<string>(error, "message"));
    }
  }
}
