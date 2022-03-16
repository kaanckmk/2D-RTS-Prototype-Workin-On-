using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    
    private Vector2 _originPosition; //identify the origin point that grid will be based on
    private int _width; //grid width
    private int _height; //grid height
    private float _cellSize; //size of cells in grid
    private int[,] _gridArray; // grid indexes
    
    public Grid(int width, int height, float cellSize, Vector2 originPosition)
    {
        this._originPosition = originPosition;
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        
        _gridArray = new int[width, height];

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                //draw gizmoz to visualize
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white,100f);  
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white,100f);
            }
        }
        //to complete horizontal line of gizmoz
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white,100f);
        //to complete vertical line of gizmoz
        Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height), Color.white,100f);
        
    }

    //convert grid index to the world position
    private Vector2 GetWorldPosition(int x , int y)
    {
        return new Vector2(x,y) * _cellSize + _originPosition;
    }

    //convert world position to the grid index
    private void GETXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }
    
    //set a value to the grid with grid index
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
            Debug.Log(value + " to  assigned to grid: " + x + "-" +y );
        }
    }
    //set a value to the grid by accessing with world position
    public void SetValue(Vector2 worldPosition, int value)
    {
        int x, y;
        GETXY(worldPosition, out x, out y );
        SetValue(x,y, value);
    }
    //get the value of the grid with grid index
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
    //get the value of the grid by accessing with world position
    public int GetValue(Vector2 worldPosition)
    {
        int x, y;
        GETXY(worldPosition, out x, out y );
        return GetValue(x,y);
    }

}
