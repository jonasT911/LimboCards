using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{

    private FieldOfPlay board;
    public GameObject card;
    ArrayList hand = new ArrayList();
    GameObject[] playerCards = new GameObject[5];
    GameObject[] enemyCards = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<FieldOfPlay>();
        hand.Add(card);
        hand.Add(card);
        hand.Add(card);
    }

    void getBoardState()
    {
        playerCards = board.getPlayerSide();
        enemyCards = board.getEnemySide();
    }
    public void enemyTurn()
    {
        hand.Add(card);
      
        int i = 0;
        while (i < 5 &&hand.Count!=0)
        {
            getBoardState();
            if (enemyCards[i] == null)
            {
                board.playEnemyCards(Instantiate(card, new Vector3(transform.position.x, transform.position.y), Quaternion.identity), i);
            }

        }
    }


    void FixedUpdate()
    {
        if (board.isEnemyTurn())
        {
            enemyTurn();
            board.enemyDone();

        }
    }
}
