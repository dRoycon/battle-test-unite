using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Susie : MagicUser
{
    //////public int id { get; private set; }
    //////public string nickname { get; protected set; }
    //////public int hp;
    //////public int maxHp { get; protected set; }
    //////public float defensePower { get; protected set; }
    //////public float attackPower { get; protected set; }
    ////public float magicPower { get; protected set; }
    ////public Color color { get; protected set; }
    ////public Color accentColor1 { get; protected set; }
    ////public Color accentColor2 { get; protected set; }
    ////public bool hasMagic { get; protected set; }
    //public const int MAX_SPELLS = 6;
    //public int count; // spell amount
    //public Magic[] spells { get; protected set; }

    public Susie(int id, int maxHp, float defensePower, float attackPower, float magicPower) 
        : base(id, maxHp, defensePower, attackPower, magicPower)
    {
        AddSpell(Consts.spells["rudeBuster"]);
        AddSpell(Consts.spells["ultimateHeal"]);
        color = Consts.SusieMagenta;
        accentColor1 = Consts.SusieAccent1;
        accentColor2 = Consts.SusieAccent2;
        nickname = "Susie";
    }
}
