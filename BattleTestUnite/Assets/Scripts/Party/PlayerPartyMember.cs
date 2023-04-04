using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyMember : PartyMember
{
    //public int id { get; private set; }
    //public string nickname { get; private set; }
    //public int hp;
    //public int maxHp { get; private set; }
    //public float defensePower { get; private set; }
    //public float attackPower { get; private set; }
    public float magicPower { get; private set; }
    public Color color { get; private set; }
    public Color accentColor1 { get; private set; }
    public Color accentColor2 { get; private set; }
    public bool hasMagic { get; private set; }

    public PlayerPartyMember(int id, string nickname, int maxHp, float defensePower, float attackPower, float magicPower, bool hasMagic, Color color, Color accentColor1, Color accentColor2)
        : base(id, nickname, maxHp, defensePower, attackPower)
    {
        this.magicPower = magicPower;
        this.color = color;
        this.hasMagic = hasMagic;
        this.accentColor1 = accentColor1;
        this.accentColor2 = accentColor2;
    }
}
