using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid _grid;
    private int x;
    private int y;
    
    /* fCost = gCost + hCost */
    public int gCost; //Walking cost from the start node
    public int hCost; //Heuristic cost to reach the end node ( just a basic assumption)
    public int fCost;

    public PathNode cameFromNode; //reference that where we came from previously.
    
    public PathNode(Grid grid, int x, int y)
    {
        this._grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
