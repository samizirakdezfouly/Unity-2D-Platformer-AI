using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour {

    public Cover detectedCover;

    public List<CompanionStates> possibleStates;

    public CompanionStates currentState;

    public enum CompanionStates
    {
        Idle,
        FindCover,
        BehindCover,
        EngageEnemy,
        InCombat,
        LocatingLoot,
        Scavenging,
        Fleeing
    }

   public GameWorld(GameWorld gameWorld)
   {
       
   }

    public void PerformAgentMove(CompanionStates state)
    {

    }

    public List<CompanionStates> GetPossibleStates()
    {
        return possibleStates;
    }

	void Start ()
    {
        currentState = CompanionStates.Idle;       
	}
	
	void FixedUpdate ()
    {
	    switch(currentState)
        {
            case CompanionStates.Idle:
                //print("Companion Is Idle");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.Idle);
                possibleStates.Add(CompanionStates.FindCover);
                possibleStates.Add(CompanionStates.EngageEnemy);
                possibleStates.Add(CompanionStates.LocatingLoot);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.FindCover:
                //print("Companion Is Finding Cover");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.FindCover);
                possibleStates.Add(CompanionStates.BehindCover);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.BehindCover:
                //print("Companion Is Behind Cover");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.FindCover);
                possibleStates.Add(CompanionStates.BehindCover);
                possibleStates.Add(CompanionStates.EngageEnemy);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.EngageEnemy:
                //print("Companion Will Engage Enemy");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.EngageEnemy);
                possibleStates.Add(CompanionStates.InCombat);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.InCombat:
                //print("Companion Is In Combat");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.FindCover);
                possibleStates.Add(CompanionStates.EngageEnemy);
                possibleStates.Add(CompanionStates.InCombat);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.LocatingLoot:
                //print("Companion Is Locating Loot");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.LocatingLoot);
                possibleStates.Add(CompanionStates.Scavenging);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.Scavenging:
                //print("Companion Is Scavenging");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.Idle);
                possibleStates.Add(CompanionStates.FindCover);
                possibleStates.Add(CompanionStates.EngageEnemy);
                possibleStates.Add(CompanionStates.LocatingLoot);
                possibleStates.Add(CompanionStates.Scavenging);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
            case CompanionStates.Fleeing:
                //print("Companion Is Fleeing");
                possibleStates.Clear();
                possibleStates.Add(CompanionStates.Idle);
                possibleStates.Add(CompanionStates.FindCover);
                possibleStates.Add(CompanionStates.EngageEnemy);
                possibleStates.Add(CompanionStates.LocatingLoot);
                possibleStates.Add(CompanionStates.Fleeing);
                break;
        }
	}
}
