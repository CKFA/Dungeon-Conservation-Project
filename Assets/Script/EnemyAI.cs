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
        PlayerStats.money += moneyDrop;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
