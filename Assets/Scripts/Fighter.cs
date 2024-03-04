using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    protected int maxHealth;
    protected int health;

    protected int baseDamage;

    protected int potions;

    public GameObject damageText;
    public GameObject defendText;
    public GameObject focusText;

    private void Start()
    {
        damageText.SetActive(false);
        defendText.SetActive(false);
        focusText.SetActive(false);
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
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
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetBaseDamage()
    {
        return baseDamage;
    }

    public void Defend()
    {
        ResetDisplay();
        defendText.SetActive(true);
        // To be implemented
    }

    public void Focus()
    {
        ResetDisplay();
        focusText.SetActive(true);
        // To be implemented
    }

    public void UsePotion()
    {
        // to be implemented
    }

    IEnumerator DisplayDamage(int damage)
    {
        damageText.SetActive(true);

        while (damageText.transform.position.y < 1)
        {
            damageText.transform.position += new Vector3(0f, 0.5f * Time.deltaTime, 0f);
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
}