using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActTitle : MonoBehaviour
{
    [HideInInspector] public int spot;
    [HideInInspector] public bool isSelected;
    [HideInInspector] public PlayerParty playerP;
    [HideInInspector] public bool isOn = false;

    void Start()
    {
        if (isOn)
        {
            playerP = transform.parent.transform.parent.GetComponent<HudText>().playerParty;
            transform.GetComponent<TextMeshProUGUI>().text = PlayerParty.inventory.items[spot].nickname;
        }
    }

}
