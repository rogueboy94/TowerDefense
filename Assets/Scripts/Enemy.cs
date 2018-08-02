using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    private float changedHealth;

    public float health = 100;

    public int moneyGain = 10;

    public GameObject deathEffect;

    [HideInInspector]
    public bool slowed = false;

    [Header("Unity Stuff")]
    public Image healthBar;
    
    void Start()
    {
        speed = startSpeed;
        changedHealth = health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / changedHealth;

        if (health <= 0)
            Die();
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);

        slowed = true;
        GetComponentInChildren<Animator>().speed = speed;
    }

    void Die()
    {
        PlayerStats.Money += moneyGain;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}