using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy
{
    public TriggerResponse playerTriggerResponse;

      public override void Start()
    {
        base.Start();
        playerTriggerResponse.onTriggerEnter2D = OnPlayerTriggerEnter2D;
        playerTriggerResponse.onTriggerExit2D = OnPlayerTriggerExit2D;
        player = GameObject.FindGameObjectWithTag("Player");

        entityType = EntityType.Enemy;
    }

    public override void Update(){
        if (player != null){
            // If enemy is damaged or attacking or dead, pause movement for 0.5f seconds.
            if (state == State.Damaged || state == State.Attacking || state == State.Dead){
                Invoke("FollowPlayer", 0.5f);
            } else {
                FollowPlayer();
            }
        }
    }
}
