#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
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
using System.Security.Cryptography.X509Certificates;
#endregion

public class ClassDT : BaseNetLogic
{
    [ExportMethod]
    public void CreateVar(string aTagName)
    {
        // Insert code to be executed by the method
        //Project.Current will return the tree that is aval in your dev  enviro.
        Folder modelFolder = Project.Current.Get<Folder>("Model");
        if (modelFolder.Get(aTagName) == null) 
        {
            IUAVariable myNewVariable = InformationModel.MakeVariable(aTagName,OpcUa.DataTypes.Int32,null);
            //Where are you going to put this new Variable.
            modelFolder.Add(myNewVariable);
        }
        else
        {
            Log.Error(LogicObject.BrowseName, "Variable already Exists");
        }
        Log.Info(LogicObject.BrowseName, "Code executed successfuly!");
        
        // IUAObject myNewVariable2 = InformationModel.MakeObject<Motor>("M594",Motor.ReferenceEquals(objectTypeId));
        // modelFolder.Add(myNewVariable2);
    }

    [ExportMethod]
    public void CreateVars()
    {
        int varsToCreate = LogicObject.GetVariable("VariablesToCreate").Value;
        NodeId dataTypeToCreate = LogicObject.GetVariable("DataTypeToCreate").Value;
        string DestinationFolder = LogicObject.GetVariable("DestinationFolder").Value;
        string BaseVarName = LogicObject.GetVariable("BaseVarName").Value;
        Folder modelFolder = Project.Current.Get<Folder>(DestinationFolder);

        for (int i=1; i <=varsToCreate;i++)
        {
            string aTagName = string.Concat(BaseVarName , i);
        if (modelFolder.Get(aTagName) == null) 
        {
            IUAVariable myNewVariable = InformationModel.MakeVariable(aTagName,dataTypeToCreate);
            //Where are you going to put this new Variable.
            modelFolder.Add(myNewVariable);
        }
        else
        {
            Log.Error(LogicObject.BrowseName, "Variable already Exists");
        }
        Log.Info(LogicObject.BrowseName, "Added - " + aTagName);
        }
    }
}
