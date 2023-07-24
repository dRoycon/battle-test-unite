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
                    int spellType = ((MagicUser)playerP.activePartyMembers[playerP.currentMemberTurn - 1]).GetSpellType(spot);
                    string name = playerP.activePartyMembers[playerP.currentMemberTurn - 1].nickname;
                    name = name.ToLower();
                    string text = ((MagicUser)playerP.activePartyMembers[playerP.currentMemberTurn - 1]).spells[spot].name;
                    switch (spellType)
                    {
                        default: // 0 - act
                            text = " <sprite name=act_" + name + ">" + text;
                            break;
                        case 1:
                            text = " <sprite name=fight_" + name + ">" + text;
                            break;
                        case 2:
                            text = " <sprite name=magic_" + name + ">" + text;
                            break;
                        case 3:
                            text = " <sprite name=sleep_" + name + ">" + text;
                            break;
                    }
                    gui.text = text;
                    break;
                case 5: // act
                    EnemyParty enemyP = transform.parent.transform.parent.GetComponent<HudText>().enemyP;
                    int enemySpt = transform.parent.transform.parent.GetComponent<HudText>().subSelect;
                    name = ((Enemy)enemyP.activePartyMembers[-enemySpt - 1]).actions[spot].name;
                    int ally1 = ((Enemy)enemyP.activePartyMembers[-enemySpt - 1]).actions[spot].ally1;
                    if (ally1 > -1)
                    {
                        string icons = "<sprite name=" + Consts.playerParty.partyMembers[ally1].nickname.ToLower() + "_0>";
                        int ally2 = ((Enemy)enemyP.activePartyMembers[-enemySpt - 1]).actions[spot].ally2;
                        if (ally2 > -1)
                            icons += "<sprite name=" + Consts.playerParty.partyMembers[ally2].nickname.ToLower() + "_0>";
                        name = icons + name;
                    }
                    gui.text = name;
                    break;
            }
        }
    }

}
