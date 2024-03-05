using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Fighter enemyScript;

    public void TakeAction(Fighter target)
    {
        enemyScript.Attack(target);
    }
}
