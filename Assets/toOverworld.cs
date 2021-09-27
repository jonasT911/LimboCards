using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toOverworld : MonoBehaviour
{
    private bool readyToBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("r", .3f);
        Deck deck = FindObjectOfType<Deck>();
        if (deck != null)
        {
            Destroy(deck.gameObject);
        }
        animationController bg = FindObjectOfType<animationController>();
        if (bg != null)
        {
            Destroy(bg.gameObject);
        }
    }

    void r()
    {
        readyToBattle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            if (readyToBattle)
            {
                SceneManager.LoadScene("Overworld");
            }

        }
    }
}

