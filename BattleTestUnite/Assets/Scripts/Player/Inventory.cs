using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IUseAct
{
    public const int PocketSpace = 12; 
    public Item[] items;

    public Inventory()
    {
        items = new Item[PocketSpace];
    }

    public void AddItem(Item add)
    {
        SortInventory();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = add;
                return;
            }
        }
    }

    /// <summary>
    /// if the item heals all members, set memberSpt to anything
    /// </summary>
    /// <param name="itemSpt"></param>
    /// <param name="memberSpt"></param>
    public void Use(int itemSpt, int memberSpt)
    {
        if (!items[itemSpt].healAll) // heals one
        {
            Consts.playerParty.activePartyMembers[memberSpt].Heal(items[itemSpt].hp[memberSpt]);
        }
        else // heals all
        {
            for (int i = 0; i < Consts.playerParty.activePartyMembers.Length; i++)
            {
                Consts.playerParty.activePartyMembers[i].Heal(items[itemSpt].hp[i]);
            }
        }
        items[itemSpt] = null;
        SortInventory();
    }

    public void SortInventory()
    {
        int pos = -1;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                if (i == 0) pos = i;
                else if (items[i - 1] != null)
                {
                    pos = i;
                }
            }
            else if (pos != -1)
            {
                items[pos] = items[i];
                items[i] = null;
                pos++;
            }
        }
    }

    public int Count()
    {
        int cnt = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) break;
            cnt++;
        }
        return cnt;
    }
}
