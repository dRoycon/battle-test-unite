using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PartyMember
{
    //public int id { get; private set; }
    //public string nickname { get; protected set; }
    //public int hp;
    //public int maxHp { get; protected set; }
    //public float defenseLevel { get; protected set; }
    //public float attackLevel { get; protected set; }
    public int spareMeter;
    public const int spareMeterMax = 100;
    public string check;
    public bool isTired;
    public Act[] actions { get; private set; }
    public const int MAX_ACTS = 6;
    public int count; // amount of acts
    // Bullet Patterns and shit

    public Enemy(int id, string nickname, int maxHp, int defenseLevel, int attackLevel, string check) 
        : base(id, nickname, maxHp, defenseLevel, attackLevel)
    {
        spareMeter = 0;
        this.check = check;
        isTired = false;
        count = 0;
        actions = new Act[MAX_ACTS];
        AddAct(Consts.actions["check"]);
        AddAct(Consts.actions["TEST1"]);
        AddAct(Consts.actions["TEST2"]);
    }

    public bool CanBeSpared()
    {
        return spareMeter >= spareMeterMax;
    }

    public void AddToSpareMeter(int add)
    {
        spareMeter = Mathf.Clamp(spareMeter + add, 0, spareMeterMax);
    }

    public void AddAct(Act act)
    {
        if (actions[MAX_ACTS - 1] != null) return;
        count++;
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i] == null)
            {
                actions[i] = act;
                break;
            }
        }
    }

    public string Act(int action, int targetSpt)
    {
        string res = "";
        switch (id)
        {
            default: // 1 - check
                res = nickname + " - " + check;
                break;
            case 2:
                break;
        }
        actions[action].Use(targetSpt, Kris.krisId);
        return res;
    }
}
