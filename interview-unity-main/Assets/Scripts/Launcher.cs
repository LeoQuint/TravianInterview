//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal	//
//		www.leoquintgames.com		    //
//////////////////////////////////////////
//  Made on:  #CREATIONDATE#        
// Launcher.cs 
//  Description:
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    //Constants

    //Public

    //Protected

    //Private
    [SerializeField] private GameController m_GameController = null;
    [SerializeField] private Grid m_Grid = null;
    [SerializeField] private Enemy m_EnemyPrefab = null;
    [SerializeField] private Tile m_TilePrefab = null;
    [SerializeField] private Player m_Player = null;
    [SerializeField] private GameObject m_ApplePrefab = null;
    [SerializeField] private PlayerController m_PlayerController = null;
    [SerializeField] private int m_GridWidth = 50;
    [SerializeField] private int m_GridHeight = 50;
    //Properties

    #region Unity API
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void Init() 
    {
        m_GameController.Init(m_EnemyPrefab, m_Grid, m_Player, m_PlayerController, m_ApplePrefab);
        m_Grid.Init(m_GridWidth, m_GridHeight, m_TilePrefab);
    }
    #endregion
}