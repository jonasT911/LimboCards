using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bottomEnemy : baseEnemy
{
    private int maxMoney = 7;

    IEnumerator enemyTurn()
    {
        int currentMoney=maxMoney;

       
        
        print("bottom enemy size " + enemyHand.Count);
        print("money held = " + currentMoney);
        int i = 4;
        while (i >= 0 && enemyHand.Count != 0&&currentMoney>=2)
        {
            getBoardState();
            if (enemyCards[i] == null)
            {
                GameObject playedCard = Instantiate(card, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                playedCard.GetComponent<Card>().clickable = false;
                playedCard.GetComponent<Card>().setTired(true);
                enemyHand.Remove(card);
                board.playEnemyCards(playedCard, i);
                currentMoney -= 2;
                yield return new WaitForSeconds(.5f);
                print("place Card on " + i);
            }
            i--;
        }
        takingTurn = false;
        enemyHand.Add(card);
        enemyHand.Add(card);
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
