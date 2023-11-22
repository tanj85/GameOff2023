using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Sprite spriteFaceRight;
    public Sprite spriteFaceDown;
    //public Animator anim;
    Vector2 movement;

    public override void Start()
    {
        base.Start();

        entityType = EntityType.Player;
    }

    void Update()
    {
        // Handle movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        //if (movement.x == 0 && movement.y == 0) anim.Play("Idle");
        if (movement.x != 0){
            //anim.Play("MoveHorizontal");
        } else if (movement.y != 0){
            //anim.Play("MoveVertical");
        }
        if (movement.x < 0) {sprite.flipX = true;} else {sprite.flipX = false;}
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); 
    }

    public override void Die()
    {
        base.Die();
    }
}
