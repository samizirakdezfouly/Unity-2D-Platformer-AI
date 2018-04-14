using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree  {
    //Complete
    Node root;

    public Tree() //Complete
    {
        root = new Node();
    }

    public Tree(Node root) //Complete
    {
        this.root = root;
    }

    public Node GetRoot() //Complete
    {
        return root;
    }

    public void SetRoot(Node root) //Complete
    {
        this.root = root;
    }

    public void AddChild(Node parent, Node child) //Complete
    {
        parent.GetChildArray().Add(child);
    }
}
