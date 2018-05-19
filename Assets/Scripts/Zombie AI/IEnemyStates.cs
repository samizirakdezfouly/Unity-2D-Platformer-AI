using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStates {

    void OnStateEnter(EnemyBehaviour enemy, EnemySensor sensor);
    void OnStateExit();
    void ExecuteState();
    string RaycastCheck2D(RaycastHit2D raycast);
}
