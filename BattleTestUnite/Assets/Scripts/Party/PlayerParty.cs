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
    public static int OverallMemberAmount = 4;
    public static Inventory inventory = new Inventory();

    protected override void Awake()
    {
        base.Awake();
        activePartyMembers = new PlayerPartyMember[PartyAmount];
        partyMembers = new PlayerPartyMember[4];
        partyMembers[0] = Consts.kris;    // Kris
        partyMembers[1] = Consts.susie;   // Susie
        partyMembers[2] = Consts.ralsei; // Ralsei
        partyMembers[3] = Consts.noelle;   // Noelle
        AddMember(partyMembers[0]);
        AddMember(partyMembers[1]);
        AddMember(partyMembers[2]);
        inventory.AddItem(Consts.items[0]);
        inventory.AddItem(Consts.items[1]);
        inventory.AddItem(Consts.items[2]);
        inventory.AddItem(Consts.items[3]);
        inventory.AddItem(Consts.items[0]);
        inventory.AddItem(Consts.items[1]);
        inventory.AddItem(Consts.items[4]);
        inventory.AddItem(Consts.items[2]);
        inventory.AddItem(Consts.items[3]);
        inventory.AddItem(Consts.items[5]);
        inventory.AddItem(Consts.items[1]);
        Consts.playerParty = gameObject.GetComponent<PlayerParty>();
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
