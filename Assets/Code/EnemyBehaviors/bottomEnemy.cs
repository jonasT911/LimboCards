using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomEnemy : baseEnemy
{
    IEnumerator enemyTurn()
    {


        enemyHand.Add(card);
        print("bottom enemy size " + enemyHand.Count);
        int i = 4;
        while (i > 0 && enemyHand.Count != 0)
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
            i--;
        }
        takingTurn = false;
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
