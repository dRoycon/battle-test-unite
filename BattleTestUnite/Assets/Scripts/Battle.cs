using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] public PlayerParty party;
    [SerializeField] public EnemyParty enemyP;

    private void OnEnable()
    {
        Enemy dummy = new Enemy(1, "dummy", 800, 5, 5, "silly fella");
        dummy.spareMeter = 100;
        dummy.isTired = true;
        enemyP.AddMember(dummy);
        Enemy dummy2 = new Enemy(2, "dummy2", 500, 2, 7, "the other silly fella");
        dummy2.isTired = true;
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