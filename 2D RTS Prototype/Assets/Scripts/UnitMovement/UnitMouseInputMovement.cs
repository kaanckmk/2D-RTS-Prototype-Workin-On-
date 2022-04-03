using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMouseInputMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<UnitMovementPathFinding>().SetMovePosition(InputController.GetMouseWorldPosition());
        }
    }
}
