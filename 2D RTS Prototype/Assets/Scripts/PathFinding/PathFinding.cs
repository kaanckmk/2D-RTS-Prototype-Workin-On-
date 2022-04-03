using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10; 
    private const int MOVE_DIAGONAL_COST = 14; // approx.. numbers multiplied by 10

    public static PathFinding instance { get; private set; }
    
    private Grid<PathNode> _grid;

    private List<PathNode> _openList; //Nodes queued up for searching
    private List<PathNode> _closedList; //Nodes that have already been searched
    /* Keep going until Current Node== End Node || Open List is Empty
    */
    
    
    public PathFinding(int width, int height)
    {
        instance = this;
        
       _grid = new Grid<PathNode>(width, height, 1f, 
           new UnityEngine.Vector2(0f,0f),
           (Grid<PathNode> g, int x, int y) => new PathNode(g,x,y));
    }

    // method that return the list of vectors
    public List<UnityEngine.Vector2> FindPath(UnityEngine.Vector2 startWorldPosition, UnityEngine.Vector2 endWorldPosition)
    {
        _grid.GETXY(startWorldPosition, out int startX, out int startY);
        _grid.GETXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        
        if (path==null)
        {
            return null;
        }
        else
        {
            List<UnityEngine.Vector2> vectorPath = new List<UnityEngine.Vector2>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new UnityEngine.Vector2(pathNode.x,pathNode.y) * 
                               _grid.GetCellSize() + 
                               UnityEngine.Vector2.one * _grid.GetCellSize() * .5f);
            }

            return vectorPath;
        }

    }
    //version that return the list of path nodes 
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetGridObject(startX, startY);
        PathNode endNode = _grid.GetGridObject(endX, endY);

        _openList = new List<PathNode> {startNode}; //start with startNode is queued up
        _closedList = new List<PathNode>();

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                PathNode pathNode = _grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == endNode)
            {
                //reached final node
                return CalculatePath(endNode);
            }

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if(_closedList.Contains(neighbourNode)) 
                    continue;
                if (!neighbourNode.isWalkable)
                {
                    _closedList.Add(neighbourNode);
                    continue;
                }
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();
                    if (!_openList.Contains(neighbourNode))
                    {
                        _openList.Add(neighbourNode);
                    }
                }
            }
        }
        //Out of nodes on the openList
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        
        if (currentNode.x -1 >= 0)
        {
            //left
            neighbourList.Add(GetNode(currentNode.x-1, currentNode.y));
            //left down
            if (currentNode.y-1 >= 0) 
                neighbourList.Add(GetNode(currentNode.x-1, currentNode.y-1));
            //left up
            if (currentNode.y+1 < _grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x-1,currentNode.y+1));
        }

        if (currentNode.x+1 < _grid.GetWidth())
        {
            //right
            neighbourList.Add(GetNode(currentNode.x+1,currentNode.y));
            //right down
            if (currentNode.y-1 >= 0)
                neighbourList.Add(GetNode(currentNode.x+1,currentNode.y-1));
            //right up
            if (currentNode.y+1 < _grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x+1, currentNode.y+1));
        }

        //down
        if (currentNode.y-1 >= 0)
            neighbourList.Add(GetNode(currentNode.x,currentNode.y-1));
        //up
        if (currentNode.y+1 < _grid.GetHeight())
            neighbourList.Add(GetNode(currentNode.x,currentNode.y+1));

        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }

    public Grid<PathNode>  GetGrid()
    {
        return _grid;
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
