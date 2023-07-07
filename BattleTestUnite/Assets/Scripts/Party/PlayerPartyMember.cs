 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyMember : PartyMember
{
    //public int id { get; private set; }
    //public string nickname { get; protected set; }
    //public int hp;
    //public int maxHp { get; protected set; }
    //public float defensePower { get; protected set; }
    //public float attackPower { get; protected set; }
    public float magicPower { get; protected set; }
    public Color color { get; protected set; }
    public Color accentColor1 { get; protected set; }
    public Color accentColor2 { get; protected set; }
    public bool hasMagic { get; protected set; }

    public PlayerPartyMember(int id, string nickname, int maxHp, float defensePower, float attackPower, float magicPower, bool hasMagic, Color color, Color accentColor1, Color accentColor2)
        : base(id, nickname, maxHp, defensePower, attackPower)
    {
        this.magicPower = magicPower;
        this.color = color;
        this.hasMagic = hasMagic;
        this.accentColor1 = accentColor1;
        this.accentColor2 = accentColor2;
        PlayerParty.OverallMemberAmount++;
    }

    public PlayerPartyMember(int id, int maxHp, float defensePower, float attackPower) : base (id, maxHp, defensePower, attackPower)
    {
        magicPower = 0;
        color = Color.white;
        accentColor1 = Color.gray;
        accentColor2 = Color.white;
        hasMagic = false;
        PlayerParty.OverallMemberAmount++;
    }
}
