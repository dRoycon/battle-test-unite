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
    [HideInInspector] public int subSubSelect;
    [HideInInspector] public bool isDone;
    [HideInInspector] public bool subSubOpt;
    public bool statsEnemy;

    void Start()
    {
        playerParty = FindObjectOfType<PlayerParty>();
        enemyP = FindObjectOfType<EnemyParty>();
        isDone = false;
        subSubOpt = false;
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
                useAct();
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
                    if (!subSubOpt) // sub options done
                    {
                        subSelect = transform.GetChild(0).GetComponent<playerSubOptions>().res;

                        Destroy(transform.GetChild(0).gameObject);
                        if (type == 3 && subSelect != -100)
                        {
                            if (!PlayerParty.inventory.items[(-subSelect)-1].healAll)
                            {
                                subSubOpt = true;
                                changeType(1);
                                transform.GetChild(0).GetComponent<playerSubOptions>().type = 1;
                            }
                            else
                            {
                                subSubSelect = -1;
                                isDone = true;
                            }
                        }
                        else isDone = true;
                        //Debug.Log("opt: " + subSelect);
                    }
                    else // sub sub options done
                    {
                        subSubSelect = transform.GetChild(0).GetComponent<playerSubOptions>().res;
                        if (subSubSelect == -100)
                        {
                            Destroy(transform.GetChild(0).gameObject);
                            subSubOpt = false;
                            changeType(3);
                            transform.GetChild(0).GetComponent<playerSubOptions>().type = 3;
                        }
                        else
                        {
                            Destroy(transform.GetChild(0).gameObject);
                            isDone = true;
                            //Debug.Log("opt: " + subSelect + " , " + subSubSelect);
                            subSubOpt = false;
                        }
                    }
                }
            }
        }
    }

    private void stats(bool statsEnemy) // type 1 & 2 - health..spare
    {
        this.statsEnemy = statsEnemy;
        GameObject SubOpt = new GameObject();
        SubOpt.AddComponent<playerSubOptions>();
        SubOpt.transform.SetParent(transform, false);
        SubOpt.GetComponent<playerSubOptions>().type = 1;
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

    private void useAct() // type 3 - item/spell/act
    {
        GameObject SubOpt = new GameObject();
        SubOpt.AddComponent<playerSubOptions>();
        SubOpt.transform.SetParent(transform, false);
        SubOpt.GetComponent<playerSubOptions>().type = 3;
        SubOpt.name = "SubOptions";

        GameObject[] actions = new GameObject[PlayerParty.inventory.Count()];
        for (int i = 0; i < PlayerParty.inventory.Count(); i++)
        {
            actions[i] = Instantiate(text, new Vector2(0, 0), Quaternion.identity);
            actions[i].transform.SetParent(SubOpt.transform, false);
            actions[i].GetComponent<ActTitle>().isOn = true;
            actions[i].name = i+"";
            float x, y;
            if (i%2==0) x = actions[i].transform.localPosition.x - 464.3f;
            else x = actions[i].transform.localPosition.x;
            if (i>=Inventory.PocketSpace/2) y = actions[i].transform.localPosition.y - (((i- (Inventory.PocketSpace/2)) / 2) * 58) -300.9901f;
            else y = actions[i].transform.localPosition.y - (((i / 2) * 58)) - 300.9901f;
            actions[i].transform.localPosition = new Vector2(x, y);
            actions[i].GetComponent<ActTitle>().spot = i;
        }

        SubOpt.GetComponent<playerSubOptions>().UpdateChildren();
    }
}
