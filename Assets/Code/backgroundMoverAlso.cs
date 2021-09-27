using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backgroundMoverAlso : MonoBehaviour
{
    animationController background;

    public float speed = 0;
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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("GameOver")){
            Destroy(gameObject);
        }
        if (transform.position.x < -19.06)
        {
            transform.position = new Vector2(19.06f, transform.position.y);
        }
        else
        {
            if (background.playing)
            {
                transform.Translate(speed, 0, 0);
            }
        }
    }
}
