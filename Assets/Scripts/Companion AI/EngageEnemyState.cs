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

        companion.EngageEnemy();

        switch (RaycastCheck2D(sensor.frontRaycast))
        {
            case null:
                companion.ChangeCompanionState(new FollowPlayerState());
                break;
            case "Health Pack":
                companion.ChangeCompanionState(new ScavangeState());
                break;
            case "Health Pack(Clone)":
                companion.ChangeCompanionState(new ScavangeState());
                break;
            case "Crate":
                companion.ChangeCompanionState(new DefensiveState());
                break;
        }
    }

    public void OnStateExit()
    {
        return;
    }

    public string RaycastCheck2D(RaycastHit2D raycast)
    {
        if (raycast)
            return raycast.collider.name;
        else
            return null;
    }
}
