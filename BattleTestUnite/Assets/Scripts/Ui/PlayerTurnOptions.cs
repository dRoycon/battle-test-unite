using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnOptions : MonoBehaviour
{
    #region variables

    private static int timer = 0;
    public static int tpTurn1 = -1;
    public static int tpTurn2 = -1;
    private static int optTurn1 = -1;
    private static int optTurn2 = -1;
    private static int optTurn3 = -1;
    private static int target1 = -1;
    private static int target2 = -1;
    private static int target3 = -1;
    private static int itemSpt1 = -1;
    private static int itemId1 = -1;
    private static int itemSpt2 = -1;
    private static int itemId2 = -1;
    private static int itemId3 = -1;
    private static int actSpt1 = -1;
    private static int actSpt2 = -1;
    private static int actSpt3 = -1;
    private static bool readyForNextTurn = false;
    public bool canMove;
    private bool playerTurnActivated;
    private bool playerTurn;
    private bool playerTurnDeactivated;
    private bool subMenu;
    private int subOpt;
    private int subSubOpt;
    CharacterUi charUi;
    GameObject player;
    EnemyParty enemyP;
    TpBar tpBar;
    PlayerTp tp;

    #region buttons
    private bool hasMagic;
    [SerializeField]
    private Sprite magic;
    [SerializeField]
    private Sprite magicText;
    Image[] options;
    Image[] texts;
    [SerializeField]
    private Image fight;
    [SerializeField]
    private Image fightText;
    [SerializeField]
    private Image ability;
    [SerializeField]
    private Image abilityText;
    [SerializeField]
    private Image item;
    [SerializeField]
    private Image itemText;
    [SerializeField]
    private Image spare;
    [SerializeField]
    private Image spareText;
    [SerializeField]
    private Image defend;
    [SerializeField]
    private Image defendText;
    #endregion

    [SerializeField] private GameObject attackMaster;
    [SerializeField] private GameObject fightBar;
    [SerializeField] private float cursorMinDistance;

    HudText hudText;
    private int opt;
    private int prevOpt;
    private const int amt = 5;
    private int spot; // goes from 0 to 2

    #endregion
    void Start()
    {
        spot = GetComponentInParent<CharacterUi>().spot;
        charUi = GetComponentInParent<CharacterUi>();
        hasMagic = ((PlayerPartyMember)charUi.party.activePartyMembers[spot]).hasMagic;
        opt = 1;
        options = new Image[amt]
        {
            fight,ability,item,spare,defend
        };
        texts = new Image[amt]
        {
            fightText,abilityText,itemText,spareText,defendText
        };
        if (hasMagic)
        {
            ability.sprite = magic;
            abilityText.sprite = magicText;
        }
        playerTurn = charUi.canMove;
        player = GameObject.FindGameObjectWithTag("Player");
        tp = player.GetComponent<PlayerTp>();
        tpBar = GameObject.FindGameObjectWithTag("TpBar").GetComponent<TpBar>();
        canMove = true;
        hudText = transform.parent.transform.parent.GetComponent<characterMaster>().hudText;
        subMenu = false;
        enemyP = FindObjectOfType<EnemyParty>();
    }

    void Update()
    {
        if (timer == 4)
        {
            if (playerTurnDeactivated) 
            {
                if (playerTurn == true && charUi.party.currentMemberTurn - 1 == spot) 
                {
                    PlayerTurnDeactivated();
                }
            }
            if (playerTurnActivated)
            {
                if (playerTurn == false && charUi.party.currentMemberTurn - 1 == spot)
                {
                    playerTurn = true;
                    options[opt - 1].color = Consts.NoelleYellow;
                    canMove = true;
                    playerTurnActivated = false;
                    playerTurnDeactivated = false;
                    if (charUi.party.activePartyMembers[spot].hp <= 0)
                    {
                        PlayerTurnDeactivated();
                        EndTurn(1);
                    }
                }
            }

            if (canMove && charUi.party.currentMemberTurn - 1 == spot) // When It's that members turn
            {
                if (!subMenu)
                {
                    prevOpt = opt;
                    opt = select.options(amt, opt, false, "left", "right");
                    if (opt >= 0) // shmoomin' in the menu
                    {
                        options[prevOpt - 1].color = Consts.StaticOrange;
                        options[opt - 1].color = Consts.NoelleYellow;
                        texts[prevOpt - 1].enabled = false;
                        texts[opt - 1].enabled = true;
                    }
                    else if (opt > -amt - 1) // selecting an option
                    {
                        timer = 0;
                        switch (opt)
                        {
                            default: // fight
                                Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Fight");
                                hudText.GetComponent<HudText>().changeType(2);
                                subMenu = true;
                                break;
                            case -2: // act/magic
                                if (((PlayerPartyMember)charUi.party.activePartyMembers[spot]).hasMagic)
                                { // magic
                                    Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Magic");
                                    hudText.GetComponent<HudText>().changeType(4);
                                    subMenu = true;
                                }
                                else
                                { // act
                                    Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Act");
                                    hudText.GetComponent<HudText>().changeType(6);
                                    subMenu = true;
                                }
                                break;
                            case -3: // item
                                Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Item");
                                hudText.GetComponent<HudText>().changeType(3);
                                subMenu = true;
                                break;
                            case -4: // spare
                                Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Spare");
                                hudText.GetComponent<HudText>().changeType(2);
                                subMenu = true;
                                break;
                            case -5: // defend
                                Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Defend");
                                switch (spot)
                                {
                                    default: // 0
                                        tpTurn1 = tp.tp;
                                        break;
                                    case 1:
                                        tpTurn2 = tp.tp;
                                        break;
                                }
                                tp.SetTp(PlayerTp.Defend(tp.tp));
                                tp.UpdtateTpPercent();
                                tpBar.CheckTp();
                                break;
                        }
                        if (!subMenu) EndTurn(opt * -1);
                    }
                    else if (spot > 0) // If You Go Back
                    {
                        switch (spot)
                        {
                            default: // spot 1
                                optTurn1 = -1;

                                if (tpTurn1 != -1) tp.SetTp(tpTurn1);
                                optTurn1 = -1;
                                Debug.Log(itemSpt1 + " + " + itemId1 + " : " + PlayerParty.inventory.ToString());
                                if (itemId1 != -1) PlayerParty.inventory.InsertItem(itemSpt1, itemId1);
                                itemId1 = -1;
                                itemSpt1 = -1;
                                actSpt1 = -1;
                                break;
                            case 2:
                                optTurn2 = -1;

                                if (tpTurn2 != -1) tp.SetTp(tpTurn2);
                                tpTurn2 = -1;
                                if (itemId2 != -1) PlayerParty.inventory.InsertItem(itemSpt2, itemId2);
                                itemId2 = -1;
                                itemSpt2 = -1;
                                actSpt2 = -1;
                                break;
                        }
                        timer = 0;
                        options[prevOpt - 1].color = Consts.StaticOrange;
                        texts[prevOpt - 1].enabled = false;
                        opt = 1;
                        charUi.party.currentMemberTurn = spot;
                        tp.UpdtateTpPercent();
                        tpBar.CheckTp();
                    }
                    else opt = prevOpt;
                }
                else
                {
                    if (hudText.isDone)
                    {
                        hudText.changeType(0);
                        hudText.isDone = false;
                        subOpt = hudText.subSelect;
                        if (opt == -3 || opt == -2) subSubOpt = hudText.subSubSelect;
                        subMenu = false;
                        if (subOpt != -100)
                        {
                            switch (opt)
                            {
                                default: // fight
                                    switch (spot)
                                    {
                                        default: // 0
                                            optTurn1 = 0;
                                            target1 = -subOpt-1;
                                            break;
                                        case 1:
                                            optTurn2 = 0;
                                            target2 = -subOpt-1;
                                            break;
                                        case 2:
                                            optTurn3 = 0;
                                            target3 = -subOpt-1;
                                            break;
                                    }
                                    break;
                                case -2: // act / magic
                                    switch (spot)
                                    {
                                        default: // 0
                                            optTurn1 = 1;
                                            actSpt1 = -subOpt - 1;
                                            target1 = -subSubOpt - 1;
                                            break;
                                        case 1:
                                            optTurn2 = 1;
                                            actSpt2 = -subOpt - 1;
                                            target2 = -subSubOpt - 1;
                                            break;
                                        case 2:
                                            optTurn3 = 1;
                                            actSpt3 = -subOpt - 1;
                                            target3 = -subSubOpt - 1;
                                            break;
                                    }
                                    tpBar.CheckTp();
                                    break;
                                case -3: // item
                                    switch (spot)
                                    {
                                        default: // 0
                                            optTurn1 = 2;
                                            itemSpt1 = -subOpt-1;
                                            itemId1 = PlayerParty.inventory.RemoveItem(itemSpt1);
                                            target1 = -subSubOpt-1;
                                            break;
                                        case 1:
                                            optTurn2 = 2;
                                            itemSpt2 = -subOpt-1;
                                            itemId2 = PlayerParty.inventory.RemoveItem(itemSpt2);
                                            target2 = -subSubOpt-1;
                                            break;
                                        case 2:
                                            optTurn3 = 2;
                                            itemId3 = PlayerParty.inventory.RemoveItem(-subOpt-1);
                                            target3 = -subSubOpt-1;
                                            break;

                                    }
                                    break;
                                case -4: // mercy
                                    switch (spot)
                                    {
                                        default: // 0
                                            optTurn1 = 3;
                                            target1 = -subOpt - 1;
                                            break;
                                        case 1:
                                            optTurn2 = 3;
                                            target2 = -subOpt - 1;
                                            break;
                                        case 2:
                                            optTurn3 = 3;
                                            target3 = -subOpt - 1;
                                            break;
                                    }
                                    break;
                            }
                            EndTurn(opt * -1);
                        }
                        else
                        {
                            opt = -opt;
                        }
                    }
                }
            }
        }
        else timer++; 
    }

    private void FixedUpdate()
    {
        if (charUi.canMove) playerTurnActivated = true;
        else
        {
            playerTurnDeactivated = true;
            if (readyForNextTurn && Consts.finishedAttackingTurn)
            {
                readyForNextTurn = false;
                Consts.finishedAttackingTurn = true;
                charUi.regularTurn = true;
                charUi.party.PartyTurn(false);
            }
        }
    }

    /// <summary>
    /// when a party member finishes their turn!
    /// </summary>
    /// <param name="_opt"></param>
    private void EndTurn(int _opt)
    {
        options[_opt - 1].color = Consts.StaticOrange;
        texts[_opt - 1].enabled = false;
        opt = 1;
        if (charUi.party.CountActiveMembers() - 1 > spot) // next member's turn
        {
            charUi.party.currentMemberTurn = spot + 2;
        }
        else // done! now the enemy's turn! but lets set stuff up first
        {
            int fightCnt = 0;
            GameObject f1 = null, f2 = null, f3 = null, am = null; // only used if u fight
            if (optTurn1==0 || optTurn2==0 || optTurn3==0) // if fighting
            {
                am = Instantiate(attackMaster, new Vector2(0, 0), Quaternion.identity);
                Consts.finishedAttackingTurn = false;
            }
            switch (optTurn1) // first party member
            {
                case 0: // fight
                    f1 = Instantiate(fightBar, new Vector2(0, 0), Quaternion.identity);
                    f1.transform.parent = am.transform;
                    f1.transform.GetChild(0).GetComponent<AttackBarMovement>().isTurn = false;
                    f1.GetComponent<SpriteRenderer>().color = ((PlayerPartyMember)charUi.party.activePartyMembers[0]).accentColor1;
                    f1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = ((PlayerPartyMember)charUi.party.activePartyMembers[0]).accentColor2;
                    f1.GetComponent<AttackBarDistance>().enemyTarget = target1;

                    fightCnt++;
                    break;
                case 1: // magic/act
                    if (((PlayerPartyMember)charUi.party.activePartyMembers[0]).hasMagic)
                    { // magic
                        ((MagicUser)(charUi.party.activePartyMembers[0])).CastSpell(actSpt1, target1);
                    }
                    else
                    { //act

                    }
                    break;
                case 2: // item
                    Consts.items[itemId1].Use(target1);
                    break;
                case 3: // spare
                    if (enemyP.activePartyMembers[target1]!=null && enemyP.activePartyMembers[target1].hp>0)
                    {
                        if (((Enemy)enemyP.activePartyMembers[target1]).spareMeter>=Enemy.spareMeterMax)
                        {
                            Debug.Log(enemyP.activePartyMembers[target1].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target1].id);
                        }
                        else Debug.Log(enemyP.activePartyMembers[target1].nickname + " can't be spared");
                    }
                    else
                    {
                        target1 = enemyP.NextInLineSpare();
                        if (target1 != -1)
                        {
                            Debug.Log(enemyP.activePartyMembers[target1].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target1].id);
                        }
                    }
                    break;
            }

            switch (optTurn2) // second party member
            {
                case 0: // fight
                    f2 = Instantiate(fightBar, new Vector2(0, 0 - (fightCnt * 3)), Quaternion.identity);
                    f2.transform.parent = am.transform;
                    f2.transform.GetChild(0).GetComponent<AttackBarMovement>().isTurn = false;
                    f2.GetComponent<SpriteRenderer>().color = ((PlayerPartyMember)charUi.party.activePartyMembers[1]).accentColor1;
                    f2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = ((PlayerPartyMember)charUi.party.activePartyMembers[1]).accentColor2;
                    f2.GetComponent<AttackBarDistance>().enemyTarget = target2;

                    fightCnt++;
                    if (optTurn1 == 0) // make sure cursors arent too close
                    {
                        if (Mathf.Abs(f1.transform.GetChild(0).transform.localPosition.x - f2.transform.GetChild(0).transform.localPosition.x) < cursorMinDistance)
                            f2.transform.GetChild(0).transform.localPosition = new Vector3(f1.transform.GetChild(0).transform.localPosition.x, f2.transform.GetChild(0).transform.localPosition.y);
                    }
                    break;
                case 1: // magic / act
                    if (((PlayerPartyMember)charUi.party.activePartyMembers[1]).hasMagic)
                    { // magic
                        ((MagicUser)(charUi.party.activePartyMembers[1])).CastSpell(actSpt2, target2);
                    }
                    else
                    { //act

                    }
                    break;
                case 2: // item
                    Consts.items[itemId2].Use(target2);
                    break;
                case 3: // spare
                    if (enemyP.activePartyMembers[target2] != null && enemyP.activePartyMembers[target2].hp > 0)
                    {
                        if (((Enemy)enemyP.activePartyMembers[target2]).spareMeter >= Enemy.spareMeterMax)
                        {
                            Debug.Log(enemyP.activePartyMembers[target2].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target2].id);
                        }
                        else Debug.Log(enemyP.activePartyMembers[target2].nickname + " can't be spared");
                    }
                    else
                    {
                        target2 = enemyP.NextInLineSpare();
                        if (target2 != -1)
                        {
                            Debug.Log(enemyP.activePartyMembers[target2].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target2].id);
                        }
                    }
                    break;
            }

            switch (optTurn3) // third party member
            {
                case 0: // fight
                    f3 = Instantiate(fightBar, new Vector2(0, 0 - (fightCnt * 3)), Quaternion.identity);
                    f3.transform.parent = am.transform;
                    f3.transform.GetChild(0).GetComponent<AttackBarMovement>().isTurn = false;
                    f3.GetComponent<SpriteRenderer>().color = ((PlayerPartyMember)charUi.party.activePartyMembers[2]).accentColor1;
                    f3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = ((PlayerPartyMember)charUi.party.activePartyMembers[2]).accentColor2;
                    f3.GetComponent<AttackBarDistance>().enemyTarget = target3;

                    if (optTurn1 == 0) // make sure cursors arent too close
                    {
                        if (Mathf.Abs(f1.transform.GetChild(0).transform.localPosition.x - f3.transform.GetChild(0).transform.localPosition.x) < cursorMinDistance)
                            f3.transform.GetChild(0).transform.localPosition = new Vector3(f1.transform.GetChild(0).transform.localPosition.x, f3.transform.GetChild(0).transform.localPosition.y);
                    }
                    if (optTurn2 == 0) // make sure cursors arent too close
                    {
                        if (Mathf.Abs(f2.transform.GetChild(0).transform.localPosition.x - f3.transform.GetChild(0).transform.localPosition.x) < cursorMinDistance)
                            f3.transform.GetChild(0).transform.localPosition = new Vector3(f2.transform.GetChild(0).transform.localPosition.x, f3.transform.GetChild(0).transform.localPosition.y);
                    }
                    fightCnt++;
                    break;
                case 1: // magic / act
                    if (((PlayerPartyMember)charUi.party.activePartyMembers[2]).hasMagic)
                    { // magic
                        ((MagicUser)(charUi.party.activePartyMembers[2])).CastSpell(actSpt3, target3);
                    }
                    else
                    { //act

                    }
                    break;
                case 2: // item
                    Consts.items[itemId3].Use(target3);
                    break;
                case 3: // spare
                    if (enemyP.activePartyMembers[target3] != null && enemyP.activePartyMembers[target3].hp > 0)
                    {
                        if (((Enemy)enemyP.activePartyMembers[target3]).spareMeter >= Enemy.spareMeterMax)
                        {
                            Debug.Log(enemyP.activePartyMembers[target3].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target3].id);
                        }
                        else Debug.Log(enemyP.activePartyMembers[target3].nickname + " can't be spared");
                    }
                    else
                    {
                        target3 = enemyP.NextInLineSpare();
                        if (target3 != -1)
                        {
                            Debug.Log(enemyP.activePartyMembers[target3].nickname + " was spared");
                            enemyP.RemoveMember(enemyP.activePartyMembers[target3].id);
                        }
                    }
                    break;
            }

            if (optTurn1==0 || optTurn2==0 || optTurn3==0) am.GetComponent<AttackMaster>().childAmt = fightCnt;
            optTurn1 = -1;
            optTurn2 = -1;
            optTurn3 = -1;
            target1 = -1;
            target2 = -1;
            target3 = -1;

            // defend
            canMove = false;
            charUi.regularTurn = false;
            charUi.canMove = false;
            readyForNextTurn = true;
            tpTurn1 = -1;
            tpTurn2 = -1;
        }
    }

    private void PlayerTurnDeactivated()
    {
        canMove = false;
        options[opt - 1].color = Consts.StaticOrange;
        opt = 1;
        playerTurn = false;
        playerTurnDeactivated = false;
        playerTurnActivated = false;
    }
}
