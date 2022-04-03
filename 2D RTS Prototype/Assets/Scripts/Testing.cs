using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour
{
    public PathFinding pathFinding;
    /* GRID TESTING ********************
     
    // Start is called before the first frame update
    private Grid<int> _grid;
    void Start()
    {
        _grid = new Grid<int>(4,4,1f,
            new Vector2(-3,0),  (Grid<int> g, int x, int y) => new int());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grid.SetGridObject(InputController.GetMouseWorldPosition(), 1);
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_grid.GetGridObject(InputController.GetMouseWorldPosition()));
        }
    }
    
    */

    private void Start()
    {
        pathFinding = new PathFinding(4,4);
        pathFinding.GetGrid().GetGridObject(1,0).SetIsWalkable(false);
        pathFinding.GetGrid().GetGridObject(1,1).SetIsWalkable(false);
        pathFinding.GetGrid().GetGridObject(1,2).SetIsWalkable(false);
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            pathFinding.GetGrid().GETXY(InputController.GetMouseWorldPosition(), out int x, out int y);
            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i=0; i<path.Count -1 ; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f + Vector3.one * .4f, 
                        new Vector3(path[i+1].x,path[i+1].y) * 1f + Vector3.one *.4f,
                                Color.green ,2f);
                }
            }
        }
    }
}
