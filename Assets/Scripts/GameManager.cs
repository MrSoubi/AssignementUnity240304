using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton here, copied from https://www.gamecodeclub.com/le-singleton-dans-unity-un-objet-pour-les-gouverner-tous/
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Shameless code here
    //       | | |
    //       v v v

    public Fighter player;
    public Fighter enemy;

    bool isPlayerActive = true;

    public void EndTurn()
    {
        // Check if someone died
        if (player.health <= 0) // Player is dead
        {
            GameOver();
        }
        else if (enemy.health <= 0) // Enemy is dead
        {
            Scavenge();
        }
        else // No one is dead : battle continues
        {
            isPlayerActive = !isPlayerActive;
            player.GetComponent<PlayerController>().canTakeAction = isPlayerActive;
            if (!isPlayerActive)
            {
                enemy.GetComponent<EnemyBehaviour>().TakeAction(player);
            }
        }
    }

    public void GameOver()
    {
        // To be implemented
    }

    public void Scavenge()
    {
        StartNewBattle();
    }

    public void StartNewBattle()
    {
        isPlayerActive = true;
        enemy.health = 60;
        enemy.maxHealth = enemy.health;
        enemy.baseDamage = 20;
    }
}
