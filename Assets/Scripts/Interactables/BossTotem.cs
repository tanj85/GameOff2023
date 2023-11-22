using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTotem : MonoBehaviour, IInteractable
{
    public delegate void OnBossStart();
    public static event OnBossStart onBossStart;

    public string description { get; private set; } = "";

    public bool active { get; set; }

    public GameObject boss;

    void Start()
    {
        active = true;
    }

    void IInteractable.Interact()
    {
        if (active == true)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
            onBossStart.Invoke();
        }
    }

    public void DisableTotemFunction()
    {
        active = false;
    }

    public void EnableTotemFunction()
    {
        active = true;
    }

    void OnEnable()
    {
        onBossStart += DisableTotemFunction;
        Boss.onBossStop += EnableTotemFunction;
    }

    void OnDisable()
    {
        onBossStart -= DisableTotemFunction;
        Boss.onBossStop -= EnableTotemFunction;
    }
}
