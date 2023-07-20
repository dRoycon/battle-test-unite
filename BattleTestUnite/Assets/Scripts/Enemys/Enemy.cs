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

    // Act commands specific to enemy
    // Bullet Patterns and shit

    public Enemy(int id, string nickname, int maxHp, int defenseLevel, int attackLevel, string check) 
        : base(id, nickname, maxHp, defenseLevel, attackLevel)
    {
        spareMeter = 0;
        this.check = check;
        isTired = false;
    }

    public bool CanBeSpared()
    {
        return spareMeter >= spareMeterMax;
    }

    public void AddToSpareMeter(int add)
    {
        spareMeter = Mathf.Clamp(spareMeter + add, 0, spareMeterMax);
    }
}
