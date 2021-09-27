using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveOrKill : MonoBehaviour
{

    private Deck playerDeck;
    public GameObject spareButton;
    public GameObject killButton;
    public GameObject spareCard;
    public GameObject killCard;
    bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        playerDeck = FindObjectOfType<Deck>();
        GameObject sCard = Instantiate(spareCard, new Vector3(spareButton.transform.position.x, spareButton.transform.position.y+2), Quaternion.identity);
        sCard.GetComponent<Card>().clickable = false;
        sCard.GetComponent<SpriteRenderer>().sortingOrder = 500;
        GameObject kCard = Instantiate(killCard, new Vector3(killButton.transform.position.x, killButton.transform.position.y + 2), Quaternion.identity);
        kCard.GetComponent<Card>().clickable = false;
        kCard.GetComponent<SpriteRenderer>().sortingOrder = 500;
    }
    void returnTo()
    {
        SceneManager.LoadScene("Overworld");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            if (!clicked)
            {
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < spareButton.transform.position.x + 2 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > spareButton.transform.position.x - 2 &&
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y < spareButton.transform.position.y + 1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > spareButton.transform.position.y - 1)
                {
                    clicked = true;
                    print("spare em");
                    if (spareCard != null)
                    {
                        playerDeck.collectCard(spareCard);//spare route
                    }
                    Invoke("returnTo", 1);
                  
                }

                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < killButton.transform.position.x + 2 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > killButton.transform.position.x - 2 &&
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y < killButton.transform.position.y + 1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > killButton.transform.position.y - 1)
                {
                    clicked = true;
                    print("kill em");
                    if (killCard != null)
                    {
                        playerDeck.badThings++;
                        playerDeck.collectCard(killCard);//Kill route
                    }
                    Invoke("returnTo", 1);
                   
                }
            }
        }
    }
}
