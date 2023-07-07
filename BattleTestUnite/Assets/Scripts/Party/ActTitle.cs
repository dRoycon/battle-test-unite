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
    [HideInInspector] public int type;

    void Start()
    {
        if (isOn)
        {
            type = transform.parent.GetComponent<playerSubOptions>().type;
            playerP = transform.parent.transform.parent.GetComponent<HudText>().playerParty;
            TextMeshProUGUI gui = transform.GetComponent<TextMeshProUGUI>();
            switch (type)
            {
                default: // item
                    gui.text = PlayerParty.inventory.items[spot].nickname;
                    break;
                case 4: // magic
                    gui.text = ((MagicUser)playerP.activePartyMembers[playerP.currentMemberTurn-1]).spells[spot].name;
                    break;
                case 5: // act
                    break;
            }
        }
    }

}
