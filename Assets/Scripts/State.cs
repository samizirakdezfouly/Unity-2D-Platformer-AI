using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State  {

    //Could Require More
    private GameWorld gameWorld;
    private int visitCount;
    private int stateScore;
    public double winScore;

    public State() //Unfinished
    {
        gameWorld = new GameWorld(null);
    }

    public State(State state)
    {
        this.gameWorld = new GameWorld(state.GetGameWorld());
        this.visitCount = state.GetVisitCount();
        this.stateScore = state.GetStateScore();
        this.winScore = state.GetWinScore();
    }

    public State(GameWorld gameWorld)
    {
        this.gameWorld = new GameWorld(gameWorld);
    }

    GameWorld GetGameWorld()
    {
        return gameWorld;
    }


    public List<State> GetAvailableStates()
    {
        int i = -1;
        List<State> possibleStates = new List<State>();
        List<GameWorld.CompanionStates> availableStates = new List<GameWorld.CompanionStates>();

        foreach (GameWorld.CompanionStates state in availableStates)
        {
            i++;
            State newState = new State(this.gameWorld);
            newState.GetGameWorld().PerformAgentMove(availableStates[i]);
            possibleStates.Add(newState);
        }

        return possibleStates;
    }

    void SetGameWorld(GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;
    }

    public double GetWinScore()
    {
        return winScore;
    }

    void SetWinScore(double score)
    {
        this.winScore = score;
    }

    void AddScore(double score)
    {
        if (this.winScore != int.MaxValue)
            this.winScore += score;
    }

    public int GetVisitCount()
    {
        return visitCount;
    }

    public void SetVisitCount(int visitCount)
    {
        this.visitCount = visitCount;
    }

    void IncreaseVisit()
    {
        this.visitCount++;
    }

    int GetStateScore()
    {
        return stateScore;
    }

    void SetStateScore(int stateScore)
    {
        this.stateScore = stateScore;
    }

    void AddStateScore(int score)
    {
        if (this.stateScore != int.MinValue)
            this.stateScore += score;
    }
}
