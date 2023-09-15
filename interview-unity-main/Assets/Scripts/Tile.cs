//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal	//
//		www.leoquintgames.com		    //
//////////////////////////////////////////
//  Made on:  #CREATIONDATE#        
// Tile.cs 
//  Description:
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Constants

    //Public
    public Vector2Int Position { get; private set; }
    //Protected

    //Private

    //Properties


    #region Public Methods
    public void Init(int x, int y) 
    {
        Position = new Vector2Int(x, y);
    }
    #endregion
}