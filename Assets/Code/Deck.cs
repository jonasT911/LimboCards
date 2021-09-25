using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject card;
    public GameObject card2;
    public GameObject card3;
    ArrayList collectedCards = new ArrayList();
    ArrayList undrawnCards = new ArrayList();
    ArrayList graveyard = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        collectedCards.Add(card);
        collectedCards.Add(card);
        collectedCards.Add(card2);
        collectedCards.Add(card2);
        collectedCards.Add(card3);
        collectedCards.Add(card3);
        battleStart();
    }

    public void drawCard()
    {

        print(undrawnCards.Count);
        int cardInd = Random.Range(0, undrawnCards.Count-1);
        GameObject drawnCardType = (GameObject) undrawnCards[cardInd];

        GameObject newCard = Instantiate(drawnCardType, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
      //  undrawnCards.Remove(card);
        //graveyard.Add(card);
        newCard.GetComponent<Card>().startPlayerCard();
    }

    public void battleStart()
    {
        print("Start the battle");
        undrawnCards = new ArrayList(collectedCards);
    

        for(int i=0; i < undrawnCards.Count; i++)
        {
            print(undrawnCards[i].ToString());
        }
    }

    public void shuffle()
    {
        while (graveyard.Count != 0) {

            undrawnCards.Add(graveyard[0]);
            graveyard.Remove(0);
                }
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
