#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.CommunicationDriver;
using FTOptix.NativeUI;
using FTOptix.WebUI;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.RAEtherNetIP;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Core;
using System.Linq;
using System.Collections.Generic;
#endregion

public class AppLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        
    }
    [ExportMethod]
    public void OpenPanelOn(NodePointer aPanel,DynamicLink aScreen) 
    {
        Log.Verbose1("It did work - " + aPanel.BrowseName + " - " + aScreen.BrowseName);
        CloseAllDialogs();
        var PanelLoader = Session.Get<PanelLoader>(aPanel.BrowseName);
        PanelLoader.ChangePanel(aScreen); 
    }

    [ExportMethod]
    public void Demo(string anode, string aAlias) {}
    [ExportMethod]
    public void CloseAllDialogs()
    {
        // DialogBoxes are children of the current Session, so we can iterate in
        // children to get all of them
        // foreach (Dialog item in Session.Get("UIRoot").Children.OfType<Dialog>().ToList()) {
        //     //Log.Info("Closing: " + item.BrowseName);
        //     item.Close();
        // }
        Log.Verbose1("Found this many popups:" + Session.Get("UI/Screens/Popups").Children.OfType<popMotor>().Count());
        foreach (pop_Template item in Session.Get("UI/Screens/Popups").Children.OfType<pop_Template>().ToList()) {
            Log.Info("Closing: " + item.BrowseName);
            item.Close();
        }
    }

    [ExportMethod]
    public void CloseAllPopUps()
    {
        // DialogBoxes are children of the current Session, so we can iterate in
        // children to get all of them
        foreach (Dialog item in Session.Get("UIRoot").Children.OfType<Dialog>().ToList())
        {
            if (item.BrowseName.IndexOf("pop_") > 0)
            {
                item.Close();
            }
        }
    }
    [ExportMethod]
    public void CloseAllManWindows()
    {
        // DialogBoxes are children of the current Session, so we can iterate in
        // children to get all of them
        foreach (Dialog item in Session.Get("UIRoot").Children.OfType<Dialog>().ToList())
        {
            if (item.BrowseName.IndexOf("man_") > 0)
            {
                item.Close();
            }
        }
    }
    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

}
