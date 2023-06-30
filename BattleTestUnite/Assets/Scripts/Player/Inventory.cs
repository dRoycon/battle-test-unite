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
    /// 
    /// </summary>
    /// <param name="itemSpt"></param>
    /// <param name="memberSpt"></param>
    public void Use(int itemId, int memberSpt)
    {
        Consts.items[itemId].Use(memberSpt);
    }

    /// <summary>
    /// This method removes an in item your inventory and returns the id of that item so that the item would be used at the end of your turn.
    /// </summary>
    /// <param name="spot"></param>
    /// <returns></returns>
    public int RemoveItem(int spot)
    {
        int res = items[spot].id;
        items[spot] = null;
        SortInventory();
        return res;
    }

        /// <summary>
        /// This method should be used in the party's turn when going back after a member used an item
        /// </summary>
        /// <param name="spot"></param>
        /// <param name="id"></param>
    public void InsertItem(int spot, int id) 
    {
        if (Count() < PocketSpace)
        {
            Debug.Log("Inserted " + id + " to spot " + spot);
            for (int i = PocketSpace-1; i >= spot; i--)
            {
                if (i == spot) items[i] = Consts.items[id];
                else if (items[i] == null)
                {
                    items[i] = items[i-1];
                    items[i-1] = null;
                }
                else Debug.Log("Error here");
            }
        }
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

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) res += ", empty";
            else res += ", " + items[i].nickname;
        }
        return res;
    }
}
