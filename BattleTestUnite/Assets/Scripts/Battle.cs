using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PlayerParty party;
    [SerializeField] private EnemyParty enemyP;

    private void OnEnable()
    {
        Enemy dummy = new Enemy(1, "dummy", 700, 5, 5, "silly fella");
        enemyP.AddMember(dummy);
        Enemy dummy2 = new Enemy(2, "dummy2", 1000, 2, 7, "the other one");
        enemyP.AddMember(dummy2);
    }

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