using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnCommand : MonoBehaviour
{
    public float lifetime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (lifetime > 0)
        {
            Invoke("destroyNow", lifetime);
        }
        
    }

    void destroyNow()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
