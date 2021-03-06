﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveState : ICompanionStates {

    private CompanionBehaviour companion;
    private CompanionSensor sensor;

    public void OnStateEnter(CompanionBehaviour companion, CompanionSensor sensor)
    {
        this.companion = companion;
        this.sensor = sensor;
    }

    public void ExecuteState()
    {
        Debug.Log("Companion Is Playing Defensively");

        companion.GetBehindCover();

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
            case "Ammo Pack":
                companion.ChangeCompanionState(new ScavangeState());
                break;
            case "Ammo Pack(Clone)":
                companion.ChangeCompanionState(new ScavangeState());
                break;
            case "Zombie":
                companion.ChangeCompanionState(new EngageEnemyState());
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
