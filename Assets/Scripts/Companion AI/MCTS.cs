using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MCTS : MonoBehaviour {

    private int level;

    public MCTS()
    {
        this.level = 1;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    private int GetMillisecondsForCurrentLevel()
    {
        return 2 * (this.level - 1) + 1;
    }

    //public GameWorld FindNextMove(GameWorld game)
    //{
    //    DateTime time = new DateTime();

    //    long start = time.Millisecond;
    //    long end = start + 60 * GetMillisecondsForCurrentLevel();

    //    Tree tree = new Tree();
    //    Node rootNode = tree.GetRoot();

    //    rootNode.GetState(); //More needed
    //    rootNode.GetState(); //More needed

    //    while (time.Millisecond < end)
    //    {
    //        //1- Selection
    //        Node promisingNode = SelectPromisingNode(rootNode);
    //        //2 - Expansion
    //        ExpandNode(promisingNode);
    //        //3 - Simulation
    //        Node nodeToExplore = promisingNode;

    //        if (promisingNode.GetChildArray().Count > 0)
    //        {
    //            nodeToExplore = promisingNode.GetRandomChildNode();
    //        }

    //        int playoutResult; 

    //    }
    //}

    private Node SelectPromisingNode(Node rootNode)
    {
        Node node = rootNode;

        while(node.GetChildArray().Count != 0)
        {
            node = UCT.FindBestWithUCT(node);
        }

        return node;
    }

    private void ExpandNode(Node node)
    {
        List<State> possibleGameStates = node.GetState().GetAvailableStates();
        
        foreach(State state in possibleGameStates)
        {
            Node newNode = new Node(state);
            newNode.SetParent(node);
            node.GetChildArray().Add(newNode);
        }
    }

    private void BackPropogation(Node nodeToExplore)
    {
        Node tempNode = nodeToExplore;

        while (tempNode != null)
        {
            tempNode.GetState().IncreaseVisit();

        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
