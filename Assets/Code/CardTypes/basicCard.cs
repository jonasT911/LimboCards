using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class basicCard : Card
{
    int maxHealth = 1;
    int currentHealth = 1;
    int attack = 1;
    int cost = 1;

    public new void attackRoutine()
    {

        print("I attack for " + attack);
    }
}
