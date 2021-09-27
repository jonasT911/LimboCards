using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldManager : MonoBehaviour
{
    animationController bg;
   public GameObject player;
    Deck deck;
    public GameObject bgMain;
    public Text battlesFought;
    public int totalBattles=7;
    public GameObject bgSecond;
    public GameObject startDeck;
    public GameObject[] enemies=new GameObject[1];
    private bool walking = true;
    // Start is called before the first frame update
    void Start()
    {
        bg = FindObjectOfType<animationController>();
        if (bg == null)
        {
            bg=Instantiate(bgMain, new Vector3(-.14f,0), Quaternion.identity).GetComponent<animationController>();
         
        }
        else
        {
            bg.playing = true;
        }

        deck = FindObjectOfType<Deck>();
        if (deck == null)
        {

            deck=Instantiate(startDeck, new Vector3(-7.4f, -4.2f), Quaternion.identity).GetComponent<Deck>();
           
        }
        deck.showTotalCards();
        int enemyInd = Random.Range(0, enemies.Length);

        int numBattles = deck.getBattles();
        if (numBattles == totalBattles)
        {
        }
        else
        {

        }
        if (numBattles == totalBattles)
        {
            if (deck.badThings > 4)
            {
                SceneManager.LoadScene("Hell");

            }
            else if(deck.badThings > 0)
            {
                SceneManager.LoadScene("Mix");
            }
            else
            {
                SceneManager.LoadScene("Win");
            }
            print("You win");
           
        }
        else{
            Instantiate(enemies[numBattles], new Vector3(20.7f, 0), Quaternion.identity);
        }
        battlesFought.text = numBattles + "/" + totalBattles;
    }

    // Update is called once per frame
    void Update()
    {
        if (bg != null)
        {
            print("Stop");
            if (!bg.playing && walking)
            {
               
                player.GetComponent<Animator>().enabled = false;
                walking = false;
            }
        }
    }
}
