using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PartyMember
{
    public int spareMeter;
    public const int spareMeterMax = 100;
    public string check;
    // Act commands specific to enemy
    // Bullet Patterns and shit

    public Enemy(int id, string nickname, int maxHp, float defensePower, float attackPower, int spareMeter, string check) 
        : base(id, nickname, maxHp, defensePower, attackPower)
    {
        this.spareMeter = spareMeter;
        this.check = check;
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
