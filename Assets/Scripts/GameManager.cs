using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public GameObject eventSystem;

    bool isPlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        player.Initialize();
        enemy.Initialize();

        isPlayerTurn = true;
    }

    private void Update()
    {
        if (!isPlayerTurn)
        {
            enemy.Attack(player);
            NextTurn();
        }
    }

    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        eventSystem.SetActive(isPlayerTurn);
    }
}
