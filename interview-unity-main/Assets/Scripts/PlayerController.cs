//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal	//
//		www.leoquintgames.com		    //
//////////////////////////////////////////
//  Made on:  #CREATIONDATE#        
// PlayerController.cs 
//  Description:
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Constants

    //Public

    //Protected

    //Private

    //Properties
    public Vector2Int MovementDirection;
    #region Unity API
    private void Update()
    {
        MovementDirection.y = 0;
        MovementDirection.x = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovementDirection.y += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MovementDirection.y -= 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovementDirection.x -= 1;
        }   

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MovementDirection.x += 1;
        }

        if (!Mathf.Approximately( MovementDirection.y , 0f))
        {
            MovementDirection.x = 0;
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}