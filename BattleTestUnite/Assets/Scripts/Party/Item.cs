using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MemberAction
{
    //public int id { get; protected set; }
    //public string itemName { get; protected set; }
    //public string nickname { get; protected set; }
    //public string description { get; protected set; }
    public int[] hp { get; private set; }
    public bool healAll { get; private set; }


    #region builders

    /// <summary>
    /// Make a regular Item
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_hp"></param>
    /// <param name="_itemName"></param>
    /// <param name="_nickname"></param>
    /// <param name="healAll"></param>
    public Item(int _id, int _hp, string _fullName, string _nickname, string description, string shortDescription, bool healAll) : base(_id, _fullName, _nickname, description, shortDescription)
    {
        this.healAll = healAll;
        hp = new int[PlayerParty.OverallMemberAmount];
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i] = _hp;
        }
    }

    /// <summary>
    /// Make an Item that heals a specific hp amount based on the party member
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_hpArr"></param>
    /// <param name="_itemName"></param>
    /// <param name="_nickname"></param>
    /// <param name="healAll"></param>
    public Item(int _id, int[] _hpArr, string _fullName, string _nickname, string description, string shortDescription, bool healAll) : base(_id, _fullName, _nickname, description, shortDescription)
    {
        this.healAll = healAll;
        this.description = description;
        hp = new int[PlayerParty.OverallMemberAmount];
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i] = _hpArr[i];
        }
    }
    #endregion

    /// <summary>
    /// this method should be used at the end of the player's turn.
    /// </summary>
    /// <param name="memberSpt"></param>
    public void Use(int memberSpt)
    {
        if (!healAll) // heals one
        {
            Consts.playerParty.activePartyMembers[memberSpt].Heal(hp[memberSpt]);
        }
        else // heals all
        {
            for (int i = 0; i < Consts.playerParty.activePartyMembers.Length; i++)
            {
                Consts.playerParty.activePartyMembers[i].Heal(hp[Consts.playerParty.activePartyMembers[i].id]);
            }
        }
    }

}
