using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select : MonoBehaviour
{
    private const int min = 1;
    public static int options(int amt, int pos)
    {
        int res = 0;
        if (Input.GetKeyDown(Consts.keys["left"]))
        {
            if (pos > min) res = pos-1;
            else res = amt;
        }
        else if (Input.GetKeyDown(Consts.keys["right"]))
        {
            if (pos < amt) res = pos+1;
            else res = 1;
        }
        else if (Input.GetKeyDown(Consts.keys["confirm"]))
        {
            res = -pos;
        }
        if (res == 0) return pos;
        return res;
    }
}
