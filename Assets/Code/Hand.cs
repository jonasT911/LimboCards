using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour
{
    public float scale = 1;

    ArrayList hand = new ArrayList();

    private int maxHand=8;

    public bool full = false;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void addCard(GameObject newCard)
    {
        if (!full)
        {
            hand.Add(newCard);
        }

        if (hand.Count > 7)
        {
            full = true;
        }
        organize();
        
    }
    public void removeCard(GameObject newCard)
    {
        hand.Remove(newCard);
        if (hand.Count <8)
        {
            full = false;
        }
        

    }


    public void organize()
    {
        for(int i = 0; i < hand.Count; i++) {
            
             
                ((GameObject)hand[i]).transform.position = new Vector3(transform.position.x + i * scale - 4, transform.position.y, ((GameObject)hand[i]).transform.position.z);
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
