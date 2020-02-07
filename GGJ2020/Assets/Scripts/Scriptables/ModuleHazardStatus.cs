using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ModuleHazardStatus : ScriptableObject
{
    public List<ModuleEnum> modules;
    public List<Hazard.Type> status;
}
