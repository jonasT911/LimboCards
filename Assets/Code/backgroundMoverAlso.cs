using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMoverAlso : MonoBehaviour
{
    animationController background;

    // Start is called before the first frame update
    void Start()
    {
        background = FindObjectOfType<animationController>();

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < -19.06)
        {
            transform.position = new Vector2(19.06f, transform.position.y);
        }
        else
        {
            if (background.playing)
            {
                transform.Translate(-.05f, 0, 0);
            }
        }
    }
}
