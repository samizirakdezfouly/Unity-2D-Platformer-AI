using System.Collections;
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

        enemy.Patrol();

        if (RaycastCheck2D(sensor.playerDetection))
            enemy.ChangeEnemyState(new EnagagePlayerState());

    }

    public void OnStateExit()
    {
        return;
    }

    public bool RaycastCheck2D(RaycastHit2D raycast)
    {
        if (raycast)
            return true;
        else
            return false;

    }
}
