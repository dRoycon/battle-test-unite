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
            party.activePartyMembers[0].hp = Mathf.Clamp(party.activePartyMembers[0].hp - damage, 0, party.activePartyMembers[0].maxHp);
            if (party.activePartyMembers[0].hp > 0)
            {
                SetInvisFrames();
            }
            else
            {
                Destroy(gameObject);
            }
            Debug.Log(party.activePartyMembers[0].nickname + ": " + party.activePartyMembers[0].hp+"/"+ party.activePartyMembers[0].maxHp);
        }
    }

    /// <summary>
    /// this methos enables or disables invis frames
    /// </summary>
    private void SetInvisFrames()
    {
        if (!invisFrames)
        {
            //timeWhenHit = (float)Time.realtimeSinceStartupAsDouble;
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
