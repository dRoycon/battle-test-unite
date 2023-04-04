using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMaster : MonoBehaviour
{
    public int childAmt;
    public Queue<Transform> queue;

    private void Start()
    {
        queue = new Queue<Transform>();
        transform.GetChild(0);

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
            for (int i = 0; i < cnt; i++)
            {
                queue.Enqueue(queue.Dequeue());
            }
        }
    }

    private void FixedUpdate()
    {
        if (queue.Count == 0)
        {
            Destroy(gameObject);
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
}
