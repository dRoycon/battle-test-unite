using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniHpBar : MonoBehaviour
{
    private int spot;
    private bool isEnemy;
    void Start()
    {
       spot = GetComponentInParent<PartyMemText>().spot;
        isEnemy = GetComponentInParent<PartyMemText>().isEnemy;
        Party party;
        if (isEnemy) party = GetComponentInParent<PartyMemText>().enemyP;
        else party = GetComponentInParent<PartyMemText>().playerP;

        transform.GetChild(1).GetComponent<Image>().fillAmount = (float)((float)party.activePartyMembers[spot].hp / (float)party.activePartyMembers[spot].maxHp);
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (Mathf.FloorToInt(100*((float)party.activePartyMembers[spot].hp / (float)party.activePartyMembers[spot].maxHp)))+"%";
    }
}
