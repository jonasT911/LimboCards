using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
   
    public bool playing = true;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void stopMoving()
    {
        playing = false;
    }
    public void startMoving()
    {
        playing = true;
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
            if (playing)
            {
                transform.Translate(-.05f, 0, 0);
            }
        }
    }
}
