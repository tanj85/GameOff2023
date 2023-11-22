using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public TriggerResponse playerTriggerResponse;
    private GameObject player;

    public override void Start()
    {
        base.Start();
        playerTriggerResponse.onTriggerEnter2D = OnPlayerTriggerEnter2D;
        playerTriggerResponse.onTriggerExit2D = OnPlayerTriggerExit2D;
        player = GameObject.FindGameObjectWithTag("Player");

        entityType = EntityType.Enemy;
    }

    public virtual void Update(){
        if (player != null){
            // If enemy is damaged or attacking or dead, pause movement for 0.5f seconds.
            if (state == State.Damaged || state == State.Attacking || state == State.Dead){
                Invoke("FollowPlayer", 0.5f);
            } else {
                FollowPlayer();
            }
        }
    }

    #region Collider methods.
    private void OnPlayerTriggerEnter2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Debug.Log("Player entered");
            Entity target = collider.gameObject.GetComponent<Entity>();
            DealDamage(attackDamage, target);
        }
    }

    private void OnPlayerTriggerExit2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Debug.Log("Player exited");
        }
    }
    #endregion

    public virtual void FollowPlayer(){
        state = State.Moving;
        Vector3 targetPosition = player.transform.position;
        Vector3 currentPosition = transform.position;
        Vector3 direction = targetPosition - currentPosition;
        direction.Normalize();
        rb.MovePosition(currentPosition + direction * speed * Time.fixedDeltaTime);
        // Detect if enemy is moving left or right and flip sprite accordingly.
        if (direction.x < 0){
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }
        anim.Play("Movement");
    }
}
