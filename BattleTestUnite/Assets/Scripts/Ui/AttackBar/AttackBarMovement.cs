using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarMovement : MonoBehaviour
{
    [HideInInspector] public bool isFinished;
    private Transform tr;
    [SerializeField] private float space = 0.01f;
    [SerializeField] private float speed = 1;
    [SerializeField] private int cursorRange;
    bool start; // Start Shmoovin'
    [HideInInspector] public bool isTurn;
    [HideInInspector] public bool canPress;
    int timer; // 3 2 1 Go!
    bool timer1Done;
    bool animationDone;
    bool perfectAttack;
    private SpriteRenderer sr;
    [SerializeField] private float fadeAmtPerTick;
    [SerializeField] private int tickAmt;
    [SerializeField] private float scaleAmtPerTick;

    int damage; // temp, should be in party member
    private void OnEnable()
    {
        transform.localPosition += new Vector3(Random.Range(0, cursorRange) * space * speed, 0, 0);
        isTurn = true;
    }
    private void Start()
    {
        canPress = false;
        timer = 0;
        isFinished = false;
        perfectAttack = false;
        tr = GetComponent<Transform>();
        timer1Done = false;
        animationDone = false;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (start)
        {
            if (canPress && isTurn)
            {
                if (Input.GetKeyDown(Consts.keys["confirm"]))
                {
                    AttackBarDistance ak = GetComponentInParent<AttackBarDistance>();
                    ak.cursorOut = true;
                    speed = 0;
                    isFinished = true;
                    canPress = false;
                    ak.attacked = true;
                    CaculateDamage();
                    if (!perfectAttack) sr.color = transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color;
                    else sr.color = Consts.NoelleYellow;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (start)
        {
            tr.localPosition += new Vector3(-space, 0, 0) * speed;
            if (isFinished)
            {
                CursorAttackAnimation();
            }
        }

        if (!timer1Done)
        {
            timer++;
            if (timer == 25) start = true;
            if (timer == 40)
            {
                canPress = true;
                timer1Done = true;
                timer = 0;
            }
        }
    }

    private void CaculateDamage()
    {
        float d = GetComponentInParent<AttackBarDistance>().CaculateDistance();
        if (d == -1) damage = 0;
        else
        {
            int amp = 70;
            if (d < 0.01f && d >= 0)
            {
                perfectAttack = true;
                d = 0;
                amp = (int)(amp * 1.1f);
            }
            damage = (int)(amp * ((8 * d * d) / ((-20.4f * d) - 2) - (d * d) + 2.1f));
        }
        if (damage == 0) Debug.Log("Miss!"); // debug
        else
        {
            EnemyParty enemyP = GetComponentInParent<AttackBarDistance>().enemyParty;
            int target = GetComponentInParent<AttackBarDistance>().enemyTarget;
            Debug.Log("Target:" + target);
            if (enemyP.activePartyMembers[target] != null && enemyP.activePartyMembers[target].hp > 0)
            {
                enemyP.activePartyMembers[target].hp -= damage;
                DidDefeat(enemyP, target);
            }
            else
            {
                target = enemyP.NextInLineAttack();
                if (target != -1)
                {
                    enemyP.activePartyMembers[target].hp -= damage;
                    DidDefeat(enemyP, target);
                }
            }
        }
    }

    private void CursorAttackAnimation()
    {
        if (!animationDone)
        {
            if (timer % tickAmt == 0)
            {
                sr.color -= new Color(0, 0, 0, fadeAmtPerTick);
                tr.localScale += new Vector3(scaleAmtPerTick, scaleAmtPerTick, 0);
                if (sr.color.a <= 0)
                {
                    animationDone = true;
                    AttackBarDistance ak = GetComponentInParent<AttackBarDistance>();
                    ak.animationDone = true;
                }
                timer = 0;
            }
            timer++;
        }
    }

    private void DidDefeat(EnemyParty enemyP, int target)
    {
        Debug.Log(enemyP.activePartyMembers[target].nickname + ": " + enemyP.activePartyMembers[target].hp + "/" + enemyP.activePartyMembers[target].maxHp);
        if (enemyP.activePartyMembers[target] != null && enemyP.activePartyMembers[target].hp <= 0)
        {
            enemyP.RemoveMember(enemyP.activePartyMembers[target].id);
        }
    }
}
