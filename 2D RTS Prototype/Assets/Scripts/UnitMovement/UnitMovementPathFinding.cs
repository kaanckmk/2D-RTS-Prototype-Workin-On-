using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementPathFinding : MonoBehaviour
{
    private int _currentPathIndex=-1;
    
    
    private List<Vector2> pathVectorList;


    public void SetMovePosition(Vector2 movePosition)
    {
        pathVectorList = PathFinding.instance.FindPath(transform.position, movePosition);
        if (pathVectorList.Count > 0 )
        {
            _currentPathIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPathIndex != -1)
        {
            Vector2 nextPathPosition = pathVectorList[_currentPathIndex];
            Vector2 currenctPosition = new Vector2(transform.position.x,transform.position.y);
            Vector2 moveVelocity = (nextPathPosition - currenctPosition).normalized;
            
            GetComponent<UnitMoveVelocity>().SetVelocity(moveVelocity);

            float reachedPathPositionDistance = 0.1f;
            if (Vector2.Distance(currenctPosition, nextPathPosition) < reachedPathPositionDistance)
            {
                _currentPathIndex++;
                if (_currentPathIndex >= pathVectorList.Count) 
                {
                    //End of path
                    _currentPathIndex = -1;
                }
            }
        }
        else
        {
            //idle
            GetComponent<UnitMoveVelocity>().SetVelocity(Vector2.zero);
        }
    /*    if (Input.GetMouseButtonDown(0)) {
            SetTargetPosition(InputController.GetMouseWorldPosition());
        }
        */
        
    }
/*
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _currentPathIndex = 0;

        pathVectorList = PathFinding.instance.FindPath(GetPosition(), targetPosition);
    }
    */
}
