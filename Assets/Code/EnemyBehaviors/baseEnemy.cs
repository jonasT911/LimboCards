using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseEnemy : MonoBehaviour
{

    protected private FieldOfPlay board;
    public GameObject card;
    protected ArrayList enemyHand = new ArrayList();
    protected GameObject[] playerCards = new GameObject[5];
    protected GameObject[] enemyCards = new GameObject[5];
    protected bool takingTurn = false;
    // Start is called before the first frame update
    protected void Start()
    {

        print("Enemy is here");
        board = FindObjectOfType<FieldOfPlay>();
        enemyHand.Add(card);
        enemyHand.Add(card);
        enemyHand.Add(card);
    }

    protected void getBoardState()
    {
        playerCards = board.getPlayerSide();
        enemyCards = board.getEnemySide();
    }
    IEnumerator enemyTurn()
    {
      
   
        enemyHand.Add(card);
        print("enemy size " + enemyHand.Count);
        int i = 0;
        while (i < 5 && enemyHand.Count != 0)
        {
            getBoardState();
            if (enemyCards[i] == null)
            {
                GameObject playedCard = Instantiate(card, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                playedCard.GetComponent<Card>().clickable = false;
                playedCard.GetComponent<Card>().setTired(true);
                enemyHand.Remove(card);
                board.playEnemyCards(playedCard, i);
                yield return new WaitForSeconds(.5f);
            }
            i++;
        }
        takingTurn = false;
        board.enemyDone();
        print("After play size " + enemyHand.Count);
    }


    protected void FixedUpdate()
    {
        if (board.isEnemyTurn()&&!takingTurn)
        {
            takingTurn = true;
            StartCoroutine(enemyTurn());

        }
    }
}
