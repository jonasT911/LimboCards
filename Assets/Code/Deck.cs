using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {


            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x + .5 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x - .5 &&
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y + 1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > transform.position.y - 1)
            {
                GameObject newCard = Instantiate(card, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                newCard.GetComponent<Card>().startPlayerCard();
            }
        }
    }
}
