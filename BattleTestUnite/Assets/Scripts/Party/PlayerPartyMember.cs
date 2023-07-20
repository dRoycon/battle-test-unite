 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyMember : PartyMember
{
    //public int id { get; private set; }
    //public string nickname { get; protected set; }
    //public int hp;
    //public int maxHp { get; protected set; }
    //public float defenseLevel { get; protected set; }
    //public float attackLevel { get; protected set; }
    public int magicLevel { get; protected set; }
    public float attackPower { get; protected set; }
    public float defensePower { get; protected set; }
    public float magicPower { get; protected set; }
    public Color color { get; protected set; }
    public Color accentColor1 { get; protected set; }
    public Color accentColor2 { get; protected set; }
    public bool hasMagic { get; protected set; }

    public PlayerPartyMember(int id, string nickname, int maxHp, int defenseLevel, int attackLevel, int magicLevel, bool hasMagic, Color color, Color accentColor1, Color accentColor2)
        : base(id, nickname, maxHp, defenseLevel, attackLevel)
    {
        this.magicLevel = magicLevel;
        this.color = color;
        this.hasMagic = hasMagic;
        this.accentColor1 = accentColor1;
        this.accentColor2 = accentColor2;
        magicPower = GetMagicPower(magicLevel);
        attackPower = GetAttackPower(attackLevel);
        defensePower = GetDefensePower(defenseLevel);
        PlayerParty.OverallMemberAmount++;
    }

    public PlayerPartyMember(int id, int maxHp, int defenseLevel, int attackLevel) : base (id, maxHp, defenseLevel, attackLevel)
    {
        magicLevel = 0;
        color = Color.white;
        accentColor1 = Color.gray;
        accentColor2 = Color.white;
        hasMagic = false;
        PlayerParty.OverallMemberAmount++;
    }

    protected static float GetMagicPower(int magicLevel)
    {
        float res;
        switch (magicLevel)
        {
            default:
                res = magicLevel;
                break;
            case 0:
                res = 0;
                break;
            case 18:
                res = 20.5f;
                break;
        }
        return res;
    }

    protected static float GetAttackPower(int attackLevel)
    {
        int res = 0;
        switch (attackLevel)
        {
            default:
                break;
        }
        return res;
    }

    protected static float GetDefensePower(int defenceLevel)
    {
        int res = 0;
        switch (defenceLevel)
        {
            default:
                break;
        }
        return res;
    }
}
