using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    FieldOfPlay board;
    bool clickable = true;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<FieldOfPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x + 2 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x - 2 &&
                  Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y + 1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > transform.position.y - 1)
            {
                if (clickable)
                {
                    board.endTurn();
                }
            }
        }
    }
}
