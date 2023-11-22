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
    public Boss.Bosses bossEnum;

    public int cost;
    public int reward;

    public void Initialize(Boss.Bosses _boss, int _cost, int _reward)
    {
        bossEnum = _boss;
        boss = GameManager.Instance.bossPrefabDictionary[_boss];
        cost = _cost;
        reward = _reward;
    }

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
        GameManager.onCleanWorld += CleanSelf;
        GameManager.onSave += SaveSelf;
    }

    void OnDisable()
    {
        onBossStart -= DisableTotemFunction;
        Boss.onBossStop -= EnableTotemFunction;
        GameManager.onCleanWorld -= CleanSelf;
        GameManager.onSave -= SaveSelf;
    }

    public BossTotemInfo ConvertToInteractableInfo()
    {
        BossTotemInfo info = new BossTotemInfo();

        info.location = transform.position;
        info.interactableType = InteractableInfo.InteractableType.BossTotem;
        info.cost = cost;
        info.heldBoss = bossEnum;
        info.reward = reward;

        return info;
    }

    public void CleanSelf()
    {
        GameManager.Instance.currentPortal.bossTotemInfos.Add(ConvertToInteractableInfo());

        Destroy(gameObject);
    }

    public void SaveSelf()
    {
        GameManager.Instance.currentPortal.bossTotemInfos.Add(ConvertToInteractableInfo());
    }
}
