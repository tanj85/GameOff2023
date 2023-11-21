using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
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
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
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
