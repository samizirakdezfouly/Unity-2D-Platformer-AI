﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyStates {

    private EnemyBehaviour enemy;
    private EnemySensor sensor;

    public void OnStateEnter(EnemyBehaviour enemy, EnemySensor sensor)
    {
        this.enemy = enemy;
        this.sensor = sensor;
    }

    public void ExecuteState()
    {
        Debug.Log("Enemy Is Patroling");
    }

    public void OnStateExit()
    {
        return;
    }

    public string RaycastCheck2D(RaycastHit2D raycast)
    {
        throw new System.NotImplementedException();
    }
}