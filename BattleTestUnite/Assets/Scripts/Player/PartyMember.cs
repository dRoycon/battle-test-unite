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
    public Color color { get; private set; }
    public bool hasMagic { get; private set; }

    public PartyMember(int id, string nickname, int maxHp, float defensePower, float attackPower, float magicPower, bool hasMagic, Color color)
    {
        this.id = id;
        this.nickname = nickname;
        this.maxHp = maxHp;
        this.defensePower = defensePower;
        this.attackPower = attackPower;
        this.magicPower = magicPower;
        this.color = color;
        this.hasMagic = hasMagic;
        hp = maxHp;
    }
}
