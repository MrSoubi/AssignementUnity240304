using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

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

    bool isPlayerActive;

    int score;
    int round;

    public GameObject scavengeScreen;
    public GameObject gameOverScreen;

    private void Start()
    {
        score = 0;
        round = 1;

        gameOverScreen.SetActive(false);
        scavengeScreen.SetActive(false);
        StartNewBattle();
    }

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
        gameOverScreen.SetActive(true);
        gameOverScreen.transform.Find("Panel/Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
        gameOverScreen.transform.Find("Panel/Rounds").GetComponent<TextMeshProUGUI>().text = round.ToString();
        // To be implemented
    }

    int modifier;
    int nameIndex;
    string part;

    public void Scavenge()
    {
        score += 5;

        scavengeScreen.SetActive(true);

        scavengeScreen.transform.Find("Panel/Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
        scavengeScreen.transform.Find("Panel/Rounds").GetComponent<TextMeshProUGUI>().text = round.ToString();

        if (Random.Range(0, 10) >= 7)
        {
            // Potion
            scavengeScreen.transform.Find("Panel/LootPanel").gameObject.SetActive(false);
            scavengeScreen.transform.Find("Panel/PotionPanel").gameObject.SetActive(true);
            player.potions++;

            Debug.Log(player.potions);
            scavengeScreen.transform.Find("Panel/Rounds").GetComponent<TextMeshProUGUI>().text = round.ToString();
            scavengeScreen.transform.Find("Panel/Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
        else
        {
            scavengeScreen.transform.Find("Panel/LootPanel").gameObject.SetActive(true);
            scavengeScreen.transform.Find("Panel/PotionPanel").gameObject.SetActive(false);

            modifier = Random.Range(1, 21);
            nameIndex = Random.Range(1, 5);

            switch (nameIndex)
            {
                case 1:
                    part = "head";
                    break;
                case 2:
                    part = "torso";
                    break;
                case 3:
                    part = "hands";
                    break;
                case 4:
                    part = "legs";
                    break;
                default:
                    Debug.LogError("Loot bug");
                    break;
            }

            scavengeScreen.transform.Find("Panel/LootPanel/ObjectFound").GetComponent<TextMeshProUGUI>().text = "Loot found : " + part;
            scavengeScreen.transform.Find("Panel/LootPanel/LootModifier").GetComponent<TextMeshProUGUI>().text = modifier.ToString() + "%";
            scavengeScreen.transform.Find("Panel/LootPanel/Bonus").GetComponent<TextMeshProUGUI>().text = "+" + modifier.ToString();

            
            int difference = modifier - player.GetModifier(part);
            if (difference < 0)
            {
                scavengeScreen.transform.Find("Panel/LootPanel/Difference").GetComponent<TextMeshProUGUI>().color = Color.red;
                scavengeScreen.transform.Find("Panel/LootPanel/Difference").GetComponent<TextMeshProUGUI>().text = (modifier - player.GetModifier(part)).ToString() + "%";
            }
            else if(difference > 0)
            {
                scavengeScreen.transform.Find("Panel/LootPanel/Difference").GetComponent<TextMeshProUGUI>().color = Color.green;
                scavengeScreen.transform.Find("Panel/LootPanel/Difference").GetComponent<TextMeshProUGUI>().text = "+" + (modifier - player.GetModifier(part)).ToString() + "%";
            }
            else
            {
                scavengeScreen.transform.Find("Panel/LootPanel/Difference").GetComponent<TextMeshProUGUI>().color = Color.blue;
                scavengeScreen.transform.Find("Panel/LootPanel/Difference").GetComponent<TextMeshProUGUI>().text = "0%";
            }
        }
    }

    public void EquipLoot()
    {
        player.Equip(part, "gud loot", modifier);
        scavengeScreen.SetActive(false);
        StartNewBattle();
    }

    public void SellLoot()
    {
        score += modifier;
        scavengeScreen.SetActive(false);
        StartNewBattle();
    }

    public void StartNewBattle()
    {
        scavengeScreen.SetActive(false);

        isPlayerActive = true;
        player.GetComponent<PlayerController>().canTakeAction = true;
        enemy.health = 60;
        enemy.maxHealth = enemy.health;
        enemy.baseDamage = 20;
    }

    public void Quit()
    {
        UnityEngine.Application.Quit();
    }

    public void Retry()
    {
        score = 0;
        round = 1;
        player.health = player.maxHealth;
        player.head = ("Hair", 0);
        player.torso = ("Skin", 0);
        player.hands = ("Fingers", 0);
        player.legs = ("Loincloth", 0);
        gameOverScreen.SetActive(false);
        StartNewBattle();
    }
}
