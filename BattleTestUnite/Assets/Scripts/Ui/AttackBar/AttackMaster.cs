using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMaster : MonoBehaviour
{
    public int childAmt;
    private int starterChildAmt;
    public Queue<Transform> queue;
    private int timer;
    [SerializeField] private int tickAmt;
    [SerializeField] private float fadeAmt;
    public bool isFinished { get; private set; }
    private bool isLeaving;

    private void Start()
    {
        queue = new Queue<Transform>();
        transform.GetChild(0);
        isFinished = false;
        isLeaving = false;
        timer = 0;

        Transform[] temp = new Transform[childAmt];
        for (int i = 0; i < childAmt; i++)
        {
            temp[i] = transform.GetChild(i);
        }

        for (int i = 0; i < childAmt; i++)
        {
            float minDistance = 1000;
            int min = 0;
            for (int y = 0; y < childAmt; y++)
            {
                Transform tr = temp[y];
                if (tr != null)
                {
                    if (minDistance > tr.transform.GetChild(0).localPosition.x)
                    {
                        minDistance = tr.transform.GetChild(0).localPosition.x;
                        min = y;
                    }
                }
            }
            queue.Enqueue(temp[min]);
            temp[min] = null;
        }

        queue.Peek().GetChild(0).GetComponent<AttackBarMovement>().isTurn = true;

        Transform q;
        int cnt = 2;
        if (childAmt > 1) // checks if cursors are on same x
        {
            q = queue.Dequeue();
            queue.Enqueue(q);
            if (q.transform.GetChild(0).localPosition.x == queue.Peek().transform.GetChild(0).localPosition.x) // second
            {
                queue.Peek().GetChild(0).GetComponent<AttackBarMovement>().isTurn = true;

                if (childAmt > 2) // checks third cursor
                {
                    cnt--;
                    q = queue.Dequeue();
                    queue.Enqueue(q);
                    if (q.transform.GetChild(0).localPosition.x == queue.Peek().transform.GetChild(0).localPosition.x)
                    {
                        queue.Peek().GetChild(0).GetComponent<AttackBarMovement>().isTurn = true;
                    }
                }
            }
            if (childAmt == 3)
            for (int i = 0; i < cnt; i++)
            {
                queue.Enqueue(queue.Dequeue());
            }
            else queue.Enqueue(queue.Dequeue());
        }
        starterChildAmt = childAmt;
    }

    private void FixedUpdate()
    {
        if (!isFinished)
        {
            if (queue.Count == 0)
            {
                isFinished = true;
            }
            else if (queue.Peek().GetComponent<AttackBarDistance>().cursorOut)
            {
                Transform q;
                int cnt = 1;
                if (childAmt > 1) // checks if cursors are on same x
                {
                    q = queue.Dequeue();
                    queue.Enqueue(q);
                    if (q.transform.GetChild(0).localPosition.x == queue.Peek().transform.GetChild(0).localPosition.x) // second
                    {
                        cnt++;

                        if (childAmt > 2) // checks third cursor
                        {
                            q = queue.Dequeue();
                            queue.Enqueue(q);
                            if (q.transform.GetChild(0).localPosition.x == queue.Peek().transform.GetChild(0).localPosition.x)
                            {
                                cnt++;
                            }
                            queue.Enqueue(queue.Dequeue());
                            queue.Enqueue(queue.Dequeue());
                        }
                    }
                    for (int i = 0; i < childAmt - 1; i++)
                    {
                        queue.Enqueue(queue.Dequeue());
                    }
                }
                for (int i = 0; i < cnt; i++)
                {
                    queue.Dequeue();
                }
                childAmt -= cnt;
                if (childAmt > 0)
                {
                    cnt = 1;

                    if (childAmt > 1) // checks if cursors are on same x
                    {
                        q = queue.Dequeue();
                        queue.Enqueue(q);
                        if (q.transform.GetChild(0).localPosition.x == queue.Peek().transform.GetChild(0).localPosition.x) // second
                        {
                            cnt++;
                        }
                        for (int i = 0; i < childAmt - 1; i++) queue.Enqueue(queue.Dequeue());
                    }
                    for (int i = 0; i < cnt; i++)
                    {
                        q = queue.Dequeue();
                        q.GetChild(0).GetComponent<AttackBarMovement>().isTurn = true;
                        queue.Enqueue(q);
                    }
                    if (childAmt - cnt != 0) queue.Enqueue(queue.Dequeue());
                }
            }
        }
        else
        {
            if (!isLeaving)
            {
                bool willGoOut = true;
                for (int i = 0; i < starterChildAmt; i++)
                {
                    willGoOut = transform.GetChild(i).GetComponent<AttackBarDistance>().animationDone;
                }
                if (willGoOut && timer >= 12)
                {
                    isLeaving = true;
                    timer = 0;

                }
                timer++;
            }
            else
            {
                FadeOut();
            }
        }
    }

    private void FadeOut()
    {
        if (timer % tickAmt == 0)
        {
            SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer child in children)
            {
                child.color -= new Color(0,0,0, fadeAmt);
            }
            if (children[0].color.a <= 0)
            {
                Consts.finishedAttackingTurn = true;
                Destroy(gameObject);
            }
            timer = 0;
        }
        timer++;
    }
}
