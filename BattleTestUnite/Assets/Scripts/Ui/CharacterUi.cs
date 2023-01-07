using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUi : MonoBehaviour
{
    [SerializeField] public int spot;
    [SerializeField] private TextMeshProUGUI nickname;
    [SerializeField] public Party party;
    public bool canMove;

    private void Start()
    {
        nickname.text = party.activePartyMembers[spot].nickname.ToUpper() + "";
    }

    private void FixedUpdate()
    {
        if (party.isPlayerTurn) canMove = true;
        else canMove = false;
    }
}
