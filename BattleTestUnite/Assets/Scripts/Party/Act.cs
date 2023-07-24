using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act : IUseAct
{
    public int id { get; private set; }
    public int ally1 { get; private set; }
    public int ally2 { get; private set; }
    public int tpCost { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public bool isComponent { get; private set; } // some acts (mostly R-Act[ect..]) will show all at once when combined for example:
                                                  // * You helped X tidy up!
                                                  // * Ralsei baked X a cake!
                                                  // * Susie ignored X

    public Act(int id, int ally1, int ally2, int tpCost, string name, string description, bool isComponent)
    {
        this.id = id;
        this.ally1 = ally1;
        this.ally2 = ally2;
        this.tpCost = tpCost;
        this.name = name;
        if (tpCost > 0) description = description + "<br><color=#FF7F27>" + tpCost + "% TP";
        this.description = description;
        this.isComponent = isComponent;
    }

    public void Use(int target, int userId)
    {
        Debug.Log(name + " " + id);
        EnemyParty enemyP = Transform.FindObjectOfType<Battle>().enemyP;
        switch (id)
        {
            default: // check
                break;
            case 1: // something
                break;
        }
    }
}
