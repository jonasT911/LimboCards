using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bottomEnemy : baseEnemy
{
    private int maxMoney = 8;

    IEnumerator enemyTurn()
    {
        int currentMoney=maxMoney;

       
        
        print("bottom enemy size " + enemyHand.Count);
        print("money held = " + currentMoney);
        int i = 4;
        while (i >= 0 && enemyHand.Count != 0)
        {
            getBoardState();
            if (enemyCards[i] == null)
            {
                GameObject pickedCard = (GameObject)enemyHand[0];
                if (currentMoney >= pickedCard.GetComponent<Card>().cost)
                {
                    GameObject playedCard = Instantiate(pickedCard, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    playedCard.GetComponent<Card>().clickable = false;
                    playedCard.GetComponent<Card>().setTired(true);
                    enemyHand.Remove(pickedCard);
                    board.playEnemyCards(playedCard, i);
                    currentMoney -= playedCard.GetComponent<Card>().cost;
                    yield return new WaitForSeconds(.5f);
                    print("place Card on " + i);
                }
            }
            i--;
        }
        takingTurn = false;
        enemyHand.Add(getCard());
        enemyHand.Add(getCard());
        board.enemyDone();
        print("After play size " + enemyHand.Count);
    }

    new void FixedUpdate()
    {
        if (board.isEnemyTurn() && !takingTurn)
        {
            takingTurn = true;
            StartCoroutine(enemyTurn());

        }
    }
}
