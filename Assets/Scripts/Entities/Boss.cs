using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public TriggerResponse playerTriggerResponse;

    public float maxKillTime;
    public float killTimer;

    public delegate void OnBossHealthChange(float _health, float _maxHealth);
    public static event OnBossHealthChange onBossHealthChange;

    public delegate void OnBossTimerChange(float _timeLeft, float _maxTime);
    public static event OnBossTimerChange onBossTimerChange;

    public delegate void OnBossDie();
    public static event OnBossDie onBossDie;

    public delegate void OnBossStop();
    public static event OnBossDie onBossStop;
    public enum Bosses
    {
        TestBoss
    }

    public override void Start()
    {
        base.Start();
        playerTriggerResponse.onTriggerEnter2D = OnPlayerTriggerEnter2D;
        playerTriggerResponse.onTriggerExit2D = OnPlayerTriggerExit2D;
        player = GameObject.FindGameObjectWithTag("Player");

        entityType = EntityType.Enemy;
    }

    public override void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }

        killTimer -= Time.deltaTime;
        onBossTimerChange?.Invoke(killTimer, maxKillTime);

        if (killTimer < 0)
        {
            onBossStop?.Invoke();
            DestroySelf();
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        health -= damage;

        onBossHealthChange?.Invoke(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override void Die()
    {
        onBossDie?.Invoke();
        onBossStop?.Invoke();

        //drop loot
        //spawn portal

        DestroySelf();
    }
}
