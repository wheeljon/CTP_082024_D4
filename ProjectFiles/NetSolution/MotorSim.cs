#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using System.Reactive.Linq;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.WebUI;
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.DataLogger;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Report;
using FTOptix.RAEtherNetIP;
using FTOptix.System;

using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
#endregion

public class MotorSim : BaseNetLogic
{

    private IDisposable _SpeedSubscription;
    private IDisposable _PowerSubscription;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        Random rnd = new Random();
        var simSpeed = LogicObject.GetVariable("Speed");
        
        _SpeedSubscription = Observable
            .Interval(TimeSpan.FromSeconds(5))
            .Select(_ => new Random().NextDouble() * 100)
            .Subscribe(randomNumber => simSpeed.RemoteWrite(randomNumber)); 

        var simPower = LogicObject.GetVariable("Power");

        _PowerSubscription = Observable
            .Interval(TimeSpan.FromSeconds(15))
            .Select(_ => new Random().NextDouble() * 10)
            .Subscribe(randomNumber => simPower.RemoteWrite(randomNumber)); 

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
}
