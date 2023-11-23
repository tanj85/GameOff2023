using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossTotemInfo : InteractableInfo
{
    public Boss.Bosses heldBoss;
    public int cost;
    public int reward;

    public BossTotemInfo(Boss.Bosses _heldBoss, int _cost, int _reward, Vector2 _location)
    {
        interactableType = InteractableType.BossTotem;
        heldBoss = _heldBoss;
        cost = _cost;
        reward = _reward;
        location = _location;
    }

    public BossTotemInfo(Boss.Bosses _heldBoss)
    {
        interactableType = InteractableType.BossTotem;
        heldBoss = _heldBoss;
        cost = 22; // figure out calcs later
        reward = GameManager.Instance.bossPrefabDictionary[heldBoss].GetComponent<Boss>().baseRewards; //figure out calcs later
        location = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)); // figure out calcs later
    }
}
