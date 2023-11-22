using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DreamEnergyUI : MonoBehaviour
{
    public TextMeshProUGUI dreamEnergyText;

    private void OnEnable()
    {
        Debug.Log("DreamEnergyUI enabled");
        PortalInfo.onResourceChange += changeEnergyText;
    }
    private void OnDisable()
    {
        PortalInfo.onResourceChange -= changeEnergyText;
    }

    private void changeEnergyText(int _dreamEnergy, int _soulCrystal)
    {
        Debug.Log($"DreamEnergyUI changeEnergyText called, dream energy: {_dreamEnergy}");
        dreamEnergyText.text = _dreamEnergy.ToString();
    }
}
