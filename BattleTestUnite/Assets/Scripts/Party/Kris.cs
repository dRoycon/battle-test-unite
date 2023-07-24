using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kris : PlayerPartyMember
{
    ////public int id { get; private set; }
    ////public string nickname { get; protected set; }
    ////public int hp;
    ////public int maxHp { get; protected set; }
    ////public float defenseLevel { get; protected set; }
    ////public float attackLevel { get; protected set; }
    //public int magicLevel { get; protected set; }
    //public float attackPower { get; protected set; }
    //public float defensePower { get; protected set; }
    //public float magicPower { get; protected set; }
    //public Color color { get; protected set; }
    //public Color accentColor1 { get; protected set; }
    //public Color accentColor2 { get; protected set; }
    //public bool hasMagic { get; protected set; }
    public const int krisId = 0;

    public Kris(int id, int maxHp, int defenseLevel, int attackLevel) :
        base(id, maxHp, defenseLevel, attackLevel)
    {
        color = Consts.KrisBlue;
        accentColor1 = Consts.KrisAccent1;
        accentColor2 = Consts.KrisAccent2;
        nickname = "Kris";
    }
}
