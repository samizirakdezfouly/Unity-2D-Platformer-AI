using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageEnemyState : ICompanionStates {

    private CompanionBehaviour companion;
    private CompanionSensor sensor;

    public void OnStateEnter(CompanionBehaviour companion, CompanionSensor sensor)
    {
        this.companion = companion;
        this.sensor = sensor;
    }

    public void ExecuteState()
    {
        Debug.Log("Companion Is Engaging An Enemy");
    }

    public void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public string RaycastCheck2D(RaycastHit2D raycast)
    {
        throw new System.NotImplementedException();
    }
}
