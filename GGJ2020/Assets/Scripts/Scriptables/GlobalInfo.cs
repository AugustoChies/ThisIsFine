using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GlobalInfo : ScriptableObject
{
    public bool victory;
    public bool defeat;
    public int currentStage;
    public float gravity;
    public float breakTimeMultiplier;
    public float progress;
    public float shipSpeed;
    public float shipSpeedMultiplier; // Between -1 and 1
    public GameStatus gameStatus;
}
