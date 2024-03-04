using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI m_DamageText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(TakeDamage(5));
        }
    }

    IEnumerator TakeDamage(int amount)
    {
        float startTime = Time.time;

        m_DamageText.text = amount.ToString();
        m_DamageText.gameObject.SetActive(true);

        while(Time.time < startTime + 1f)
        {
            m_DamageText.transform.position += Vector3.up * Time.deltaTime;
            yield return null;
        }

        m_DamageText.gameObject.SetActive(false);
        m_DamageText.transform.position = transform.position;
    }
}
