using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public const int PartyAmount = 3;
    public bool isTurn { get; private set; }
    public PartyMember[] activePartyMembers { get; protected set; } // party members in the party

    protected virtual void Awake()
    {
    }

    public virtual void AddMember(PartyMember add)
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

    public virtual void RemoveMember(int id)
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

    public virtual void SortParty()
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

    public virtual void PartyTurn(bool trigger)
    {
        isTurn = trigger;
    }

    public virtual bool IsPartyDown()
    {
        for (int i = 0; i < activePartyMembers.Length; i++)
        {
            if (activePartyMembers[i] == null) break;
            else if (activePartyMembers[i].hp > 0) return false;
        }
        return true;
    }

    public virtual int CountActiveMembers()
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
