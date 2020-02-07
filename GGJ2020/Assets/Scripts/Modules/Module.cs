using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public GlobalInfo globalInfo;
    public ModuleHazardList moduleHazardList;
    public HazardsInfos hazardsInfos;
    public float minBreakTime;
    public float maxBreakTime;
    protected ModuleEnum type;
    private float breakCountDown = 0f;
    protected bool isBroken = false;
    private Hazard currentHazard;
    public ModuleHazardStatus status;

    public Hazard CurrentHazard
    {
        get { return currentHazard; }
    }

    private void Start()
    {
        this.OnStart();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        StartRandomBreakCountDown();
    }

    private void Update()
    {
        this.OnUpdate();
        if (!isBroken)
        {
            breakCountDown -= Time.deltaTime * globalInfo.breakTimeMultiplier;
            if (breakCountDown <= 0)
            {
                Break();
            }
        }
    }

    private void StartRandomBreakCountDown()
    {
        float randomTime = Random.Range(minBreakTime, maxBreakTime);
        breakCountDown = randomTime;
    }

    private void Break()
    {
        Hazard.Type[] possibleHazards = new Hazard.Type[0];
        foreach (var moduleType in moduleHazardList.modules)
        {
            if (moduleType.enumID == this.type)
            {
                possibleHazards = moduleType.GetPossibleHazards();
                break;
            }
        }
        if (possibleHazards.Length > 0)
        {
            Hazard.Type selectedHazardType = possibleHazards[Random.Range(0, possibleHazards.Length)];

            Vector3 hazardPosition = transform.position;
            Hazard hazardPrefab = hazardsInfos.GetPrefab(selectedHazardType);
            currentHazard = GameObject.Instantiate(hazardPrefab, hazardPosition, Quaternion.identity);

            currentHazard.onFix += this.Fix;
            this.OnBreak();
            isBroken = true;
            status.status[status.modules.IndexOf(type)] = selectedHazardType;
            this.GetComponent<AudioSource>().Play();
        }
    }

    private void Fix()
    {
        this.OnFix();
        currentHazard = null;
        StartRandomBreakCountDown();
        isBroken = false;
        status.status[status.modules.IndexOf(type)] = Hazard.Type.None;
    }

    public virtual void OnStart() {}
    public virtual void OnUpdate() {}
    public virtual void OnBreak() {}
    public virtual void OnFix() {}
}
