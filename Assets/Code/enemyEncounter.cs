using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyEncounter : MonoBehaviour
{
    animationController background;
    public float xfightLocation=9;
    public string battleRoom = "";
    bool readyToBattle = false;
    bool speak = false;
    public GameObject preBattle;
    // Start is called before the first frame update
    void Start()
    {
        
        background = FindObjectOfType<animationController>();
    }

    private void speakLine()
    {
        preBattle.transform.position = new Vector2(0, 0);
        Instantiate(preBattle, new Vector3(1, 2), Quaternion.identity);
        speak = true;

    }
    private void startBattle()
    {
        print("Battle");
        readyToBattle = true;
       
    }

    private void FixedUpdate()
    {
        if (transform.position.x > xfightLocation)
        {
            transform.Translate(-.05f, 0,0);

        }
        else
        {
            if (!speak)
            {
                background.stopMoving();
                speakLine();
                Invoke("startBattle", .3f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            if (readyToBattle)
            {
                SceneManager.LoadScene(battleRoom);
            }

            }
        }
    }

