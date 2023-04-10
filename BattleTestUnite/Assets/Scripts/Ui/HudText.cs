using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudText : MonoBehaviour
{
    // 0 - none , 1 - PlayerPartyMemeber + health , 2 - EnemyPartyMemeber + health + spare, 3 - item/spell + description , 4 - diolouge 
    [HideInInspector] public int type;
    [SerializeField] public GameObject text;
    [SerializeField] public GameObject statsText;
    [SerializeField] public TMP_FontAsset DT_sans;
    [SerializeField] public TMP_FontAsset DT_mono;
    [HideInInspector] public PlayerParty playerParty;
    [HideInInspector] public EnemyParty enemyP;
    [HideInInspector] public int subSelect;
    [HideInInspector] public bool isDone;
    public bool statsEnemy;

    void Start()
    {
        playerParty = FindObjectOfType<PlayerParty>();
        enemyP = FindObjectOfType<EnemyParty>();
        isDone = false;
    }

    public void changeType(int type)
    {
        this.type = type;
        switch (type)
        {
            default:
                // nothing
                break;
            case 1:
                stats(false);
                break;
            case 2:
                stats(true);
                break;
            case 3:
                // item / spell description
                break;
            case 4:
                // diolouge
                break;
        }
    }

    private void FixedUpdate()
    {
        if (!isDone)
        {
            if (type != 0)
            {
                if (transform.GetChild(0).GetComponent<playerSubOptions>().isDone)
                {
                    subSelect = transform.GetChild(0).GetComponent<playerSubOptions>().res;
                    Destroy(transform.GetChild(0).gameObject);
                    isDone = true;
                }
            }
        }
    }

    private void stats(bool statsEnemy) // type 1 & 2
    {
        this.statsEnemy = statsEnemy;
        GameObject SubOpt = new GameObject();
        SubOpt.AddComponent<playerSubOptions>();
        SubOpt.transform.SetParent(transform, false);
        SubOpt.name = "SubOptions";
        SubOpt.GetComponent<playerSubOptions>().isEnemy = statsEnemy;
        Party party;
        if (statsEnemy) party = enemyP;
        else party = playerParty;

        GameObject[] memberStats = new GameObject[Party.PartyAmount];
        for (int i = 0; i < party.CountActiveMembers(); i++)
        {
            memberStats[i] = Instantiate(statsText, new Vector2(0, 0), Quaternion.identity);
            memberStats[i].transform.SetParent(SubOpt.transform, false);
            memberStats[i].transform.localPosition = new Vector2(memberStats[i].transform.localPosition.x, memberStats[i].transform.localPosition.y - (i * 58));
            memberStats[i].GetComponent<PartyMemText>().spot = i;
            if (!statsEnemy) Destroy(memberStats[i].transform.GetChild(1).gameObject);
        }

        //titles
        GameObject hpTitle = Instantiate(text, new Vector2(0, 0), Quaternion.identity);
        hpTitle.transform.SetParent(SubOpt.transform, false);
        hpTitle.transform.localPosition = new Vector2(312.9f, -262.2f);
        hpTitle.transform.localScale = new Vector2(1, 0.52f);
        TextMeshProUGUI hpTitleTMPG = hpTitle.GetComponent<TextMeshProUGUI>();
        hpTitleTMPG.text = "HP";
        hpTitleTMPG.fontSize = 57;
        hpTitleTMPG.font = DT_mono;
        hpTitleTMPG.characterSpacing = -3;
        if (statsEnemy)
        {
            GameObject mercyTitle = Instantiate(hpTitle, new Vector2(0, 0), Quaternion.identity);
            mercyTitle.transform.SetParent(SubOpt.transform, false);
            mercyTitle.transform.localPosition = new Vector2(514.3f, -262.2f);
            mercyTitle.GetComponent<TextMeshProUGUI>().text = "MERCY";
        }

        SubOpt.GetComponent<playerSubOptions>().UpdateChildren();
    }
}
