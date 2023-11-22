using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Player,
        Enemy
    }

    public enum State
    {
        Moving,
        Attacking,
        Damaged,
        Idle,
        Dead,
    }

    public EntityType entityType;
    public State state;

    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public float health;
    public float maxHealth;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float attackCooldown;
    public Animator anim;
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
        state = State.Damaged;
        anim.Play("Damaged");
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void DealDamage(float damage, Entity target)
    {
        Debug.Log("state is " + state);
        target.TakeDamage(damage);
        state = State.Attacking;
        Debug.Log("state is " + state);
        anim.Play("Attack");
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        state = State.Dead;
        anim.Play("Death");
        // TODO: Drop loot
    }

    public virtual void Delete(){
        Destroy(gameObject);
    }
}
