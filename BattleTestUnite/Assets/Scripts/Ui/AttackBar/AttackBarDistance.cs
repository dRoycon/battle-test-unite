using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarDistance : MonoBehaviour
{
    bool inTrigger;
    public float distance { get; private set; }
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject cursor;
    private int timer;
    [HideInInspector] public bool deactivating;
    [HideInInspector] public bool attacked;
    [HideInInspector] public bool animationDone;
    [HideInInspector] public bool cursorOut; // cursor is out, missed or attacked
    [SerializeField] private float fadeAmtPerTick;
    [SerializeField] private GameObject afterEffect;
    [SerializeField] private int tickAmt;
    [SerializeField] private float disTillEffect;
    private float lastDashX;
    private SpriteRenderer cursorRender;

    void Start()
    {
        attacked = false;
        deactivating = false;
        timer = 0;
        inTrigger = false;
        distance = -1;
        cursorRender = cursor.GetComponent<SpriteRenderer>();
        cursorOut = false;
        animationDone = false;
        lastDashX = cursor.transform.localPosition.x;

    }
    private void Update()
    {
        if (!deactivating && Mathf.Abs(cursor.transform.localPosition.x - lastDashX) >= disTillEffect)
        {
            lastDashX = cursor.transform.localPosition.x;
            GameObject af = Instantiate(afterEffect, new Vector2(cursor.transform.position.x, cursor.transform.position.y), Quaternion.identity);
            af.transform.parent = transform;
        }
    }

    private void FixedUpdate()
    {
        if (!attacked)
        {
            if (deactivating && !animationDone && !inTrigger) CursorFadeOut(tickAmt);
        }
        else if (inTrigger) distance = CaculateDistance(); // if in box
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attacked)
        {
            if (collision.gameObject.tag == "Cursor")
            {
                inTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!attacked)
        {
            if (collision.tag == "Cursor")
            {
                cursor.tag = "Untagged";
                inTrigger = false;
                distance = -1;
                deactivating = true;
                cursorOut = true;
                cursor.GetComponent<AttackBarMovement>().canPress = false;
            }
        }
    }

    public float CaculateDistance()
    {
        return Vector2.Distance(cursor.transform.localPosition, target.transform.localPosition); // if in box
    }

    private void CursorFadeOut(int pace)
    {
        if (!animationDone)
        {
            if (timer % tickAmt == 0)
            {
                cursorRender.color -= new Color(0, 0, 0, fadeAmtPerTick);
                if (cursorRender.color.a <= 0)
                {
                    animationDone = true;
                }
                timer = 0;
            }
            timer++;
        }
    }
}
