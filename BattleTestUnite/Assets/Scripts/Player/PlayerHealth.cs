using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int dmgCooldown;
    private bool invisFrames;
    public bool canGetHit;
    private SpriteRenderer sr;
    private int timer;
    public int targeted { get; private set; }
    public Party party { get; private set; }

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        canGetHit = true;
        party = GameObject.Find("Party").GetComponent<Party>();
    }

    void Update()
    {
        if (invisFrames)
        {
            if (timer >= dmgCooldown)
            {
                SetInvisFrames();
            }
        }
    }

    private void FixedUpdate()
    {
        if (invisFrames) timer++;
    }

    /// <summary>
    /// This method removes a set amount of health from the player.
    /// It should be called when the player has come in contact with a GameObject with the "Bullet" tag.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (!invisFrames && canGetHit)
        {
            // Select target
            targeted = Random.Range(0, party.CountActiveMembers());
            if (party.activePartyMembers[targeted].hp <= 0 && !party.IsPartyDown())
            {
                while (party.activePartyMembers[targeted].hp <= 0)
                {
                    targeted = Random.Range(0, party.CountActiveMembers());
                }
            }

            // damage target and confirm if the party is down
            party.activePartyMembers[targeted].hp = Mathf.Clamp(party.activePartyMembers[targeted].hp - damage, -999, party.activePartyMembers[targeted].maxHp);
            if (!party.IsPartyDown())
            {
                SetInvisFrames();
            }
            else
            {
                Destroy(gameObject);
            }
            Debug.Log(party.activePartyMembers[targeted].nickname + ": " + party.activePartyMembers[targeted].hp+"/"+ party.activePartyMembers[targeted].maxHp);
        }
    }

    /// <summary>
    /// this methos enables or disables invis frames
    /// </summary>
    public void SetInvisFrames()
    {
        if (!invisFrames)
        {
            invisFrames = true;
            sr.color = Color.gray;
            timer = 0;
        }
        else
        {
            invisFrames = false;
            sr.color = Color.white;
        }
    }

    public bool InvisFrames() { return invisFrames; }
}
