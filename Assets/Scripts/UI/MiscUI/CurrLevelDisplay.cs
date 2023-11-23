using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrLevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    void Update()
    {
        text.text = GameManager.Instance.currentPortal.id.ToString() + "|" + GameManager.Instance.currentPortal.level.ToString();
    }
}
