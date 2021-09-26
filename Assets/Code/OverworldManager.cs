using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    animationController bg;
    Deck deck;
    public GameObject bgMain;
    public GameObject bgSecond;
    public GameObject startDeck;
    public GameObject[] enemies=new GameObject[1];
    // Start is called before the first frame update
    void Start()
    {
        bg = FindObjectOfType<animationController>();
        if (bg == null)
        {
            Instantiate(bgMain, new Vector3(-.14f,0), Quaternion.identity);
            Instantiate(bgSecond, new Vector3(19.02f, 0), Quaternion.identity);
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

        Instantiate(enemies[enemyInd], new Vector3(20.7f, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
