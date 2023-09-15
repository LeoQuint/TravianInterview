//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal	//
//		www.leoquintgames.com		    //
//////////////////////////////////////////
//  Made on:  #CREATIONDATE#        
// Player.cs 
//  Description:
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Constants

    //Public

    //Protected

    //Private
    [SerializeField] private GameObject m_BodyPart = null;

    private List<GameObject> m_BodyParts;
    private List<Vector2Int> m_Positions;
    private Action m_OnLost;
    private Action m_OnAppleCollected;

    private Rigidbody2D m_Rigidbody2D;
    //Properties

    #region Unity API
    #endregion

    #region Public Methods
    public void Init(Action onLost, Action onAppleCollected) 
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_OnLost = onLost;
        m_OnAppleCollected = onAppleCollected;
        m_Positions = new List<Vector2Int>();
        m_BodyParts = new List<GameObject>();
    }

    public void AddSegment(Vector2Int position)
    {
        m_Positions.Add(position);
        GameObject bodyPart = Instantiate(m_BodyPart, m_BodyPart.transform.parent);
        bodyPart.SetActive(true);
        m_BodyParts.Add(Instantiate(bodyPart));
    }

    public void Move(Vector2Int movement) 
    {
        for (int i = m_Positions.Count - 1; i >= 0; --i)
        {
            if (i == 0)
            {
                m_Positions[i] += movement;
            }
            else
            {
                m_Positions[i] = m_Positions[i - 1];
            }
        }

        m_Rigidbody2D.MovePosition(new Vector2(m_Positions[0].x, m_Positions[0].y));

        for (int i = 0; i < m_BodyParts.Count; ++i)
        {
            m_BodyParts[i].transform.position = new Vector3(m_Positions[i].x, m_Positions[i].y, 0f);
        }
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            AddSegment(m_Positions[m_Positions.Count - 1]);
            Destroy(collision.gameObject);
            m_OnAppleCollected();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You lose!");
            m_OnLost();
        }
    }
    #endregion
}