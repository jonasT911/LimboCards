using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public float speed = -.05f;
    public bool playing = true;
    public GameObject[] backgroundPieces=new GameObject[1];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < backgroundPieces.Length; i++)
        {
            GameObject tmp = Instantiate(backgroundPieces[i], new Vector3(19.06f, 0), Quaternion.identity);
            Instantiate(backgroundPieces[i], new Vector3(0, 0), Quaternion.identity);
            tmp.transform.localScale = new Vector3(-tmp.transform.localScale.x, tmp.transform.localScale.y, tmp.transform.localScale.z) ;
        }
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
                transform.Translate(speed, 0, 0);
            }
        }
    }
}
