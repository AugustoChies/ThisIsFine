using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HazardsInfos : ScriptableObject
{
    [Serializable]
    public struct HazardInfos
    {
        public Hazard.Type type;
        public Hazard prefab;
    }

    public HazardInfos[] hazardsInfos;

    public Hazard GetPrefab(Hazard.Type type)
    {
        foreach (var hazardInfos in hazardsInfos)
        {
            if (hazardInfos.type == type)
            {
                return hazardInfos.prefab;
            }
        }
        return null;
    }
}
