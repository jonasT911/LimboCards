using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseEnemy : MonoBehaviour
{

    protected private FieldOfPlay board;
    public GameObject[] myCards = new GameObject[5];
    protected ArrayList enemyHand = new ArrayList();
    protected GameObject[] playerCards = new GameObject[5];
    protected GameObject[] enemyCards = new GameObject[5];
    protected bool takingTurn = false;
    protected int currentHealth;
    // Start is called before the first frame update
    protected void Start()
    {

        print("Enemy is here");
        board = FindObjectOfType<FieldOfPlay>();
        enemyHand.Add(getCard());
        enemyHand.Add(getCard());
        enemyHand.Add(getCard());
    }

    protected GameObject getCard()
    {
        int cardInd = Random.Range(0, myCards.Length);
        print(cardInd + " = random");
        GameObject drawnCard = (GameObject)myCards[cardInd];
        return drawnCard;
    }

    protected void getBoardState()
    {
        playerCards = board.getPlayerSide();
        enemyCards = board.getEnemySide();
    }
    IEnumerator enemyTurn()
    {
      
   
        enemyHand.Add(getCard());
        print("enemy size " + enemyHand.Count);
        int i = 0;
        while (i < 5 && enemyHand.Count != 0)
        {
            getBoardState();
            if (enemyCards[i] == null)
            {
                GameObject pickedCard =(GameObject) enemyHand[0];
                GameObject playedCard = Instantiate(pickedCard, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                playedCard.GetComponent<Card>().clickable = false;
                playedCard.GetComponent<Card>().setTired(true);
                enemyHand.Remove(pickedCard);
                board.playEnemyCards(playedCard, i);
                yield return new WaitForSeconds(.5f);
            }
            i++;
        }
        takingTurn = false;
        board.enemyDone();
        print("After play size " + enemyHand.Count);
    }

    private void Update()
    {
        if (currentHealth > board.getEnemyHealth())
        {
            shake();
            Invoke("stopShake", .5f);
        }
        currentHealth = board.getEnemyHealth();
    }

    private void shake()
    {
        Invoke("shake", .05f);
        int rotateAmt = Random.Range(-7, 7);
        transform.Rotate(new Vector3(0, 0, rotateAmt));
    }
    public void stopShake()
    {
        CancelInvoke("shake");
        transform.eulerAngles = new Vector3(0, 0, 0);

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
