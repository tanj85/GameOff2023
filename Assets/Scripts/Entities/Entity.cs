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
    public float attackCooldown;
    public Animator anim;
    public Slider healthBar;

    public virtual void Start()
    {
        health = maxHealth;
        healthBar.value = 1;
    }

    public virtual void Update()
    {
        ;
    }

    protected virtual void OnEnable()
    {
        GameManager.onCleanWorld += Delete;
    }

    protected virtual void OnDisable()
    {
        GameManager.onCleanWorld -= Delete;
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
        if (anim != null) anim.Play("Damaged");
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void DealDamage(float damage, Entity target)
    {
        target.TakeDamage(damage);
        state = State.Attacking;
        if (anim != null) anim.Play("Attack");
    }

    public virtual void Die()
    {
        state = State.Dead;
        if (anim != null) anim.Play("Death");
        var deathAnimTime = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Invoke("Delete", deathAnimTime);
    }

    public virtual void Delete(){
        Destroy(gameObject);
    }

    public static Entity GrabTargetEntityOrParentReferenceEntity(GameObject target)
    {
        Entity hitEntity = target.GetComponent<Entity>();
        if (hitEntity)
        {
            return hitEntity;
        }
        else
        {
            ParentReference parentReference = target.GetComponent<ParentReference>();
            if (parentReference && parentReference.parent.GetComponent<Entity>())
                 return parentReference.parent.GetComponent<Entity>();
        }

        return null;
    }
}
