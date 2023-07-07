using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : IUseAct
{
    public int id { get; private set; }
    public int type { get; private set; } // 0-char.act(R-Act,S-Act-N-Act) / 1-attack / 2-heal / 3-spare
    public int tpCost { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string shortDescription { get; private set; }

    public Magic(int id, int type, int tpCost, string name, string description, string shortDescription)
    {
        this.id = id;
        this.type = type;
        this.tpCost = tpCost;
        this.name = name;
        this.description = description;
        this.shortDescription = shortDescription + " " + tpCost + "% TP";
    }

    public void Use(int target, int userId) // cast
    {
        Debug.Log(name);
        switch (id)
        {
            default: // 0
                break;
        }
    }
}
