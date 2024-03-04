using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public void Initialize()
    {
        maxHealth = Random.Range(50, 76);
        health = maxHealth;

        baseDamage = Random.Range(20, 41);

        potions = Random.Range(1, 6) % 5 == 0 ? 1 : 0; // 20% chance to spawn with a potion
    }
}
