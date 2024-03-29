using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : Party
{
    //protected const int PartyAmount = 3;
    //public bool isTurn { get; private set; }
    //public PartyMember[] activePartyMembers { get; protected set; } // party members in the party

    // Bullet Pattern combinations


    protected override void Awake()
    {
        base.Awake();
        activePartyMembers = new Enemy[PartyAmount];
    }
    
    /// <summary>
    /// returns the spot of the first enemy it finds which isnt defeated
    /// </summary>
    /// <returns></returns>
    public int NextInLineAttack() 
    {
        for (int i = 0; i < CountActiveMembers(); i++)
        {
            if (activePartyMembers[i].hp > 0) return i;
        }
        return -1;
    }

    /// <summary>
    /// returns the spot of the first enemy it finds which can be spared
    /// </summary>
    /// <returns></returns>
    public int NextInLineSpare()
    {
        for (int i = 0; i < CountActiveMembers(); i++)
        {
            if (activePartyMembers[i].hp > 0 && ((Enemy)activePartyMembers[i]).spareMeter >= Enemy.spareMeterMax) return i;
        }
        return -1;
    }

    /// <summary>
    /// Adds a party member to the party. Make sure the added member is of a Enemy Type
    /// </summary>
    /// <param name="add"></param>
    public override void AddMember(PartyMember add)
    {
        if (add is Enemy) base.AddMember(add);
        else Debug.Log("Make sure the added member is of a Enemy Type");
    }

    public override void RemoveMember(int id)
    {
        base.RemoveMember(id);
    }

    public override void SortParty()
    {
        base.SortParty();
    }

    public void AddToSpareMeterAll(int add)
    {
        for (int i = 0; i < activePartyMembers.Length; i++)
        {
            ((Enemy)activePartyMembers[i]).AddToSpareMeter(add);
        }
    }

    public override void PartyTurn(bool trigger)
    {
        base.PartyTurn(trigger);
    }

    public override bool IsPartyDown()
    {
        return base.IsPartyDown();
    }

    public override int CountActiveMembers()
    {
        return base.CountActiveMembers();
    }
}
