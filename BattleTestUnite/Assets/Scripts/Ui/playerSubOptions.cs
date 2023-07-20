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
    private PlayerTp tp;
    private int currentMemberTurn;
    private static int preSpentTp;
    private static int oldPos;
    private static bool cancelled = false;
    private void OnEnable()
    {
        if (cancelled)
        {
            pos = oldPos;
            cancelled = false;
        }
        else pos = 1;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        childAmt = 0;
        isDone = false;
        res = -100;
        currentMemberTurn = Consts.playerParty.currentMemberTurn;
    }
    private void Start()
    {

        RenderPos();
        tp = player.GetComponent<PlayerTp>();
        if (type == 3 || type == 4 || type == 5)
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
            if (pos > pageLimit)
            {
                if (arrow != null)
                    if (arrow.TryGetComponent<arrowHover>(out arrowHover hover)) hover.Flip();
                VisiblePage(pageLimit, children.Length);
            }
        }
    }

    private void Update()
    {
        if (!isDone)
        {
            int prevPos = pos;
            if (type == 1) pos = select.options(childAmt, pos, false, "up", "down");
            else if (type == 3 || type == 4 || type == 5) pos = select.options(childAmt, pos, true, "left", "right");
            if (pos != prevPos) // shmooving
            {
                if (pos > 0)
                {
                    RenderPos();
                    if (type == 3 || type == 4 || type == 5) SetDescription(pos);
                    if ((type == 3 || type == 4 || type == 5) && ((pos > pageLimit && prevPos <= pageLimit) || (pos <= pageLimit && prevPos > pageLimit))) // next page
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
                        if (arrow != null)
                            if (arrow.TryGetComponent<arrowHover>(out arrowHover hover)) hover.Flip();
                        VisiblePage(start, end);
                    }
                }
                else if (type == 4 && pos != -100) // check if has enough tp for spell/act
                {
                    int cost = ((MagicUser)(Consts.playerParty.activePartyMembers[currentMemberTurn - 1])).spells[-pos - 1].tpCost;
                    if (cost > tp.TpPercent()) // doesnt
                        pos = prevPos;
                    else // does
                    {
                        Debug.Log("tp was " + tp.TpPercent());
                        preSpentTp = tp.tp;
                        oldPos = -pos;   // remember old pos for cancelling
                        tp.SetTp((int)(tp.tp-(((float)cost/100f)*PlayerTp.MAX_TP)));
                        Debug.Log("tp is now " + tp.TpPercent());
                        CancelOrConfirm();
                    }
                }
                else // confirm/back
                {
                    HudText hud = transform.parent.GetComponent<HudText>();
                    Debug.Log(preSpentTp + "@");
                    if (hud.subSubOpt && hud.oldType == 4 && pos == -100 && preSpentTp != -1) // cancelling spell
                    {
                        tp.SetTp(preSpentTp);
                        cancelled = true;
                    }
                    else if (hud.subSubOpt && hud.oldType == 4 && preSpentTp != -1) // remember tp shit 
                        switch (currentMemberTurn - 1)
                        {
                            default: // 0
                                PlayerTurnOptions.tpTurn1 = preSpentTp;
                                break;
                            case 1:
                                PlayerTurnOptions.tpTurn2 = preSpentTp;
                                break;
                        }
                    else if (hud.subSubOpt && hud.oldType == 3 && pos == -100)
                    {
                        Debug.Log(oldPos + "&&*");
                        cancelled = true;
                    }
                    else if (!hud.subSubOpt && type == 3 && pos != -100) oldPos = -pos; // remember old pos for cancelling
                    preSpentTp = -1;
                    CancelOrConfirm();
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

        else if (type == 3 || type == 4 || type == 5)
        {
            player.transform.position = new Vector2(-22.07f + (((pos + 1) % 2) * 16.88925f), children[pos - 1].transform.position.y - 0.15f);
        }
    }

    public void UpdateChildren()
    {
        if (type == 1) children = new GameObject[Party.PartyAmount];
        else if (type == 3) children = new GameObject[PlayerParty.inventory.Count()];
        else if (type == 4) children = 
                new GameObject[((MagicUser)(Consts.playerParty.activePartyMembers[Consts.playerParty.currentMemberTurn-1])).count];
        else if (type == 5) { }
        childAmt = 0;
        //Debug.Log(transform.childCount);
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject gb = transform.GetChild(i).gameObject;
                if (type == 1)
                    if (gb.GetComponent<PartyMemText>()==null) break;
                else if (type == 3 || type == 4 || type == 5)
                        if (gb.GetComponent<ActTitle>() == null) break;
                children[i] = gb;
                childAmt++;
            }
        }
    }

    private void CancelOrConfirm()
    {
        res = pos;
        player.transform.position = new Vector2(0, 19);
        isDone = true;
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
        switch (type)
        {
            default: // 3 - item
                description.GetComponent<Description>().SetText(PlayerParty.inventory.items[x - 1].shortDescription);
                break;
            case 4: // magic
                description.GetComponent<Description>().SetText
                    (((MagicUser)(Consts.playerParty.activePartyMembers[Consts.playerParty.currentMemberTurn-1])).spells[x-1].shortDescription);
                break;
            case 5: // act
                break;
        }
    }
}
