using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleEnum { cryo, gravity, propulsion, GPS, shield, monitoring}


[System.Serializable]
public class ModuleTypes
{
    public ModuleEnum enumID;
    public bool fire, virus, shortCircuit, broken, lowEnergy;

    public Hazard.Type[] GetPossibleHazards()
    {
        List<Hazard.Type> possibleHazards = new List<Hazard.Type>();
        if (fire) possibleHazards.Add(Hazard.Type.Fire);
        if (virus) possibleHazards.Add(Hazard.Type.Virus);
        if (shortCircuit) possibleHazards.Add(Hazard.Type.ShortCircuit);
        if (broken) possibleHazards.Add(Hazard.Type.BrokenPiece);
        if (lowEnergy) possibleHazards.Add(Hazard.Type.LowEnergy);
        return possibleHazards.ToArray();
    }
}
