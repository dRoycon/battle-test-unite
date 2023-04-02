using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnOptions : MonoBehaviour
{
    #region variables

    private static int timer = 0;
    private static int tpTurn1 = -1;
    private static int tpTurn2 = -1;
    private static bool fightTurn1 = false;
    private static bool fightTurn2 = false;
    private static bool fightTurn3 = false; 
    public bool canMove;
    private bool playerTurnActivated;
    private bool playerTurn;
    private bool playerTurnDeactivated;
    CharacterUi charUi;
    GameObject player;
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

    [SerializeField] private GameObject fightBar;


    private int opt;
    private int prevOpt;
    private const int amt = 5;
    private int spot;

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
                prevOpt = opt;
                opt = select.options(amt, opt);
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
                            switch (spot)
                            {
                                default: // 0
                                    fightTurn1 = true;
                                    break;
                                case 1:
                                    fightTurn2 = true;
                                    break;
                                case 2:
                                    fightTurn3 = true;
                                    break;
                            }
                            break;
                        case -2: // act/magic
                            if (((PlayerPartyMember)charUi.party.activePartyMembers[spot]).hasMagic) Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Magic");
                            else Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Act");
                            break;
                        case -3: // item
                            Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Item");
                            break;
                        case -4: // spare
                            Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Spare");
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
                    EndTurn(opt * -1);
                }
                else if (spot > 0) // If You Go Back
                {
                    switch (spot)
                    {
                        default: // spot 1
                            fightTurn1 = false;

                            if (tpTurn1 != -1) tp.SetTp(tpTurn1);
                            tpTurn1 = -1;
                            break;
                        case 2:
                            fightTurn2 = false;

                            if (tpTurn2 != -1) tp.SetTp(tpTurn2);
                            tpTurn2 = -1;
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
        }
        else timer++; 
    }

    private void FixedUpdate()
    {
        if (charUi.canMove) playerTurnActivated = true;
        else playerTurnDeactivated = true;
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
        else // done! now the enemy's turn!
        {
            int fightCnt = 0;
            if (fightTurn1)
            {
                Instantiate(fightBar, new Vector2(0, 0), Quaternion.identity);
                fightCnt++;
            }
            if(fightTurn2)
            {
                Instantiate(fightBar, new Vector2(0, 0 - (fightCnt*3)), Quaternion.identity);
                fightCnt++;
            }
            if (fightTurn3)
            {
                Instantiate(fightBar, new Vector2(0, 0 - (fightCnt * 3)), Quaternion.identity);
            }

            charUi.party.PartyTurn(false);
            tpTurn1 = -1;
            tpTurn2 = -1;
        }
    }

    private void PlayerTurnDeactivated()
    {
        playerTurn = false;
        canMove = false;
        options[opt - 1].color = Consts.StaticOrange;
        opt = 1;
        playerTurnDeactivated = false;
        playerTurnActivated = false;
    }
}
