using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniMercyBar : MonoBehaviour
{
    private int spot;
    private bool isEnemy;
    void Start()
    {
        spot = GetComponentInParent<PartyMemText>().spot;
        isEnemy = GetComponentInParent<PartyMemText>().isEnemy;
        if (isEnemy)
        {
            EnemyParty party = GetComponentInParent<PartyMemText>().enemyP;

            float spareMeter = (float)((Enemy)party.activePartyMembers[spot]).spareMeter;
            if (spareMeter >= Enemy.spareMeterMax)
                transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().color = Consts.NoelleYellow;

            transform.GetChild(1).GetComponent<Image>().fillAmount = (float)(spareMeter / (float)(Enemy.spareMeterMax));
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (Mathf.FloorToInt(100 * (spareMeter / (float)(Enemy.spareMeterMax)))) + "%";
        }
    }
}
