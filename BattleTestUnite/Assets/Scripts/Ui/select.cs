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
            if (pos > min) res = -1;
            else res = amt-1;
        }
        else if (Input.GetKeyDown(Consts.keys["right"]))
        {
            if (pos < amt) res = 1;
            else res = -amt+1;
        }
        return res;
    }
}
