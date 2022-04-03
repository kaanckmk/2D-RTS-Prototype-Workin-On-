using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    public int x;
    public int y;
    
    /* fCost = gCost + hCost */
    public int gCost; //Walking cost from the start node
    public int hCost; //Heuristic cost to reach the end node ( just a basic assumption)
    public int fCost;

    public PathNode cameFromNode; //reference that where we came from previously.

    public bool isWalkable;
    
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this._grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }
 
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
}
