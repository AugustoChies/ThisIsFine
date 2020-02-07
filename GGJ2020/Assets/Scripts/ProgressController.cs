using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public GlobalInfo globalInfo;

    private void Update()
    {
        globalInfo.progress += globalInfo.shipSpeed * Time.deltaTime;
        if (globalInfo.progress >= 1)
        {
            Debug.Log("Success");
            // TODO: end game with success
            globalInfo.gameStatus = GameStatus.Success;
        }
    }
}
