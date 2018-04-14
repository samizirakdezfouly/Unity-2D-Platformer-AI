using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Node  {
    //Complete
    State state;
    Node parent;
    List<Node> childArray; 


    public Node() //Complete
    {
        state = new State();
        childArray = new List<Node>();
    }

    public Node (State state) //Complete
    {
        this.state = state;
        this.childArray = new List<Node>();
    } 

    public Node (State state, Node parent, List<Node> childArray) //Complete
    {
        this.state = state;
        this.parent = parent;
        this.childArray = childArray;
    }

    public Node (Node node) //Complete
    {
        this.childArray = new List<Node>();
        this.state = new State(node.GetState());

        if(node.GetParent() != null)
        {
            parent = node.GetParent();
        }

        List<Node> childArray = node.GetChildArray();

        foreach (Node child in childArray)
        {
            this.childArray.Add(new Node(child));
        }

    }

    public State GetState() //Complete
    {
        return state;
    }

    public void SetState(State state) //Complete
    {
        this.state = state;
    }

    public Node GetParent() //Complete
    {
        return parent;
    }

    public void SetParent(Node parent) //Complete
    {
        this.parent = parent;
    }

    public List<Node> GetChildArray() //Complete
    {
        return childArray;
    }

    public void SetChildArray(List<Node> childArray) //Complete
    {
        this.childArray = childArray;
    }

    public Node GetRandomChildNode() //Complete
    {
        int possibleMoves = this.childArray.Count;
        int randomNode = (UnityEngine.Random.Range(0, 1) * ((possibleMoves - 1) + 1));
        return this.childArray[randomNode];
    }

    public Node GetChildWithMaxScore() //Possibly Complete
    {   
        int maxVal = this.childArray.Max(child => child.GetState().GetVisitCount());
        Node maxChild = this.childArray.First(child => child.GetState().GetVisitCount() == maxVal);

        return maxChild;
        //https://www.youtube.com/watch?v=-Qxuvv5fXSg
    }
}
