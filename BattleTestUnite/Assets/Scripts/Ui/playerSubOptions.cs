using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private int pageLimit = 6;
    private GameObject arrow;
    private GameObject description;
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
        if (type == 3)
        {
            VisiblePage(0, pageLimit - 1);
            if (childAmt > pageLimit)
            {
                arrow = Instantiate((GameObject)Resources.Load("Prefabs/arrow", typeof(GameObject)), new Vector2(0,0), Quaternion.identity);
                arrow.transform.parent = transform.GetChild(0);
            }
            description = Instantiate((GameObject)Resources.Load("Prefabs/BattleText", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
            description.transform.SetParent(transform.GetChild(0), false);
            description.AddComponent<Description>();
            SetDescription(1);
        }
    }

    private void Update()
    {
        if (!isDone)
        {
            int prevPos = pos;
            if (type == 1) pos = select.options(childAmt, pos, false, "up", "down");
            else if (type == 3) pos = select.options(childAmt, pos, true, "left", "right");
            if (pos != prevPos)
            {
                if (pos > 0)
                {
                    RenderPos();
                    if (type == 3) SetDescription(pos);
                    if (type == 3 && ((pos > pageLimit && prevPos <= pageLimit) || (pos <= pageLimit && prevPos > pageLimit))) // next page
                    {
                        int start, end;
                        if (pos > pageLimit && prevPos <= pageLimit) // next
                        {
                            start = pageLimit;
                            end = children.Length;
                        }
                        else // prev
                        {
                            start = 0;
                            end = pageLimit - 1;
                        }
                        if (arrow!=null)
                            if (arrow.TryGetComponent<arrowHover>(out arrowHover hover)) hover.Flip();
                        VisiblePage(start, end);
                    }
                }
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
        if (type == 1)
        {
            float ob = children[pos - 1].transform.Find("BattleText").transform.position.y;
            player.transform.position = new Vector2(-18.527f, ob);
        }

        else if (type == 3)
        {
            player.transform.position = new Vector2(-22.07f + (((pos + 1) % 2) * 16.88925f), children[pos - 1].transform.position.y - 0.15f);
        }
    }

    public void UpdateChildren()
    {
        if (type == 1) children = new GameObject[Party.PartyAmount];
        else if (type == 3) children = new GameObject[PlayerParty.inventory.Count()];
        childAmt = 0;
        //Debug.Log(transform.childCount);
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

    private void VisiblePage(int start, int end)
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (i >= start && i <= end) children[i].GetComponent<TextMeshProUGUI>().enabled = true;
            else children[i].GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    private void SetDescription(int x)
    {
        description.GetComponent<Description>().SetText(PlayerParty.inventory.items[x - 1].shortDescription);
    }
}
