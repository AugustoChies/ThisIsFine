using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoModule : Module
{
    public float timeToFail;
    private float countDownTimer;
    public GlobalInfo info;
    override public void OnStart()
    {
        this.type = ModuleEnum.cryo;
    }

    override public void OnUpdate()
    {
        if (isBroken)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer <= 0)
            {
                globalInfo.gameStatus = GameStatus.Failed;               
                info.defeat = true;
            }
        }
    }

    override public void OnBreak()
    {
        countDownTimer = timeToFail;
    }
}
