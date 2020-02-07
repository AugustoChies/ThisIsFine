using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSModule : Module
{
    public float timeToChange;
    private IEnumerator timerCoroutine;

    override public void OnStart()
    {
        this.type = ModuleEnum.GPS;
    }

    override public void OnBreak()
    {
        timerCoroutine = CountDownToChange();
        StartCoroutine(timerCoroutine);
    }

    override public void OnFix()
    {
        StopCoroutine(timerCoroutine);
        globalInfo.shipSpeedMultiplier = 1f;
    }

    private IEnumerator CountDownToChange()
    {
        while (true)
        {
            globalInfo.shipSpeedMultiplier = Random.Range(-2f, 1.5f);
            yield return new WaitForSeconds(timeToChange);
        }
    }
}
