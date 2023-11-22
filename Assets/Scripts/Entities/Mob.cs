using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy
{
    public TriggerResponse playerTriggerResponse;
    public int numSoulCrystalsToDrop;
    public int numCrystalsPerDrop;
    public GameObject soulCrystalPrefab;

      public override void Start()
    {
        base.Start();
        playerTriggerResponse.onTriggerEnter2D = OnPlayerTriggerEnter2D;
        playerTriggerResponse.onTriggerExit2D = OnPlayerTriggerExit2D;
        playerTriggerResponse.onTriggerStay2D = OnPlayerTriggerStay2D;
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

    public override void Die(){
        base.Die();
        DropSoulCrystals(numSoulCrystalsToDrop, numCrystalsPerDrop);
    }

    public void DropSoulCrystals(int numSoulCrystalsToDrop, int numCrystalsPerDrop){
        float xRange = 0.5f;
        float yRange = 0.5f;
        for (int i = 0; i < numSoulCrystalsToDrop; i++){
            float xOffset = Random.Range(-xRange, xRange);
            float yOffset = Random.Range(-yRange, yRange);
            Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
            GameObject soulCrystal = Instantiate(soulCrystalPrefab, spawnPosition, Quaternion.identity);
            soulCrystal.GetComponent<SoulCrystal>().soulCrystals = numCrystalsPerDrop;
        }
    }
}
