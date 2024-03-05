using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public int baseDamage;

    public int potions;

    (string Name, int Modifier) head;
    (string Name, int Modifier) torso;
    (string Name, int Modifier) hands;
    (string Name, int Modifier) legs;

    public GameObject damageText;
    public GameObject defendText;
    public GameObject focusText;

    private void Start()
    {
        damageText.SetActive(false);
        defendText.SetActive(false);
        focusText.SetActive(false);

        head = ("Hair", 0);
        torso = ("Skin", 0);
        hands = ("Fingers", 0);
        legs = ("Loincloth", 0);
    }

    public void TakeDamage(int amount)
    {
        int blockedDamage = amount * (head.Modifier + torso.Modifier + hands.Modifier + legs.Modifier) / 100;

        health -= amount - blockedDamage;
        StartCoroutine(DisplayDamage(amount));
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    public void Attack(Fighter target)
    {
        ResetDisplay();
        target.TakeDamage(baseDamage);

        StartCoroutine(EndTurn());
    }

    public void Defend()
    {
        ResetDisplay();
        defendText.SetActive(true);
        // To be implemented

        StartCoroutine(EndTurn());
    }

    public void Focus()
    {
        ResetDisplay();
        focusText.SetActive(true);
        // To be implemented

        StartCoroutine(EndTurn());
    }

    public void UsePotion()
    {
        // to be implemented

        StartCoroutine(EndTurn());
    }

    IEnumerator DisplayDamage(int damage)
    {
        damageText.SetActive(true);

        while (damageText.transform.position.y < 1)
        {
            damageText.transform.position += new Vector3(0f, Time.deltaTime, 0f);
            yield return null;
        }

        damageText.SetActive(false);
        damageText.transform.position = transform.position;
    }

    private void ResetDisplay()
    {
        defendText.SetActive(false);
        focusText.SetActive(false);
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.EndTurn();
    }
}