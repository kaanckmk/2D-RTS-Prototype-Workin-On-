using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    public static  Vector2 GetMouseWorldPosition()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(worldPosition.x + "   " +  worldPosition.y);
        return worldPosition;
    }
}
