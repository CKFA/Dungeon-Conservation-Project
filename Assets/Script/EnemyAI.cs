using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float hp;
    public float startHp;

    public int moneyDrop = 50;

    public GameObject deathEffect;

    [Header("Unit Stuff")]
    public Image healthBar;
    public GameObject moneyGainTextObject;

    private void Start()
    {
        speed = startSpeed;
        startHp = 100;
        hp = startHp;
    }
    public void TakeDamage(float amount)
    {
        hp -= amount;
        healthBar.fillAmount = hp / startHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    void Die()
    {
        int moneyActualDrop = 0;
        moneyActualDrop = Mathf.RoundToInt(moneyDrop * PlayerStats.buildingMoneyBuff);
        PlayerStats.money += moneyActualDrop;

        GameObject moneyDropObject = (GameObject)Instantiate(moneyGainTextObject, transform.position, Quaternion.Euler(90f,0f,0f));
        moneyDropObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        moneyDropObject.GetComponentInChildren<Text>().text = $"${moneyActualDrop}";
        Destroy(moneyDropObject,5f);

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
