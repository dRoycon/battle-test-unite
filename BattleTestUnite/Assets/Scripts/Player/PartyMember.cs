using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember
{
    public int id { get; private set; }
    public string nickname { get; private set; }
    public int hp;
    public int maxHp { get; private set; }
    public float defensePower { get; private set; }
    public float attackPower { get; private set; }
    public float magicPower { get; private set; }
    public bool isTargeted { get; private set; } // if the partyMember is the target of an enemy attack

    public PartyMember(int id, string nickname, int maxHp, float defensePower, float attackPower, float magicPower)
    {
        this.id = id;
        this.nickname = nickname;
        this.maxHp = maxHp;
        this.defensePower = defensePower;
        this.attackPower = attackPower;
        this.magicPower = magicPower;
        hp = maxHp;
    }
}
