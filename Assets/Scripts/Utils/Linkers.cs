using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossLinker
{
    public Boss.Bosses boss;
    public GameObject bossPrefab;
}


[System.Serializable]
public class InteractableLinker
{
    public InteractableInfo.InteractableType interactableType;
    public GameObject interactablePrefab;
}