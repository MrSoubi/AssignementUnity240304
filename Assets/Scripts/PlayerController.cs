using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Fighter playerScript;
    public bool canTakeAction;

    public void Attack(Fighter target)
    {
        if(canTakeAction)
        {
            playerScript.Attack(target);
            canTakeAction = false;
        }
    }

    public void Defend()
    {
        if(canTakeAction)
        {
            playerScript.Defend();
            canTakeAction = false;
        }
    }

    public void Focus()
    {
        if (canTakeAction)
        {
            playerScript.Focus();
            canTakeAction = false;
        }
    }

    public void UsePotion()
    {
        if (canTakeAction)
        {
            playerScript.UsePotion();
            canTakeAction = false;
        }
    }
}
