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

    // not needed to be saved
    private bool isCurrentBossTotem;

    public void Initialize(Boss.Bosses _boss, int _cost, int _reward)
    {
        bossEnum = _boss;
        boss = GameManager.Instance.bossPrefabDictionary[_boss];
        cost = _cost;
        reward = _reward;
    }

    public void Initialize(BossTotemInfo info)
    {
        Initialize(info.heldBoss, info.cost, info.reward);
    }

    void Start()
    {
        active = true;
    }

    void IInteractable.Interact()
    {
        if (active == true)
        {
            isCurrentBossTotem = true;
            Instantiate(boss, transform.position, Quaternion.identity);
            onBossStart.Invoke();
        }
        GameManager.Instance.SyncInteractables();
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
        Boss.onBossDie += spawnPortal;
        GameManager.onCleanWorld += CleanSelf;
        GameManager.onSyncInteractables += SaveSelf;
    }

    void OnDisable()
    {
        onBossStart -= DisableTotemFunction;
        Boss.onBossStop -= EnableTotemFunction;
        Boss.onBossDie -= spawnPortal;
        GameManager.onCleanWorld -= CleanSelf;
        GameManager.onSyncInteractables -= SaveSelf;
    }

    public BossTotemInfo ConvertToInteractableInfo()
    {
        BossTotemInfo info = new BossTotemInfo(bossEnum, cost, reward, transform.position);

        return info;
    }

    public void spawnPortal()
    {
        if (isCurrentBossTotem)
        {
            isCurrentBossTotem = false;
            GameObject interactable = Instantiate(GameManager.Instance.interactableDictionary[InteractableInfo.InteractableType.Portal],
                transform.position, Quaternion.identity);
            if (GameManager.Instance.currentPortal.hasParent)
            {
                int minReward = reward;
                if (reward < GameManager.Instance.currentPortal.output)
                {
                    minReward = GameManager.Instance.currentPortal.output;
                }
                else
                {
                    GameManager.Instance.currentPortal.output = reward;
                }
                interactable.GetComponent<Portal>().Initialize(GameManager.Instance.currentPortal.parentID, true, true, minReward);
                GameManager.Instance.currentPortal.SyncSenderDreamFlowAmts(minReward);
            }
            else
            {
                interactable.GetComponent<Portal>().Initialize(-1, false, true, reward);
                GameManager.Instance.currentPortal.output = reward;
            }
            Destroy(gameObject);
        }
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
