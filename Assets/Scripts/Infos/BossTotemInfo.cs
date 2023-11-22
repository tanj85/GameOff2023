using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossTotemInfo : InteractableInfo
{
    public Boss.Bosses heldBoss;
    public int cost;
    public int reward;
}
