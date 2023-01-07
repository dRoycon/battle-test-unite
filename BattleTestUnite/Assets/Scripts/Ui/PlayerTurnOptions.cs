using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnOptions : MonoBehaviour
{
    #region variables

    public bool canMove;
    private bool playerTurnActivated;
    private bool playerTurn;
    private bool playerTurnDeactivated;
    CharacterUi charUi;

    private bool hasMagic;
    [SerializeField]
    private Sprite magic;
    Image[] options;
    [SerializeField]
    private Image fight;
    [SerializeField]
    private Image ability;
    [SerializeField]
    private Image item;
    [SerializeField]
    private Image spare;
    [SerializeField]
    private Image defend;

    private int opt;
    private int prevOpt;
    private const int amt = 5;
    private int spot;

    #endregion
    void Start()
    {
        spot = GetComponentInParent<CharacterUi>().spot;
        charUi = GetComponentInParent<CharacterUi>();
        hasMagic = charUi.party.activePartyMembers[spot].hasMagic;
        opt = 1;
        options = new Image[amt]
        {
            fight,ability,item,spare,defend
        };
        if (hasMagic) ability.sprite = magic;
        playerTurn = charUi.canMove;
    }

    void Update()
    {
        if (playerTurnDeactivated)
        {
            if (playerTurn == true && charUi.party.currentMemberTurn - 1 == spot)
            {
                playerTurn = false;
                canMove = false;
                options[opt - 1].color = Consts.StaticOrange;
                opt = 1;
                playerTurnDeactivated = false;
                playerTurnActivated = false;
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
            }
        }

        if (canMove && charUi.party.currentMemberTurn-1 == spot)
        {
            prevOpt = opt;
            opt = select.options(amt, opt);
            if (opt >= 0)
            {
                options[prevOpt - 1].color = Consts.StaticOrange;
                options[opt - 1].color = Consts.NoelleYellow;
            }
            else
            {
                switch (opt)
                {
                    default: // fight
                        Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Fight");
                        break;
                    case -2: // act/magic
                        if (charUi.party.activePartyMembers[spot].hasMagic) Debug.Log(charUi.party.activePartyMembers[spot].nickname + ": Magic");
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
                        break;
                }
                options[(opt*-1) - 1].color = Consts.StaticOrange;
                opt = 1;
                if (charUi.party.CountActiveMembers()-1 > spot)
                {
                    charUi.party.currentMemberTurn = spot + 2;
                }
                else charUi.party.PartyTurn(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (charUi.canMove) playerTurnActivated = true;
        else playerTurnDeactivated = true;
    }
}
