using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    private FieldOfPlay board;
    private Hand hand;

    public int maxHealth = 1;
    private int currentHealth = 1;
    public int attack = 1;
    public int cost = 1;

    public Text attackDisplay;
    public Text healthDisplay;
    public Text costDisplay;

    public bool clickable = true;
    bool followMouse = false;
    bool tired = false;

    public bool debugPlayer = false;

    public GameObject sleepingEffect;

    // Start is called before the first frame update
    void Start()
    {
        print("card working");
        board = FindObjectOfType<FieldOfPlay>();
      
        currentHealth = maxHealth;
        updateValues();

        if (debugPlayer)
        {
            startPlayerCard();
        }
    }

    public void startPlayerCard()
    {
        hand = FindObjectOfType<Hand>();
        hand.addCard(gameObject);
    }

    public void setHealth(int newHealth)
    {

        currentHealth = newHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateValues();
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public void setTired(bool state)
    {
        tired = state;
        if (tired)
        {
            Invoke("makeZ", .6f);
        }
    }
    
    private void makeZ()
    {
       
        if (tired)
        {
            Instantiate(sleepingEffect, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            Invoke("makeZ", 1.3f);
        }
    }
     public bool getTired()
    {
        return tired;
    }
    public void attackRoutine()
    {
        print("Parent Card");
     
    }

    private void updateValues()
    {

        attackDisplay.text = ""+attack;
        if (clickable)
        {
            healthDisplay.text = "" + maxHealth;
        }
        else
        {
            healthDisplay.text = "" + currentHealth;
        }
        costDisplay.text = ""+ cost;
    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          

            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x + .5 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x - .5 &&
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y + 1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > transform.position.y - 1)
            {
                if (clickable)
                {
                    followMouse = !followMouse;
                }
                if (!followMouse && clickable)
                {
                    print("Click Detected");

                    if (board.playedOnBoard(gameObject))
                    {
                        print("It worked");
                        clickable = false;
                        hand.removeCard(gameObject);
                        //attackRoutine();
                       
                    }

                    hand.organize();
                }
            }

     
        }
        if (followMouse)
        {
            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        }
    }
}
