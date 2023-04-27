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
    public int type;
    [HideInInspector] public bool isDone;
    [HideInInspector] public int res;
    private void OnEnable()
    {
        pos = 1;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
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
            if (type  == 1) pos = select.options(childAmt, pos, false, "up", "down");
            else if (type == 3) pos = select.options(childAmt, pos, true, "left", "right");
            Debug.Log(pos);
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
        if (type == 1) player.transform.position = new Vector2(-18.527f, children[pos - 1].transform.GetChild(2).transform.position.y - 0.15f);
        else if (type == 3)
        {
            player.transform.position = new Vector2(-22.07f + (((pos+1)%2)*16.88925f), children[pos - 1].transform.position.y - 0.15f);
        }
    }

    public void UpdateChildren()
    {
        if (type == 1) children = new GameObject[Party.PartyAmount];
        else if (type == 3) children = new GameObject[PlayerParty.inventory.Count()];
        childAmt = 0;
        Debug.Log(transform.childCount);
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject gb = transform.GetChild(i).gameObject;
                if (type == 1)
                    if (gb.GetComponent<PartyMemText>()==null) break;
                else if (type == 3)
                        if (gb.GetComponent<ActTitle>() == null) break;
                children[i] = gb;
                childAmt++;
            }
        }
    }
}
