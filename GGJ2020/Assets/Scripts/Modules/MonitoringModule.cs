using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitoringModule : Module
{
    override public void OnStart()
    {
        this.type = ModuleEnum.monitoring;
    }
}
