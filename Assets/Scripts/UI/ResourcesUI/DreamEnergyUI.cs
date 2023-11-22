using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DreamEnergyUI : MonoBehaviour
{
    public TextMeshProUGUI dreamEnergyText;

    private void OnEnable()
    {
        PortalInfo.onResourceChange += changeEnergyText;
    }
    private void OnDisable()
    {
        PortalInfo.onResourceChange -= changeEnergyText;
    }

    private void changeEnergyText(int _dreamEnergy, int _soulCrystal)
    {
        dreamEnergyText.text = _dreamEnergy.ToString();
    }
}
