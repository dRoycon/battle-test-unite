using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSubOptions : MonoBehaviour
{
    [HideInInspector] public bool isEnemy;
    private int pos;
    private GameObject player;
    private GameObject[] children;
    private int childAmt;
    [HideInInspector] public bool isDone;
    [HideInInspector] public int res;
    private void OnEnable()
    {
        pos = 1;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        children = new GameObject[Party.PartyAmount];
        childAmt = 0;
        isDone = false;
        res = -100;
    }
    private void Start()
    {
        RenderPos();
    }

    private void Update()
    {
        if (!isDone)
        {
            int prevPos = pos;
            pos = select.options(childAmt, pos, false, "up", "down");
            if (pos != prevPos)
            {
                if (pos > 0) RenderPos();
                else
                {
                    res = pos;
                    player.transform.position = new Vector2(0, 19);
                    isDone = true;
                }
            }
        }
    }
    

    private void RenderPos()
    {
        Debug.Log(pos +" " +childAmt);
        player.transform.position = new Vector2(-18.527f, children[pos-1].transform.position.y-11.1502f);
    }

    public void UpdateChildren()
    {
        children = new GameObject[Party.PartyAmount];
        Debug.Log(transform.childCount);
        childAmt = 0;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject gb = transform.GetChild(i).gameObject;
                if (gb.GetComponent<PartyMemText>()==null) break;
                children[i] = gb;
                childAmt++;
            }
        }
    }
}
