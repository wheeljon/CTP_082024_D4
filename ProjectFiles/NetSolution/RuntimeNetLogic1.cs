#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.RAEtherNetIP;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.WebUI;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.NetLogic;
using System.Reactive.Linq;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using System.Reflection.Metadata.Ecma335;
using FTOptix.Alarm;
using FTOptix.EventLogger;
using FTOptix.Recipe;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    private IDisposable _randomNumberSubscription;
 
    public override void Start()
    {
        Random rnd = new Random();
        var randomNumberVariable = Project.Current.GetVariable("Model/RandomNumber");
        
 
        _randomNumberSubscription = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Select(_ => new Random().NextDouble() * 100)
            .Subscribe(randomNumber => randomNumberVariable.RemoteWrite(randomNumber)); 

        var DebugString = Project.Current.GetVariable("Model/strDebug");
        DebugString.RemoteWrite(Project.Current.GetVariable(this.Owner.Children.GetVariable("typMotor/Speed").BrowseName).Value);
        var Speed = this.Owner.Children.GetVariable("typMotor/Speed");
        Speed.RemoteWrite(rnd.Next(100));
        var Power = this.Owner.Children.GetVariable("{typMotor}/Power");
        Power.RemoteWrite(rnd.Next(10));          
    }
 
    public override void Stop()
    {
        _randomNumberSubscription.Dispose();
    }
    
    private void Time_VariableChange(object sender, VariableChangeEventArgs e)
{
        Random rnd = new Random();
        
        var RandomNumber = Project.Current.GetVariable("Model/RandomNumber");
        RandomNumber.RemoteWrite(rnd.Next(100));
  

     
       
}
}
