using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float damage;
    public List<Entity.EntityType> affectedTypes = new List<Entity.EntityType>();
    public float range;
    private Vector2 startPos;

    public int pierce;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        startPos = transform.position;
    }

    void Update()
    {
        float distance_travelled = ((Vector2)transform.position - startPos).magnitude;
        if (distance_travelled > range)
        {
            destroyProjectile();
        }
    }

    public void SetDirection(Vector2 _direction)
    {
        rb = GetComponent<Rigidbody2D>();
        direction = _direction.normalized;
        rb.velocity = direction * speed;
    }

    protected virtual void OnHit(Entity entity)
    {
        if (affectedTypes.Contains(entity.entityType))
        {
            entity.TakeDamage(damage);
            pierce -= 1;
            if (pierce <= 0)
            {
                destroyProjectile();
            }
        }
    }

    protected virtual void destroyProjectile()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        Entity hitEntity = collision.GetComponent<Entity>();
        if (hitEntity)
        {
            OnHit(hitEntity);
        }
        else 
        {
            ParentReference parentReference = collision.GetComponent<ParentReference>();
            if (parentReference && parentReference.parent.GetComponent<Entity>())
            OnHit(parentReference.parent.GetComponent<Entity>());
        }
    }
}
