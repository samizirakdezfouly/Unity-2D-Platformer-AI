using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UCT  {

    public static double UCTValue (int totalVisit, double nodeWinScore, int nodeVisit)
    {
        if(nodeVisit == 0)
        {
            return int.MaxValue;
        }

        return (nodeWinScore / nodeVisit) + 1.41 * Mathf.Sqrt(Mathf.Log(totalVisit) / nodeVisit);
    }

    static Node FindBestWithUCT(Node node)
    {
        int parentCount = node.GetState().GetVisitCount();

        double bestVal = node.GetChildArray().Max(child => 
        UCTValue(parentCount, child.GetState().GetWinScore(), child.GetState().GetVisitCount()));

        Node bestUCTNode = node.GetChildArray().First(child => 
        UCTValue(parentCount, child.GetState().GetWinScore(), child.GetState().GetVisitCount()) == bestVal);

        return bestUCTNode;
    }

}
