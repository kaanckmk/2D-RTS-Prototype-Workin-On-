using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private const float _speed = 40f;
    private int _currentPathIndex;
    
    
    private List<Vector2> pathVectorList;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SetTargetPosition(InputController.GetMouseWorldPosition());
        }
        
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _currentPathIndex = 0;

        pathVectorList = PathFinding.instance.FindPath(GetPosition(), targetPosition);
    }
}
