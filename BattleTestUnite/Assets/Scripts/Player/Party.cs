using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Party : MonoBehaviour
{
    private const int PartyAmount = 3;
    public bool isPlayerTurn { get; private set; }
    public PartyMember[] activePartyMembers { get; private set; } // party members in the party
    public PartyMember[] partyMembers { get; private set; }
    public int currentMemberTurn;

    private void Awake()
    {
        partyMembers = new PartyMember[4];
        partyMembers[0] = new PartyMember(0, "Kris", 270, 2, 18, 0, false, Consts.KrisBlue);    // Kris
        partyMembers[1] = new PartyMember(1, "Susie", 340, 2, 23, 8, true, Consts.SusieMagenta);   // Susie
        partyMembers[2] = new PartyMember(2, "Ralsei", 250, 2, 16, 16, true, Consts.RalseiGreen); // Ralsei
        partyMembers[3] = new PartyMember(3, "Noelle", 160, 1, 7, 14, true, Consts.NoelleYellow);   // Noelle
        activePartyMembers = new PartyMember[PartyAmount];
        AddMember(partyMembers[0]);
        AddMember(partyMembers[1]);
        AddMember(partyMembers[2]);
    }

    public void AddMember(PartyMember add)
    {
        for (int i = 0; i < activePartyMembers.Length; i++)
        {
            if (activePartyMembers[i] == null)
            {
                activePartyMembers[i] = add;
                break;
            }
        }
    }

    public void RemoveMember(int id)
    {
        for (int i = 0; i < activePartyMembers.Length; i++)
        {
            if (activePartyMembers[i].id == id)
            {
                activePartyMembers[i] = null;
                break;
            }
        }
        SortParty();
    }

    public void SortParty()
    {
        for (int i = 0; i < activePartyMembers.Length - 1; i++)
        {
            if (activePartyMembers[i] == null && activePartyMembers[i + 1] != null)
            {
                activePartyMembers[i] = activePartyMembers[i + 1];
                activePartyMembers[i + 1] = null;
            }
        }
    }

    public void PartyTurn(bool trigger)
    {
        isPlayerTurn = trigger;
        if (trigger) currentMemberTurn = 1;
        else currentMemberTurn = 0;
    }

    public bool IsPartyDown()
    {
        bool res = true;
        for (int i = 0; i < activePartyMembers.Length; i++)
        {
            if (activePartyMembers[i] == null) break;
            else if (activePartyMembers[i].hp > 0) res = false;
        }
        return res;
    }

    public int CountActiveMembers()
    {
        int cnt = 0;
        for (int i = 0; i < activePartyMembers.Length; i++)
        {
            if (activePartyMembers[i] != null) cnt++;
            else break;
        }
        return cnt;
    }
}
