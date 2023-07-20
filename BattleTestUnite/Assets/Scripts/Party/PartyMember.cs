using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember
{
    public int id { get; private set; }
    public string nickname { get; protected set; }
    public int hp;
    public int maxHp { get; protected set; }
    public int defenseLevel { get; protected set; }
    public int attackLevel { get; protected set; }

    public PartyMember(int id, string nickname, int maxHp, int defenseLevel, int attackLevel)
    {
        this.id = id;
        this.nickname = nickname;
        this.maxHp = maxHp;
        this.defenseLevel = defenseLevel;
        this.attackLevel = attackLevel;
        hp = maxHp;
    }

    public PartyMember(int id, int maxHp, int defenseLevel, int attackLevel)
    {
        this.id = id;
        nickname = "dummy";
        this.maxHp = maxHp;
        this.defenseLevel = defenseLevel;
        this.attackLevel = attackLevel;
        hp = maxHp;
    }

    public void Heal(int amt)
    {
        hp = Mathf.Clamp(hp + amt, hp, maxHp);
    }
}
