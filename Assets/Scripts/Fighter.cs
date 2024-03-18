using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public int baseDamage;

    public int potions;

    GameObject equipment;

    public (string Name, int Modifier) head;
    public (string Name, int Modifier) torso;
    public (string Name, int Modifier) hands;
    public (string Name, int Modifier) legs;

    public GameObject damageText;
    public GameObject defendText;
    public GameObject focusText;
    public GameObject healthText;

    int defense = 1; // 1 if not active, 2 if active
    int focus = 1; // 1 if not active, 2 if active
    int turnActivationDefense;
    int turnActivationFocus;

    private void Start()
    {
        damageText.SetActive(false);
        defendText.SetActive(false);
        focusText.SetActive(false);
        healthText.GetComponent<TextMeshProUGUI>().text = "HP ";

        head = ("Hair", 0);
        torso = ("Skin", 0);
        hands = ("Fingers", 0);
        legs = ("Loincloth", 0);
    }

    public void TakeDamage(int amount)
    {
        int blockedDamage = amount * (head.Modifier + torso.Modifier + hands.Modifier + legs.Modifier) / 100;
        int totalDamage = (amount - blockedDamage) / defense;

        health -= totalDamage;
        StartCoroutine(DisplayDamage(totalDamage));
        healthText.GetComponent<TextMeshProUGUI>().text = "HP " + health.ToString();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        healthText.GetComponent<TextMeshProUGUI>().text = "HP " + health.ToString();
    }

    public void Attack(Fighter target)
    {
        ResetDisplay();
        target.TakeDamage(baseDamage * focus);

        StartCoroutine(EndTurn());
    }

    public void Defend()
    {
        ResetDisplay();
        defendText.SetActive(true);
        defense = 2;
        turnActivationDefense = 2;
        StartCoroutine(EndTurn());
    }

    public void Focus()
    {
        ResetDisplay();
        focusText.SetActive(true);
        focus = 2;
        turnActivationFocus = 2;

        StartCoroutine(EndTurn());
    }

    public void UsePotion()
    {
        if (potions > 0)
        {
            Heal(50);
            potions--;
        }

        StartCoroutine(EndTurn());
    }

    IEnumerator DisplayDamage(int damage)
    {
        damageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
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
        healthText.SetActive(true);
        healthText.GetComponent<TextMeshProUGUI>().text = "HP " + health.ToString();
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1);
        turnActivationDefense = Mathf.Max(0, turnActivationDefense - 1);
        turnActivationFocus = Mathf.Max(0, turnActivationFocus - 1);

        if (turnActivationFocus == 0)
        {
            focus = 1;
        }
        if (turnActivationDefense == 0)
        {
            defense = 1;
        }

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