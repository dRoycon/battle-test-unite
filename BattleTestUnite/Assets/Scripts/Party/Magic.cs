using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : IUseAct
{
    public int id { get; private set; }
    public int type { get; private set; } // 0-char.act(R-Act,S-Act-N-Act) / 1-attack / 2-heal / 3-spare
    public int tpCost { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string shortDescription { get; private set; }
    public bool isOnAll { get; private set; }

    public Magic(int id, int type, int tpCost, string name, string description, string shortDescription, bool isOnAll)
    {
        this.id = id;
        this.type = type;
        this.tpCost = tpCost;
        this.name = name;
        this.description = description;
        this.shortDescription = shortDescription + "<br><color=#FF7F27>" + tpCost + "% TP";
        this.isOnAll = isOnAll;
    }

    public void Use(int target, int userId) // cast
    {
        Debug.Log(name + " " + id);
        EnemyParty enemyP = Transform.FindObjectOfType<Battle>().enemyP;
        switch (id)
        {
            default: // 1 - healPrayer
                float mp = Consts.playerParty.partyMembers[userId].magicPower;
                int hp = (int)Mathf.Ceil(mp * 7.85f);
                Consts.playerParty.activePartyMembers[target].Heal(hp);
                break;
            case 2: // 2 - pacify
                if (enemyP != null)
                {
                    if (enemyP.activePartyMembers[target] != null && enemyP.activePartyMembers[target].hp > 0)
                    {
                        if (((Enemy)enemyP.activePartyMembers[target]).isTired)
                        {
                            Debug.Log(enemyP.activePartyMembers[target].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target].id);
                        }
                        else Debug.Log(enemyP.activePartyMembers[target].nickname + " isn't tired");
                    }
                    else
                    {
                        target = enemyP.NextInLineSpare();
                        if (target != -1)
                        {
                            if (((Enemy)enemyP.activePartyMembers[target]).isTired)
                            {
                                Debug.Log(enemyP.activePartyMembers[target].nickname + " was spared");
                                enemyP.RemoveMember(enemyP.activePartyMembers[target].id);
                            }
                            else Debug.Log(enemyP.activePartyMembers[target].nickname + " isn't tired");
                        }
                    }
                }
                break;
            case 3: // 3 - rudeBuster
                if (enemyP != null)
                {
                    float amp = 32.6f;
                    if (enemyP.activePartyMembers[target] != null && enemyP.activePartyMembers[target].hp > 0)
                    {
                        mp = Consts.playerParty.partyMembers[userId].magicPower;
                        hp = (int)Mathf.Ceil(mp * amp);
                        enemyP.activePartyMembers[target].hp -= hp;
                    }
                    else
                    {
                        target = enemyP.NextInLineAttack();
                        if (target != -1)
                        {
                            if (((Enemy)enemyP.activePartyMembers[target]).hp>0)
                            {
                                mp = Consts.playerParty.partyMembers[userId].magicPower;
                                hp = (int)Mathf.Ceil(mp * amp);
                                enemyP.activePartyMembers[target].hp -= hp;
                            }
                        }
                    }
                }
                    break;
            case 4: // 4 - ultimateHeal
                mp = Consts.playerParty.partyMembers[userId].magicPower;
                hp = (int)Mathf.Ceil(mp * 3.85f);
                Consts.playerParty.activePartyMembers[userId].Heal(hp);
                break;
            case 5: // 5 - sleepMist
                if (enemyP != null)
                {
                    for (int i = 0; i < enemyP.activePartyMembers.Length; i++)
                    {
                        if (enemyP.activePartyMembers[target] != null)
                        {
                            if (enemyP.activePartyMembers[target].hp > 0 && ((Enemy)(enemyP.activePartyMembers[target])).isTired)
                            {
                                Debug.Log(enemyP.activePartyMembers[target].nickname + " was spared");
                                enemyP.RemoveMember(enemyP.activePartyMembers[target].id);
                            }
                        }
                    }
                }
                break;
            case 6: // 6 - iceShock
                if (enemyP != null)
                {
                    float amp = 13.4f;
                    if (enemyP.activePartyMembers[target] != null && enemyP.activePartyMembers[target].hp > 0)
                    {
                        mp = Consts.playerParty.partyMembers[userId].magicPower;
                        hp = (int)Mathf.Ceil(mp * amp);
                        Debug.Log(hp);
                        enemyP.activePartyMembers[target].hp -= hp;
                    }
                    else
                    {
                        target = enemyP.NextInLineAttack();
                        if (target != -1)
                        {
                            if (((Enemy)enemyP.activePartyMembers[target]).hp > 0)
                            {
                                mp = Consts.playerParty.partyMembers[userId].magicPower;
                                hp = (int)Mathf.Ceil(mp * amp);
                                Debug.Log(hp);
                                enemyP.activePartyMembers[target].hp -= hp;
                            }
                        }
                    }
                }
                break;
            case 7: // 7 - snowGrave
                Debug.Log("lmao no");
                break;
        }
    }
}
