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
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log(distance);
        //}
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
                //Debug.Log("enter");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!attacked)
        {
            if (collision.tag == "Cursor")
            {
                inTrigger = false;
                distance = -1;
                //Debug.Log("exit");
                deactivating = true;
            }
        }
    }

    private void CursorFadeOut(int pace)
    {
        if (timer % tickAmt == 0)
        {
            cursorRender.color -= new Color(0, 0, 0, fadeAmtPerTick);
            if (cursorRender.color.a <= 0)
            {
                Destroy(cursor);
                deactivating = false;
            }
            timer = 0;
        }
        timer++;
    }
}
