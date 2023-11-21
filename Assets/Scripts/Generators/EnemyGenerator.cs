using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn. Fill this in Inspector.
    public List<float> enemySpawnWeights; // Weights of spawn chances for each enemy prefab. Fill this in Inspector.
    private Transform enemyParent; // Parent object for all enemies.
    public float timeUntilSpawn; // Time until next enemy spawns.
    public float minimumSpawnTime; // Minimum time until next enemy spawns.
    public float maximumSpawnTime; // Maximum time until next enemy spawns.

    // Start is called before the first frame update
    void Start()
    {
        NormalizeSpawnWeights();
        enemyParent = GameObject.FindGameObjectWithTag("EnemyParentTransform").transform; // Find the object with name "Enemies" to serve as parent object for all enemies.
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0){
            SpawnEnemy();
            SetTimeUntilSpawn();
        }
    }

    void NormalizeSpawnWeights(){
        float sum = 0;
        foreach (float weight in enemySpawnWeights){
            sum += weight;
        }
        for (int i = 0; i < enemySpawnWeights.Count; i++){
            enemySpawnWeights[i] = enemySpawnWeights[i] / sum;
        }
    }

    void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    GameObject SpawnEnemy(){
        // Choose enemy index from enemyPrefabs using the weights from enemySpawnWeights
        float random = Random.Range(0f, 1f);
        // Use algorithm as described here to choose random index from enemyPrefabs based off their list of weights
        //   https://forum.unity.com/threads/setting-a-weight-probability-on-a-list-of-objects-ina-datalist-then-choosing-an-object-at-random-b.1463372/
        int i = 0;
        while(true){
            float p = enemySpawnWeights[i];
            if (random < p) break;
            random -= p;
            i++;
        }
        GameObject enemy = Instantiate(enemyPrefabs[i], transform.position, Quaternion.identity, enemyParent);
        return enemy;
    }
}
