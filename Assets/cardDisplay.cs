using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardDisplay : MonoBehaviour
{
    public Text attackDisplay;
    public Text healthDisplay;
    public Text costDisplay;
    public Text ability;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void passValues(int attack, int currentHealth, int cost,string cardText)
    {
        attackDisplay.text = "" + attack;
        
            healthDisplay.text = "" + currentHealth;
        
        costDisplay.text = "" + cost;
        ability.text = cardText;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}
