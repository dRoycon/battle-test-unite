using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarMovement : MonoBehaviour
{
    public bool isFinished;
    private Transform tr;
    [SerializeField] private float space = 0.01f;
    [SerializeField] private float speed = 1;
    bool start; // Start Shmoovin'
    bool canPress;
    int timer; // 3 2 1 Go!
    bool timer1Done;
    private SpriteRenderer sr;
    [SerializeField] private float fadeAmtPerTick;
    [SerializeField] private int tickAmt;
    [SerializeField] private float scaleAmtPerTick;

    int damage; // temp, should be in party member
    private void Start()
    {
        canPress = false;
        timer = 0;
        isFinished = false;
        tr = GetComponent<Transform>();
        timer1Done = false;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (start)
        {
            if (canPress)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    speed = 0;
                    isFinished = true;
                    canPress = false;
                    GetComponentInParent<AttackBarDistance>().attacked = true;
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
                Debug.Log(CaculateDamage());
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
        if (timer % tickAmt == 0)
        {
            sr.color -= new Color(0, 0, 0, fadeAmtPerTick);
            tr.localScale += new Vector3(scaleAmtPerTick, scaleAmtPerTick,0);
            if (sr.color.a <= 0)
            {
                // put shit here
                Destroy(gameObject);
            }
            timer = 0;
        }
        timer++;
    }
}
