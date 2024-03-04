using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    private Dictionary<string, Equipment> equipment = new Dictionary<string, Equipment>();

    public void Initialize()
    {
        maxHealth = 150;
        health = maxHealth;

        baseDamage = 45;

        potions = 0;

        equipment.Add("Head", null);
        equipment.Add("Torso", null);
        equipment.Add("Hands", null);
        equipment.Add("Legs", null);
    }

    public override void TakeDamage(int amount)
    {
        int blockedDamage = amount * GetGlobalModifier() / 100;
        base.TakeDamage(amount - blockedDamage);
    }

    private int GetGlobalModifier()
    {
        int modifier = 0;

        if (equipment["Head"] != null)
        {
            modifier += equipment["Head"].modifier;
        }
        if (equipment["Torso"] != null)
        {
            modifier += equipment["Torso"].modifier;
        }
        if (equipment["Hands"] != null)
        {
            modifier += equipment["Hands"].modifier;
        }
        if (equipment["Legs"] != null)
        {
            modifier += equipment["Legs"].modifier;
        }

        return modifier;
    }
}