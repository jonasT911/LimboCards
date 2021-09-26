using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FieldOfPlay : MonoBehaviour
{
    //Test comment
    private int playerHealth = 10;
    private int enemyHealth = 10;
    private int money = 7;
    private int maxMoney = 7;

    public int xSpaceSize = 1;
    public int ySpaceSize = 1;
    public GameObject[] spaces = new GameObject[5];

    private Deck playerDeck;

    public Deck errorDeck;

    public Text playerDisplay;
    public Text enemyDisplay;
    public Text moneyDisplay;

    GameObject[] playerCards = new GameObject[5];
    GameObject[] enemyCards = new GameObject[5];

    bool enemyTurn = false;
    bool playerCanAct = true;
    // Start is called before the first frame update
    void Start()
    {

        playerDeck = FindObjectOfType<Deck>();
        if (playerDeck == null)
        {
            print("error deck");
           playerDeck=Instantiate(errorDeck, new Vector3(-7.4f, -4.2f,0), Quaternion.identity);
           
        }
        Invoke("beginGame", .2f);

    }

    private void beginGame()
    {
        updateText();
        playerDeck.battleStart();

    }

    public GameObject[] getPlayerSide()
    {
        return playerCards;
    }
    public GameObject[] getEnemySide()
    {
        return enemyCards;
    }

    public void playEnemyCards(GameObject card, int location)
    {
        card.transform.position = new Vector3(spaces[location].transform.position.x + xSpaceSize - 1f, spaces[location].transform.position.y, transform.position.z);
        enemyCards[location] = card;
        updateText();
        
    }

    void updateText()
    {
        playerDisplay.text = "" + playerHealth;
        enemyDisplay.text = "" + enemyHealth;
        moneyDisplay.text = "" + money;
        if (playerHealth <= 0)
        {
            StartCoroutine(playerDeath());
        }
        if (enemyHealth <= 0)
        {
            StartCoroutine(enemyDeath());
        }
      

    }

    IEnumerator playerDeath()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("GameOver");
    }
    IEnumerator enemyDeath()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Overworld");
    }

    public void endTurn()
    {
        print("end turn");
        doCombat(ref playerCards, ref enemyCards, ref enemyHealth, ref playerHealth);
        money = maxMoney;

        updateText();

        playerCanAct = false;
        Invoke("startEnemyTurn", 1f);
    }

    private void startEnemyTurn()
    {
        enemyTurn = true;
    }

    public bool isEnemyTurn()
    {
        return enemyTurn;
    }
    public void enemyDone(){
        enemyTurn = false;
        Invoke("enemyCombat", .5f);
   
    }

    void enemyCombat()
    {
        doCombat(ref enemyCards, ref playerCards, ref playerHealth, ref enemyHealth);
        playerDeck.drawCard();
        playerCanAct = true;
        updateText();
    }

    public void doCombat(ref GameObject[] attackers, ref GameObject[] defenders, ref int defendingHero, ref int attackingHero)
    {
        for(int i = 0; i < 5; i++)
        {
            if (attackers[i] != null)
            {
                if (!attackers[i].GetComponent<Card>().getTired())
                {
                    if (defenders[i] != null)
                    {
                        print("Enemy in the way");
                        int healthTmp = defenders[i].GetComponent<Card>().getHealth();
                        healthTmp -= attackers[i].GetComponent<Card>().attack;
                        defenders[i].GetComponent<Card>().setHealth(healthTmp);
                        if (healthTmp <= 0)
                        {
                            if (defenders == playerCards)
                            {
                                defenders[i].transform.position = new Vector3(100, 100, 0);
                                defenders[i].GetComponent<Card>().destroyOnPull = true;

                                playerDeck.addToGraveyard(defenders[i]);
                            }
                            else
                            {
                                Destroy(defenders[i]);
                            }
                            defenders[i] = null;
                        }
                    }
                    else
                    {
                        defendingHero -= attackers[i].GetComponent<Card>().attack;
                    }
                }
                else
                {
                    attackers[i].GetComponent<Card>().setTired(false);
                }
            }
        }
    }

    public bool playedOnBoard(GameObject cardHeld)
    {
 
        if(!playerCanAct)
        {
            return false;
        }
        if(onSpace(cardHeld.transform.position,spaces[0].transform.position))
        {
           return setOnPostion(cardHeld, 0);
            
        }else
        if (onSpace(cardHeld.transform.position, spaces[1].transform.position))
        {
            return setOnPostion(cardHeld,  1);
        }
        else
        if (onSpace(cardHeld.transform.position, spaces[2].transform.position))
        {
            return setOnPostion(cardHeld,  2);
        }
        else
        if (onSpace(cardHeld.transform.position, spaces[3].transform.position))
        {
            return setOnPostion(cardHeld,  3);

        }
        else
        if (onSpace(cardHeld.transform.position, spaces[4].transform.position))
        {
            return setOnPostion(cardHeld,  4);
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

    bool setOnPostion(GameObject cardHeld,int location)
    {
        if (playerCards[location] == null&&cardHeld.GetComponent<Card>().cost<=money)
        {
            money -= cardHeld.GetComponent<Card>().cost;
            cardHeld.transform.position = new Vector3(spaces[location].transform.position.x - xSpaceSize + 1f, spaces[location].transform.position.y, transform.position.z);
            playerCards[location] = cardHeld;
            updateText();
            cardHeld.GetComponent<Card>().setTired(true);
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
