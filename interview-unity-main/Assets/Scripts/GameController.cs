//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal	//
//		www.leoquintgames.com		    //
//////////////////////////////////////////
//  Made on:  #CREATIONDATE#        
// GameController.cs 
//  Description:
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Constants

    //Public

    //Protected

    //Private
    [SerializeField] private TextMeshProUGUI m_AppleDisplay;
    [SerializeField] private int m_InitialSnakeLength = 3;
    [SerializeField] private int m_SnakeLengthPerApple = 1;
    [SerializeField] private float m_SnakeSpeed = 1f;
    [SerializeField] private float m_BulletSpeed = 1f;
    [SerializeField] private float m_ShootingRate = 1f;
    [SerializeField] private float m_EnemySpawnRate = 1f;
    [SerializeField] private int m_AppleTargetPerLevel = 1;
    [SerializeField] private float m_AppleSpawnRate = 1f;

    private GameObject m_ApplePrefab = null;
    private Enemy m_EnemyPrefab = null;
    private Player m_Player = null;
    private Grid m_Grid = null;
    private Camera m_Camera = null;
    private PlayerController m_PlayerController = null;

    private static int m_CurrentLevel = 0;

    private int m_ApplesCollected;

    private bool m_IsPlaying = false;
    private float m_EnemySpawnTimer = 0f;
    private float m_PlayerMovementTimer = 0f;
    private float m_AppleSpawnTimer = 0f;
    private bool m_IsGameOver = false;

    private Vector2Int m_PlayerTargetDestination;
    private Vector2Int m_PlayerCurrentPosition;
    private Vector2Int m_CurrentDirection = new Vector2Int(0, 1);
    //Properties

    #region Unity API
    private void Update()
    {
        if (m_IsPlaying)
        {
            MovePlayer();
            UpdateEnemySpawning();
            UpdateAppleSpawning();
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.P))
            {               

                if (m_IsGameOver)
                {
                    Scene s = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(s.name);
                }
                else 
                {
                    StartGame();
                }
            }            
        }
    }
    #endregion

    #region Public Methods
    public void Init(Enemy enemyPrefab, Grid grid, Player player, PlayerController playerController, GameObject applePrefab) 
    {
        m_Camera = Camera.main;
        m_EnemyPrefab = enemyPrefab;

        m_ApplesCollected = 0;
        m_PlayerController = playerController;

        m_Grid = grid;
        m_ApplePrefab = applePrefab;
        m_AppleTargetPerLevel = m_AppleTargetPerLevel + (m_CurrentLevel * 3);

        m_AppleDisplay.SetText($"Apples to collect: {m_AppleTargetPerLevel.ToString()}" );

        m_Player = player;
        m_Player.Init(GameOver, OnAppleCollected);

        m_Camera.transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y, m_Camera.transform.position.z);
    }

    public void StartGame() 
    {
        m_IsPlaying = true;

        //Spawn the player
        m_PlayerCurrentPosition = new Vector2Int(m_Grid.Width / 2, m_Grid.Height / 2);
        //Add the initial segments
        for (int i = 0; i <= m_InitialSnakeLength; ++i) 
        {
            m_Player.AddSegment(m_PlayerCurrentPosition + new Vector2Int(0, i * -1));
        }


        m_Player.Move(Vector2Int.zero);
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void LevelCompleted() 
    {
        m_CurrentLevel++;
        m_IsPlaying = false;
        m_AppleDisplay.SetText($"Level {m_CurrentLevel} Completed" );
    }

    private void OnAppleCollected() 
    {
        ++m_ApplesCollected;

        m_AppleDisplay.SetText($"Apples to collect: {(m_AppleTargetPerLevel- m_ApplesCollected).ToString()}");
        if (m_ApplesCollected >= m_AppleTargetPerLevel)
        {
            LevelCompleted();
        }
    }

    private void MovePlayer() 
    {
        m_PlayerMovementTimer += Time.deltaTime;
        if (m_PlayerMovementTimer >= m_SnakeSpeed)
        {
            m_PlayerMovementTimer -= m_SnakeSpeed;
            //Move by 1 step
            if (m_PlayerController.MovementDirection.magnitude > 0f)
            {
                m_CurrentDirection = m_PlayerController.MovementDirection;
            }

            m_PlayerTargetDestination = m_PlayerCurrentPosition + m_CurrentDirection;

            Tile tile = m_Grid.GetTile(m_PlayerTargetDestination.x, m_PlayerTargetDestination.y);
            if (tile == null)
            {
                GameOver();
            }

            m_Player.Move(m_CurrentDirection);

            m_PlayerCurrentPosition = m_PlayerTargetDestination;
        }
    }

    private void UpdateEnemySpawning() 
    {
        m_EnemySpawnTimer += Time.deltaTime;
        if (m_EnemySpawnTimer >= m_EnemySpawnRate) 
        {
            m_EnemySpawnTimer -= m_EnemySpawnRate;
            SpawnEnemy();
        }
    }

    //Spawns an enemy at a random location on the grid
    private void SpawnEnemy() 
    {
        int x = Random.Range(0, m_Grid.Width);
        int y = Random.Range(0, m_Grid.Height);
        Tile tile = m_Grid.GetTile(x, y);
        if (tile != null) 
        {
            Enemy enemy = Instantiate(m_EnemyPrefab, tile.transform);
            enemy.Init();
        }
    }

    private void UpdateAppleSpawning() 
    {
        m_AppleSpawnTimer += Time.deltaTime;
        if (m_AppleSpawnTimer >= m_AppleSpawnRate) 
        {
            m_AppleSpawnTimer -= m_AppleSpawnRate;
            SpawnApple();
        }
    }

    private void SpawnApple() 
    {
        //Spawn an apple at a random location on the grid
        int x = Random.Range(0, m_Grid.Width);
        int y = Random.Range(0, m_Grid.Height);
        Tile tile = m_Grid.GetTile(x, y);
        if (tile != null) 
        {
            GameObject apple = Instantiate(m_ApplePrefab, tile.transform);
            apple.SetActive(true);
        }
    }

    private void GameOver() 
    {
        m_IsPlaying = false;
        Debug.Log("GameOver");
        m_IsGameOver = true;
        
        m_AppleDisplay.SetText($"Gameover" );
    }
    #endregion
}