//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal	//
//		www.leoquintgames.com		    //
//////////////////////////////////////////
//  Made on:  #CREATIONDATE#        
// Grid.cs 
//  Description:
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Constants

    //Public

    //Protected

    //Private
    
    private Tile[,] m_Grid;
    private Tile m_TilePrefab;
    //Properties

    //Width property
    public int Width 
    {
        get 
        {
            return m_Grid.GetLength(0);
        }
    }
    //Height property
    public int Height 
    {
        get 
        {
            return m_Grid.GetLength(1);
        }
    }

    #region Public Methods
    public void Init(int width, int height, Tile tilePrefab)
    {
        m_TilePrefab = tilePrefab;
        CreateGrid(width, height);
    }

    //Get tile at position
    public Tile GetTile(int x, int y) 
    {
        //Check if the position is within the grid
        if (x < 0 || x >= Width || y < 0 || y >= Height) 
        {
            return null;
        }

        return m_Grid[x, y];
    }
    #endregion

    #region Private Methods
    private void CreateGrid(int width, int height) 
    {
        //create the grid
        m_Grid = new Tile[width, height];

        //populate the grid
        for (int x = 0; x < width; ++x) 
        {
            for (int y = 0; y < height; ++y) 
            {
                Tile tile = Instantiate(m_TilePrefab, transform);
                tile.gameObject.name = string.Format("Tile ({0}, {1})", x, y);
                tile.transform.position = new Vector3(x, y, 0f);
                tile.Init(x, y);
                m_Grid[x, y] = tile;
            }
        }
    }
    #endregion
}