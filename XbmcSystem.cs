// Decompiled with JetBrains decompiler
// Type: XBMC.JsonRpc.XbmcSystem
// Assembly: XBMCJsonRpcSharp, Version=0.1.0.9, Culture=neutral, PublicKeyToken=null
// MVID: E4126A99-3372-4657-847E-BEAE7622136F
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XBMCJsonRpcSharp.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace XBMC.JsonRpc
{
  public class XbmcSystem : XbmcJsonRpcNamespace
  {
    private bool checkedLabels;
    private bool canShutdown;
    private bool canSuspend;
    private bool canHibernate;
    private bool canReboot;

    public bool CanShutdown
    {
      get
      {
        this.client.LogMessage("XbmcSystem.CanShutdown");
        this.checkLabels();
        return this.canShutdown;
      }
    }

    public bool CanSuspend
    {
      get
      {
        this.client.LogMessage("XbmcSystem.CanSuspend");
        this.checkLabels();
        return this.canSuspend;
      }
    }

    public bool CanHibernate
    {
      get
      {
        this.client.LogMessage("XbmcSystem.CanHibernate");
        this.checkLabels();
        return this.canHibernate;
      }
    }

    public bool CanReboot
    {
      get
      {
        this.client.LogMessage("XbmcSystem.CanReboot");
        this.checkLabels();
        return this.canReboot;
      }
    }

    public event EventHandler ShuttingDown;

    public event EventHandler Suspending;

    public event EventHandler Hibernating;

    public event EventHandler Rebooting;

    public event EventHandler Sleeping;

    public event EventHandler Waking;

    public event EventHandler Resuming;

    public event EventHandler LowBattery;

    internal XbmcSystem(JsonRpcClient client)
      : base(client)
    {
    }

    public bool Shutdown()
    {
      this.client.LogMessage("XbmcSystem.Shutdown()");
      if (this.CanShutdown)
        return this.client.Call("System.Shutdown") != null;
      this.client.LogErrorMessage("Cannot shutdown computer running XBMC");
      return false;
    }

    public bool Suspend()
    {
      this.client.LogMessage("XbmcSystem.Suspend()");
      if (this.CanSuspend)
        return this.client.Call("System.Suspend") != null;
      this.client.LogErrorMessage("Cannot suspend computer running XBMC");
      return false;
    }

    public bool Hibernate()
    {
      this.client.LogMessage("XbmcSystem.Hibernate()");
      if (this.CanHibernate)
        return this.client.Call("System.Hibernate") != null;
      this.client.LogErrorMessage("Cannot hibernate computer running XBMC");
      return false;
    }

    public bool Reboot()
    {
      this.client.LogMessage("XbmcSystem.Reboot()");
      if (this.CanReboot)
        return this.client.Call("System.Reboot") != null;
      this.client.LogErrorMessage("Cannot reboot computer running XBMC");
      return false;
    }

    public IDictionary<string, string> GetInfoLabels(params string[] labels)
    {
      this.client.LogMessage("XBMC.GetInfoLabels(" + string.Join(", ", labels) + ")");
      if (labels == null)
        throw new ArgumentNullException(nameof (labels));
      if (labels.Length <= 0)
        throw new ArgumentException();
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty(nameof (labels), (object) new JArray((object[]) labels)));
      JObject jobject2 = this.client.Call("XBMC.GetInfoLabels", (object) jobject1) as JObject;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (jobject2 == null)
      {
        this.client.LogErrorMessage("XBMC.GetInfoLabels(" + string.Join(", ", labels) + "): invalid response");
        return (IDictionary<string, string>) dictionary;
      }
      int num = 0;
      foreach (string label in labels)
      {
        if (jobject2[label] != null)
          dictionary.Add(label, (string) jobject2[label]);
        ++num;
      }
      return (IDictionary<string, string>) dictionary;
    }

    public string GetInfoLabel(string label)
    {
      this.client.LogMessage("XBMC.GetInfoLabel(" + label + ")");
      if (label == null)
        throw new ArgumentNullException(nameof (label));
      JObject jobject1 = new JObject();
      jobject1.Add((object) new JProperty("labels", (object) new JArray((object) label)));
      JObject jobject2 = this.client.Call("XBMC.GetInfoLabels", (object) jobject1) as JObject;
      if (jobject2 != null && jobject2[label] != null)
        return (string) jobject2[label];
      this.client.LogErrorMessage("XBMC.GetInfoLabels(" + label + "): invalid response");
      return (string) null;
    }

    public IDictionary<string, bool> GetInfoBooleans(params string[] labels)
    {
      this.client.LogMessage("XBMC.GetInfoBooleans(" + string.Join(", ", labels) + ")");
      if (labels == null)
        throw new ArgumentNullException(nameof (labels));
      if (labels.Length <= 0)
        throw new ArgumentException();
      JObject jobject = this.client.Call("XBMC.GetInfoBooleans", (object[]) labels) as JObject;
      Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
      if (jobject == null)
      {
        this.client.LogErrorMessage("XBMC.GetInfoBooleans(" + string.Join(", ", labels) + "): invalid response");
        return (IDictionary<string, bool>) dictionary;
      }
      int num = 0;
      foreach (string label in labels)
      {
        if (jobject[label] != null)
          dictionary.Add(label, (bool) jobject[label]);
        ++num;
      }
      return (IDictionary<string, bool>) dictionary;
    }

    public bool GetInfoBoolean(string label)
    {
      this.client.LogMessage("XBMC.GetInfoBoolean(" + label + ")");
      if (label == null)
        throw new ArgumentNullException(nameof (label));
      JObject jobject = this.client.Call("XBMC.GetInfoBooleans", (object[]) new string[1]
      {
        label
      }) as JObject;
      if (jobject != null && jobject[label] != null)
        return (bool) jobject[label];
      this.client.LogErrorMessage("XBMC.GetInfoBooleans(" + label + "): invalid response");
      return false;
    }

    internal void OnShutdown()
    {
      if (this.ShuttingDown == null)
        return;
      this.ShuttingDown((object) this, (EventArgs) null);
    }

    internal void OnSuspend()
    {
      if (this.Suspending == null)
        return;
      this.Suspending((object) this, (EventArgs) null);
    }

    internal void OnHibernate()
    {
      if (this.Hibernating == null)
        return;
      this.Hibernating((object) this, (EventArgs) null);
    }

    internal void OnReboot()
    {
      if (this.Rebooting == null)
        return;
      this.Rebooting((object) this, (EventArgs) null);
    }

    internal void OnSleep()
    {
      if (this.Sleeping == null)
        return;
      this.Sleeping((object) this, (EventArgs) null);
    }

    internal void OnWake()
    {
      if (this.Waking == null)
        return;
      this.Waking((object) this, (EventArgs) null);
    }

    internal void OnResume()
    {
      if (this.Resuming == null)
        return;
      this.Resuming((object) this, (EventArgs) null);
    }

    internal void OnLowBattery()
    {
      if (this.LowBattery == null)
        return;
      this.LowBattery((object) this, (EventArgs) null);
    }

    private void checkLabels()
    {
      if (this.checkedLabels)
        return;
      IDictionary<string, bool> infoBooleans = this.GetInfoBooleans("System.CanShutdown", "System.CanSuspend", "System.CanHibernate", "System.CanReboot");
      if (infoBooleans.ContainsKey("System.CanShutdown"))
        this.canShutdown = infoBooleans["System.CanShutdown"];
      if (infoBooleans.ContainsKey("System.CanSuspend"))
        this.canSuspend = infoBooleans["System.CanSuspend"];
      if (infoBooleans.ContainsKey("System.CanHibernate"))
        this.canHibernate = infoBooleans["System.CanHibernate"];
      if (infoBooleans.ContainsKey("System.CanReboot"))
        this.canReboot = infoBooleans["System.CanReboot"];
      this.checkedLabels = true;
    }
  }
}
