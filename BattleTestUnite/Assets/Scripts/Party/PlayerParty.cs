using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerParty : Party
{
    //protected const int PartyAmount = 3;
    //public bool isTurn { get; private set; }
    //public PartyMember[] activePartyMembers { get; private set; } // party members in the party
    public PlayerPartyMember[] partyMembers { get; private set; }
    public int currentMemberTurn;

    protected override void Awake()
    {
        base.Awake();
        activePartyMembers = new PlayerPartyMember[PartyAmount];
        partyMembers = new PlayerPartyMember[4];
        partyMembers[0] = new PlayerPartyMember(0, "Kris", 270, 2, 18, 0, false, Consts.KrisBlue);    // Kris
        partyMembers[1] = new PlayerPartyMember(1, "Susie", 340, 2, 23, 8, true, Consts.SusieMagenta);   // Susie
        partyMembers[2] = new PlayerPartyMember(2, "Ralsei", 250, 2, 16, 16, true, Consts.RalseiGreen); // Ralsei
        partyMembers[3] = new PlayerPartyMember(3, "Noelle", 160, 1, 7, 14, true, Consts.NoelleYellow);   // Noelle
        AddMember(partyMembers[0]);
        AddMember(partyMembers[1]);
        AddMember(partyMembers[2]);
    }

    /// <summary>
    /// Adds a party member to the party. Make sure the added member is of a Player Type
    /// </summary>
    /// <param name="add"></param>
    public override void AddMember(PartyMember add)
    {
        if (add is PlayerPartyMember) base.AddMember(add);
        else Debug.Log("Cannot add an enemy type to a Player Party, Make sure the added member is of a Player Type");
    }

    public override void RemoveMember(int id)
    {
        base.RemoveMember(id);
    }

    public override void SortParty()
    {
        base.SortParty();
    }

    public override void PartyTurn(bool trigger)
    {
        base.PartyTurn(trigger);
        if (trigger) currentMemberTurn = 1;
        else currentMemberTurn = 0;
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
