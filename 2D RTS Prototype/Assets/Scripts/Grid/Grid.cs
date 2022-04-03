using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Generic grid class since grid indexes can be int, bool or some other types like custom objects */
public class Grid<TGridObject>
{
    
    
    private Vector2 _originPosition; //identify the origin point that grid will be based on
    private int _width; //grid width
    private int _height; //grid height
    private float _cellSize; //size of cells in grid
    private TGridObject[,] _gridArray; // grid indexes as generic
    
    public Grid(int width, int height, float cellSize, Vector2 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this._originPosition = originPosition;
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        
        _gridArray = new TGridObject[width, height];
        
        /* INITIALIZE OBJECT FOR EACH GRID INDEX
         To give custom type for cells we need to receive a function to create object (createGridObject())*/
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _gridArray[x, y] = createGridObject(this, x, y);
            }
        }
        
        //DRAW GIZMOZ TO VISUALIZE
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white,100f);  
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white,100f);
            }
        }
        //to complete horizontal line of gizmoz
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white,100f);
        //to complete vertical line of gizmoz
        Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height), Color.white,100f);
        
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }

    //convert grid index to the world position
    public Vector2 GetWorldPosition(int x , int y)
    {
        return new Vector2(x,y) * _cellSize + _originPosition;
    }

    //convert world position to the grid index
    public void GETXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }
    
    //set a value to the grid with grid index
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
            Debug.Log(value + " to  assigned to grid: " + x + "-" +y );
        }
    }
    //set a value to the grid by accessing with world position
    public void SetGridObject(Vector2 worldPosition, TGridObject value)
    {
        int x, y;
        GETXY(worldPosition, out x, out y );
        SetGridObject(x,y, value);
    }

    
    //get the value of the grid with grid index
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return default(TGridObject); //if grid object is int return 0 - if bool return false
        }
    }
    //get the value of the grid by accessing with world position
    public TGridObject GetGridObject(Vector2 worldPosition)
    {
        int x, y;
        GETXY(worldPosition, out x, out y );
        return GetGridObject(x,y);
    }

}
