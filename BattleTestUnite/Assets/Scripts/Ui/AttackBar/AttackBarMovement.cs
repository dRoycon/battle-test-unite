using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarMovement : MonoBehaviour
{
    public bool isFinished;
    private Transform tr;
    [SerializeField] private float space = 0.01f;
    [SerializeField] private float speed = 1;
    [SerializeField] private int cursorRange;
    bool start; // Start Shmoovin'
    public bool isTurn;
    bool canPress;
    int timer; // 3 2 1 Go!
    bool timer1Done;
    bool animationDone;
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
                    Debug.Log(CaculateDamage());
                }
            }
        }
    }

    private void FixedUpdate()
    {
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

        if (start)
        {
            tr.localPosition += new Vector3(-space, 0, 0) * speed;
            if (isFinished)
            {
                CursorAttackAnimation();
            }
        }
    }

    private int CaculateDamage()
    {
        float d = GetComponentInParent<AttackBarDistance>().distance;
        if (d == -1) damage = 0;
        else
        {
            damage = (int)(70 * ((8 * d * d) / ((-20.4f * d) - 2) - (d * d) + 2.1f));
            if (d < 0.01f && d >= 0) damage = (int)(damage*1.1f); 
        }
        if (damage == 0) Debug.Log("Miss!"); // debug
        return damage;
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
}