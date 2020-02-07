using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterModule : Module
{
    private float defaultShipSpeed;

    override public void OnStart()
    {
        this.type = ModuleEnum.propulsion;
        defaultShipSpeed = globalInfo.shipSpeed;
    }

    override public void OnBreak()
    {
        globalInfo.shipSpeed = 0;
    }

    override public void OnFix()
    {
        globalInfo.shipSpeed = defaultShipSpeed;
    }
}
