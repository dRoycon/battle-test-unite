using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id { get; private set; }
    public string itemName { get; private set; }
    public string nickname { get; private set; }
    public float hp { get; private set; }
    public int tp { get; private set; }
    public int ability { get; private set; }
    public string description { get; private set; }
    public string shortDescription { get; private set; }

    #region builders
    public Item(int _id, float _hp, string _itemName, string _nickname)
    {
        id = _id;
        itemName = _itemName;
        nickname = _nickname;
        hp = _hp;
        ability = 0;
        tp = 0;
    }

    public Item(int id, string itemName, string nickname, float hp, int tp, int ability)
    {
        this.id = id;
        this.itemName = itemName;
        this.nickname = nickname;
        this.hp = hp;
        this.tp = tp;
        this.ability = ability;
    }
    #endregion

    public void Use(int partyMemberPosition) // party member position is the party member's position in line, starts at 1
    {
        // Heal Player
        // Add Tp to Player
        // Use ability
    }
}
