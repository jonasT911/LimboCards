using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    int battlesFought = 0;
    public int badThings = 0;
    private Hand playerHand;
    private FieldOfPlay board;
    public GameObject[] startingCards = new GameObject[6];

    public Text cardsLeft;
    public Text graveYardCount;
    ArrayList collectedCards = new ArrayList();
    ArrayList undrawnCards = new ArrayList();
    ArrayList graveyard = new ArrayList();
    GameObject[] playerCards = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        print("added Cards");
      for(int i = 0; i <startingCards.Length; i++)
        {
            collectedCards.Add(startingCards[i]);
        }
        showTotalCards();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void collectCard(GameObject newcard)
    {
        print("added one card");
        collectedCards.Add(newcard);
    }

    public void drawCard()
    {

        if (!playerHand.full&&!(graveyard.Count==0&&undrawnCards.Count==0)&&!board.gameEnded)
        {
            print(undrawnCards.Count);
            if (undrawnCards.Count == 0)
            {
                shuffle();
            }
            int cardInd = Random.Range(0, undrawnCards.Count);
            print(cardInd+" = random");
            GameObject drawnCardType = (GameObject)undrawnCards[cardInd];

            while (drawnCardType == null&&!(graveyard.Count == 0 && undrawnCards.Count == 0))
            {
                print("ERROR missing card");
                undrawnCards.Remove(drawnCardType);
                if (undrawnCards.Count == 0)
                {
                    shuffle();
                }
                if (!(graveyard.Count == 0 && undrawnCards.Count == 0))
                {
                    
                    cardInd = Random.Range(0, undrawnCards.Count);
                    print(cardInd + " = random");
                    drawnCardType = (GameObject)undrawnCards[cardInd];
                }
            }
            if (drawnCardType != null)
            {
                GameObject newCard = Instantiate(drawnCardType, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                newCard.GetComponent<Card>().destroyOnPull = false;
                undrawnCards.Remove(drawnCardType);
                newCard.GetComponent<Card>().startPlayerCard();

                if (drawnCardType.GetComponent<Card>().destroyOnPull)
                {
                    Destroy(drawnCardType);
                }
            }
            updateCount();
        }
        else
        {
            print("full");
        }

    }
    public void showTotalCards()
    {
        cardsLeft.text = "" + collectedCards.Count;
        graveYardCount.text = "0" ;
    }
    public void addToGraveyard(GameObject card)
    {
     graveyard.Add(card);
    }

    public int getBattles()
    {
        return battlesFought;
    }
    public void incrementBattles()
    {
        battlesFought += 1;
    }

    private void updateCount()
    {
        cardsLeft.text = "" + undrawnCards.Count;
        graveYardCount.text = "" + graveyard.Count;
    }

    public void battleStart()
    {
        
        print("Start the battle");
        undrawnCards = new ArrayList(collectedCards);
        board = FindObjectOfType<FieldOfPlay>();
        playerHand = FindObjectOfType<Hand>();
        for (int i = 0; i < 4; i++)
        {
            drawCard();
        }
    }

    public void shuffle()
    {
        print("shuffle time");
        while (graveyard.Count != 0) {

            undrawnCards.Add(graveyard[0]);
            graveyard.Remove(graveyard[0]);
                }
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
