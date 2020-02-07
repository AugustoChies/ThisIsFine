using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityModule : Module
{
    public float lowGravity = 0.1f;
    private float defaultGravity;

    override public void OnStart()
    {
        this.type = ModuleEnum.gravity;
        defaultGravity = globalInfo.gravity;
    }

    override public void OnBreak()
    {
        globalInfo.gravity = lowGravity;
    }

    override public void OnFix()
    {
        globalInfo.gravity = defaultGravity;
    }
}
