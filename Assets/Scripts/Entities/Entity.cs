using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Player,
        Enemy
    }

    public EntityType entityType;

    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public float health;
    public float maxHealth;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float attackCooldown;
    public Slider healthBar;

    public virtual void Start()
    {
        health = maxHealth;
        healthBar.value = 1;
    }

    [ContextMenu("Take Damage")]
    public void TestTakeDamage()
    {
        TakeDamage(10);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health/maxHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void DealDamage(float damage, Entity target)
    {
        target.TakeDamage(damage);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        // TODO: Drop loot
    }

    public virtual void Delete(){
        Destroy(gameObject);
    }
}
