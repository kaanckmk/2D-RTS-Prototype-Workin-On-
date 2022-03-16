using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid _grid;
    void Start()
    {
        _grid = new Grid(4,4,1f,new Vector2(-5,-5));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grid.SetValue(InputController.GetMouseWorldPosition(), 45);
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_grid.GetValue(InputController.GetMouseWorldPosition()));
        }
    }
}
