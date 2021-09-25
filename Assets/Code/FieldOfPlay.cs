using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfPlay : MonoBehaviour
{
    //Test comment
    private int playerHealth = 10;
    private int enemyHealth = 10;
    private int money = 10;
    private int maxMoney = 10;

    public int xSpaceSize=1;
    public int ySpaceSize=1;
    public GameObject farLeft;
    public GameObject midRight;
    public GameObject middle;
    public GameObject midLeft;
    public GameObject farRight;


    public Text playerDisplay;
    public Text enemyDisplay;
    public Text moneyDisplay;

    GameObject[] playerCards = new GameObject[5];
    GameObject[] enemyCards = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        updateText();

    }

     void updateText()
    {
        playerDisplay.text = "" + playerHealth;
        enemyDisplay.text = "" + enemyHealth;
        moneyDisplay.text = "" + money;

    }

   public void endTurn()
    {
        print("end turn");
        money = maxMoney;
        updateText();
    }

    public bool playedOnBoard(GameObject cardHeld)
    {
 
        
        if(onSpace(cardHeld.transform.position,farLeft.transform.position))
        {
           return setOnPostion(cardHeld, farLeft, 0);
            
        }else
        if (onSpace(cardHeld.transform.position, midLeft.transform.position))
        {
            return setOnPostion(cardHeld, midLeft, 1);
        }
        else
        if (onSpace(cardHeld.transform.position, middle.transform.position))
        {
            return setOnPostion(cardHeld, middle, 2);
        }
        else
        if (onSpace(cardHeld.transform.position, midRight.transform.position))
        {
            return setOnPostion(cardHeld, midRight, 3);

        }
        else
        if (onSpace(cardHeld.transform.position, farRight.transform.position))
        {
            return setOnPostion(cardHeld, farRight, 4);
        }
        else
        {
            return false;
        }
     
    }

    bool onSpace(Vector3 cardPos,Vector3 space)
    {
        if(cardPos.x - xSpaceSize < space.x && cardPos.x + xSpaceSize > space.x)
        {
            if (cardPos.y - ySpaceSize < space.y && cardPos.y + ySpaceSize > space.y)
            {
                return true;
            }
        }
        return false;
    }

    bool setOnPostion(GameObject cardHeld,GameObject space,int location)
    {
        if (playerCards[location] == null&&cardHeld.GetComponent<Card>().cost<=money)
        {
            money -= cardHeld.GetComponent<Card>().cost;
            cardHeld.transform.position = new Vector3(space.transform.position.x - xSpaceSize + 1f, space.transform.position.y, transform.position.z);
            playerCards[location] = cardHeld;
            updateText();
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
