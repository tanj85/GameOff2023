using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulCrystalsUI : MonoBehaviour
{
    public TextMeshProUGUI soulCrystalText;

    private void OnEnable()
    {
        PortalInfo.onResourceChange += changeCrystalText;
    }
    private void OnDisable()
    {
        PortalInfo.onResourceChange -= changeCrystalText;
    }

    private void changeCrystalText(int _dreamEnergy, int _soulCrystal)
    {
        soulCrystalText.text = _soulCrystal.ToString();
    }
}
