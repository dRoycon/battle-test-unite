using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember
{
    public int id { get; private set; }
    public string nickname { get; protected set; }
    public int hp;
    public int maxHp { get; protected set; }
    public float defensePower { get; protected set; }
    public float attackPower { get; protected set; }

    public PartyMember(int id, string nickname, int maxHp, float defensePower, float attackPower)
    {
        this.id = id;
        this.nickname = nickname;
        this.maxHp = maxHp;
        this.defensePower = defensePower;
        this.attackPower = attackPower;
        hp = maxHp;
    }

    public PartyMember(int id, int maxHp, float defensePower, float attackPower)
    {
        this.id = id;
        nickname = "dummy";
        this.maxHp = maxHp;
        this.defensePower = defensePower;
        this.attackPower = attackPower;
        hp = maxHp;
    }

    public void Heal(int amt)
    {
        hp = Mathf.Clamp(hp + amt, hp, maxHp);
    }
}
