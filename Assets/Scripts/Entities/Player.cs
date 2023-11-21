using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Sprite spriteFaceRight;
    public Sprite spriteFaceDown;
    //public Animator anim;
    //public Animator 
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
        
        // Use Brackey's top-down movement video to implement animations for movement
        //   https://www.youtube.com/watch?v=whzomFgjT50
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); 
    }
    
    public override void Die()
    {
        base.Die();
    }
}
