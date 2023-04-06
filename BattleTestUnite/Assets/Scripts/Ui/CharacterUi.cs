using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUi : MonoBehaviour
{
    [SerializeField] public int spot;
    [SerializeField] private TextMeshProUGUI nickname;
    [HideInInspector] public PlayerParty party;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool regularTurn; // the choosing, not the attacking bit after choosing

    private void OnEnable()
    {
        party = FindObjectOfType<PlayerParty>();
        canMove = true;
    }
    private void Start()
    {
        nickname.text = party.activePartyMembers[spot].nickname.ToUpper() + "";
        regularTurn = true;
    }

    private void FixedUpdate()
    {
        if (regularTurn)
        {
            if (party.isTurn) canMove = true;
            else canMove = false;
        }
    }
}
