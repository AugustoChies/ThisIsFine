using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : Module
{
    override public void OnStart()
    {
        this.type = ModuleEnum.shield;
    }

    override public void OnBreak()
    {
        globalInfo.breakTimeMultiplier = 1.5f;
    }

    override public void OnFix()
    {
        globalInfo.breakTimeMultiplier = 1f;
    }
}
