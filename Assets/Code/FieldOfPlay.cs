using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FieldOfPlay : MonoBehaviour
{
    //Test comment
    private int playerHealth = 20;
    private int enemyHealth = 20;
    private int money = 8;
    private int maxMoney = 8;

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
    public bool gameEnded = false;
    public GameObject attackSword;
    public GameObject spareDialogue;
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

    public int getEnemyHealth()
    {
        return enemyHealth;
    }

    public int getPlayerHealth()
    {
        return playerHealth;
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
        if (card.GetComponent<Card>().ability != null)
        {
            Instantiate(card.GetComponent<Card>().ability, new Vector3(location, 2, 0), Quaternion.identity);
        }
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
            if (!gameEnded)
            {
                gameEnded = true;
                playerDeck.incrementBattles();
                StartCoroutine(enemyDeath());
            
            }
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
        Instantiate(spareDialogue, new Vector3(0, 0,0), Quaternion.identity);
    }

    public void endTurn()
    {
        if (playerCanAct && !gameEnded)
        {
            print("end turn");
            doCombat(ref playerCards, ref enemyCards, ref enemyHealth, ref playerHealth);
            money = maxMoney;

            updateText();

            playerCanAct = false;
            Invoke("startEnemyTurn", 1.5f);
        }
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
                    float xloc = .6f;
                    GameObject sword =Instantiate(attackSword, new Vector3(attackers[i].transform.position.x + xloc, attackers[i].transform.position.y), Quaternion.identity);
                    if (attackers != playerCards)
                    {
                        sword.transform.Translate(-1.2f,0,0);
                        sword.transform.localScale=new Vector3(-sword.transform.localScale.x, sword.transform.localScale.y, sword.transform.localScale.z);
                    }
                   
                    if (defenders[i] != null)
                    {
                      
                        int healthTmp = defenders[i].GetComponent<Card>().getHealth();
                        healthTmp -= attackers[i].GetComponent<Card>().attack;
                        defenders[i].GetComponent<Card>().setHealth(healthTmp);
                       
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
        checkBoard();
    }

    public void checkBoard()
    {
        Invoke("clearTheDead", 1f);
    }

    private void clearTheDead()
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerCards[i] != null)
            {
                if (playerCards[i].GetComponent<Card>().getHealth() <= 0)
                {

                    playerCards[i].transform.position = new Vector3(100, 100, 0);
                    playerCards[i].GetComponent<Card>().destroyOnPull = true;

                    playerDeck.addToGraveyard(playerCards[i]);

                    playerCards[i] = null;
                }
            }
            if (enemyCards[i] != null)
            {
                if (enemyCards[i].GetComponent<Card>().getHealth() <= 0)
                {
                    Destroy(enemyCards[i]);
                    enemyCards[i] = null;
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
            if (cardHeld.GetComponent<Card>().ability != null)
            {
                Instantiate(cardHeld.GetComponent<Card>().ability, new Vector3(location, 1, 0), Quaternion.identity);
            }
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
