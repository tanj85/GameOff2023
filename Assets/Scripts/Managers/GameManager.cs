using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

    public PortalInfo currentPortal = null;

    public Dictionary<int, PortalInfo> portalDict = new Dictionary<int, PortalInfo>();

    // Start is called before the first frame update
    void Start()
    {
        portalDict.Add(0, new PortalInfo(0, 1, 1f, 10));
        portalDict[0].AddSoulCrystals(10);
        portalDict.Add(1, new PortalInfo(1, 2, 2f, 35));
        portalDict[1].AddSoulCrystals(99);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadWorld(portalDict[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadWorld(portalDict[1]);
        }
    }

    public void generateRandomWorld(int level, int dreamEnergy, int? parentID = null, int? childID = null)
    {
        int i = 0;
        while (true)
        {
            if (!portalDict.ContainsKey(i)){
                portalDict.Add(i, new PortalInfo(i, level, Mathf.Pow(10f, level), dreamEnergy));
                if (parentID.HasValue)
                {
                    portalDict[i].SetParentID(parentID.Value);
                }
                if (childID.HasValue)
                {
                    portalDict[i].childrenID.Add(childID.Value);
                }
                break;
            }
            i++;
        }
    }

    // Loads a portal by clearing the current game state to base and then setting up a new world
    public void LoadWorld(PortalInfo portal){
        ClearCurrentWorld();
        SetupWorld(portal);
    }

    /*
     * 1. Set currentPortal as new portal
     * 1.5 Update the world look?
     * 2. force invoke currentPortal resources
     * 3. Instantiate all the interactables on the level
     * 4. Instantiate the Player
     */
    private void SetupWorld(PortalInfo portal)
    {
        currentPortal = portal;

        currentPortal.InvokeResourceChange();
        //update world look?

        // Instantiate interactables
        // Instantiate player
    }


    /*
     * 1. Destroy all active entities (player, monsters, bosses)
     * 2. Destroy all interactables
     */
    private void ClearCurrentWorld(){
        // Destroy active entities
        // Destroy interactables
        currentPortal = null;
    }
}
