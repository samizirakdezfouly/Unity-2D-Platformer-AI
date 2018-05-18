using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompanionStates {

    void OnStateEnter(CompanionBehaviour companion, CompanionSensor sensor);
    void OnStateExit();
    void ExecuteState();
    string RaycastCheck2D(RaycastHit2D raycast);
}
