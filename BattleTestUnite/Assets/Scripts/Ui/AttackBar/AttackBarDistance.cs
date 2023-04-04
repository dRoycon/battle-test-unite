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
    private bool deactivating;
    public bool attacked;
    public bool animationDone;
    public bool cursorOut; // cursor is out, missed or attacked
    [SerializeField] private float fadeAmtPerTick;
    [SerializeField] private int tickAmt;
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
    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!attacked)
        {
            if (inTrigger) distance = Vector2.Distance(cursor.transform.localPosition, target.transform.localPosition); // if in box
            else if (deactivating) CursorFadeOut(tickAmt);
        }
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
            }
        }
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
                    deactivating = false;
                    animationDone = true;
                }
                timer = 0;
            }
            timer++;
        }
    }
}
