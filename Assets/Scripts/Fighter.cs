using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public int baseDamage;

    public int potions;

    public (string Name, int Modifier) head;
    public (string Name, int Modifier) torso;
    public (string Name, int Modifier) hands;
    public (string Name, int Modifier) legs;

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
    public int GetModifier(string part)
    {
        int value = 0;

        switch (part)
        {
            case "head":
                value = head.Modifier;
                break;
            case "hands":
                value = hands.Modifier;
                break;
            case "torso":
                value = torso.Modifier;
                break;
            case "legs":
                value = legs.Modifier;
                break;
        }

        return value;
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

    public void Equip(string part, string name, int modifier)
    {
        switch (part)
        {
            case "head":
                head = (name, modifier);
                break;
            case "hands":
                hands = (name, modifier);
                break;
            case "torso":
                torso = (name, modifier);
                break;
            case "legs":
                legs = (name, modifier);
                break;
        }
    }


}