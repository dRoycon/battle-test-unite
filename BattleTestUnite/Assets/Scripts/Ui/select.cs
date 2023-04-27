using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select
{
    private const int min = 1;
    public static int options(int amt, int pos, bool is2D, string back, string forth)
    {
        int res = 0;
        if (Input.GetKeyDown(Consts.keys[back])) // left or up
        {
            if (pos > min) res = pos-1;
            else res = amt;
        }
        else if (Input.GetKeyDown(Consts.keys[forth])) // right or down
        {
            if (pos < amt) res = pos+1;
            else res = 1;
        }
        else if (Input.GetKeyDown(Consts.keys["confirm"]))
        {
            res = -pos;
        }
        else if (Input.GetKeyDown(Consts.keys["cancel"]))
        {
            res = -100;
        }
        else if (is2D && Input.GetKeyDown(Consts.keys["up"]))
        {
            if (pos - 1 > min) res = pos - 2;
            else if (pos % 2 == 0) res = amt;
            else res = amt - 1;
        }
        else if (is2D && Input.GetKeyDown(Consts.keys["down"]))
        {
            if (pos + 1 < amt) res = pos + 2;
            else if (pos % 2 == 0 && amt>=2) res = 2;
            else res = 1;
        }
        if (res == 0) return pos;
        return res;
    }
}
