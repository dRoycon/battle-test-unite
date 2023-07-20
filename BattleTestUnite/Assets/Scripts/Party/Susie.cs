using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Susie : MagicUser
{
    //////public int id { get; private set; }
    //////public string nickname { get; protected set; }
    //////public int hp;
    //////public int maxHp { get; protected set; }
    //////public float defenseLevel { get; protected set; }
    //////public float attackLevel { get; protected set; }
    ////public float magicLevel { get; protected set; }
    ////public Color color { get; protected set; }
    ////public Color accentColor1 { get; protected set; }
    ////public Color accentColor2 { get; protected set; }
    ////public bool hasMagic { get; protected set; }
    //public const int MAX_SPELLS = 6;
    //public int count; // spell amount
    //public Magic[] spells { get; protected set; }
    public const int susieId = 1;
    public Susie(int id, int maxHp, int defenseLevel, int attackLevel, int magicLevel) 
        : base(id, maxHp, defenseLevel, attackLevel, magicLevel)
    {
        AddSpell(Consts.spells["rudeBuster"]);
        AddSpell(Consts.spells["ultimateHeal"]);
        color = Consts.SusieMagenta;
        accentColor1 = Consts.SusieAccent1;
        accentColor2 = Consts.SusieAccent2;
        nickname = "Susie";
    }
}
