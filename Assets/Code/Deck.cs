using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{

    private Hand playerHand;
    private FieldOfPlay board;
    public GameObject card;
    public GameObject card2;
    public GameObject card3;
    public Text cardsLeft;
    ArrayList collectedCards = new ArrayList();
    ArrayList undrawnCards = new ArrayList();
    ArrayList graveyard = new ArrayList();
    GameObject[] playerCards = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        collectedCards.Add(card);
      
      
        collectedCards.Add(card2);
        
        collectedCards.Add(card3);
        battleStart();
    }

    public void drawCard()
    {
        if (!playerHand.full&&!(graveyard.Count==0&&undrawnCards.Count==0))
        {
            print(undrawnCards.Count);
            if (undrawnCards.Count == 0)
            {
                shuffle();
            }
            int cardInd = Random.Range(0, undrawnCards.Count);
            print(cardInd+" = random");
            GameObject drawnCardType = (GameObject)undrawnCards[cardInd];
          
            GameObject newCard = Instantiate(drawnCardType, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            undrawnCards.Remove(drawnCardType);
            newCard.GetComponent<Card>().startPlayerCard();

            if (drawnCardType.GetComponent<Card>().destroyOnPull)
            {
                Destroy(drawnCardType);
            }

            updateCount();
        }
        else
        {
            print("full");
        }

    }

    public void addToGraveyard(GameObject card)
    {
     graveyard.Add(card);
    }

    private void updateCount()
    {
        cardsLeft.text = "" + undrawnCards.Count;
    }

    public void battleStart()
    {
        print("Start the battle");
        undrawnCards = new ArrayList(collectedCards);
        board = FindObjectOfType<FieldOfPlay>();
        playerHand = FindObjectOfType<Hand>();
      
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
