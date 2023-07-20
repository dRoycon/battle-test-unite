using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUser : PlayerPartyMember
{
    ////public int id { get; private set; }
    ////public string nickname { get; protected set; }
    ////public int hp;
    ////public int maxHp { get; protected set; }
    ////public float defenseLevel { get; protected set; }
    ////public float attackLevel { get; protected set; }
    //public float magicLevel { get; protected set; }
    //public Color color { get; protected set; }
    //public Color accentColor1 { get; protected set; }
    //public Color accentColor2 { get; protected set; }
    //public bool hasMagic { get; protected set; }
    public const int MAX_SPELLS = 12;
    public int count; // amount of spells
    public Magic[] spells { get; protected set; }
     
    public MagicUser(int id, int maxHp, int defenseLevel, int attackLevel, int magicLevel) : base(id, maxHp, defenseLevel, attackLevel)
    {
        hasMagic = true;
        this.magicLevel = magicLevel;
        spells = new Magic[MAX_SPELLS];
        count = 0;
        magicPower = GetMagicPower(magicLevel);
        attackPower = GetAttackPower(attackLevel);
        defensePower = GetDefensePower(defenseLevel);
    }

    public void CastSpell(int num, int target)
    {
        if (!(num >= 0 && num < spells.Length)) return;
        Debug.Log(nickname + " casts:");
        spells[num].Use(target, id);
    }

    public int GetSpellType(int num)
    {
        if (!(num >= 0 && num < spells.Length)) return -1;
        return spells[num].type;
    }

    public void AddSpell(Magic sp)
    {
        if (spells[MAX_SPELLS - 1] != null) return;
        count++;
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] == null)
            {
                spells[i] = sp;
                break;
            }
        }
    }
}
