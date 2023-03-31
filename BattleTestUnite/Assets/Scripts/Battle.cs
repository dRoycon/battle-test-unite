using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PlayerParty party;
    private void Start()
    {
        party.PartyTurn(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (party.isTurn) party.PartyTurn(false);
            else party.PartyTurn(true);
        }
    }
}
