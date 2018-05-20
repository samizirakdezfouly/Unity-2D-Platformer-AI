using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnagagePlayerState : IEnemyStates
{
    private EnemyBehaviour enemy;
    private EnemySensor sensor;

    public void OnStateEnter(EnemyBehaviour enemy, EnemySensor sensor)
    {
        this.enemy = enemy;
        this.sensor = sensor;
    }

    public void ExecuteState()
    {
        //Debug.Log("Enemy Is Engaging Player");

        enemy.EngageEnemy();

        if (!RaycastCheck2D(sensor.playerDetection) || enemy.playerHealth.health <= 0)
            enemy.ChangeEnemyState(new PatrolState());
    }

    public void OnStateExit()
    {
        enemy.ResetEnemy();
    }

    public bool RaycastCheck2D(RaycastHit2D raycast)
    {
        if (raycast)
            return true;
        else
            return false;
    }
}
