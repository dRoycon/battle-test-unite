using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMemText : MonoBehaviour
{
    [HideInInspector] public int spot;
    [HideInInspector] public bool isSelected;
    [HideInInspector] public EnemyParty enemyP;
    [HideInInspector] public PlayerParty playerP;
    [HideInInspector] public bool isEnemy;

    void Start()
    {
        isEnemy = transform.parent.GetComponentInParent<HudText>().statsEnemy;

        if (isEnemy)
        {
            enemyP = transform.parent.transform.parent.GetComponent<HudText>().enemyP;
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = enemyP.activePartyMembers[spot].nickname;
        }
        else
        {
            playerP = transform.parent.transform.parent.GetComponent<HudText>().playerParty;
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = playerP.activePartyMembers[spot].nickname;
        }
    }


    void Update()
    {
        
    }
}
