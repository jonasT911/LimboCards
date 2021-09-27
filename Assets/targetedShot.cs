using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetedShot : MonoBehaviour
{
    FieldOfPlay board;
    Hand hand;

    public int damage = 1;

    GameObject[] playerCards = new GameObject[5];
    GameObject[] enemyCards = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<FieldOfPlay>();
        hand = FindObjectOfType<Hand>();
        if (transform.position.y == 1)
        {
            enemyCards = board.getEnemySide();
        }
        else
        {
            enemyCards = board.getPlayerSide();
        }
      
        int i = (int)transform.position.x;
        if (enemyCards[i] != null)
            {
                int tempHealth = enemyCards[i].GetComponent<Card>().getHealth() - damage;
                enemyCards[i].GetComponent<Card>().setHealth(tempHealth);
            }
        
        board.checkBoard();
        Invoke("destroyMe", .2f);
    }


    private void destroyMe()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
