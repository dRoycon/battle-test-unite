using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarDistance : MonoBehaviour
{
    bool inTrigger;
    private Transform tr;
    private float distance;
    [SerializeField] private GameObject target;

    void Start()
    {
        inTrigger = false;
        distance = -1;
        tr = GetComponent<Transform>();
    }
    private void Update()
    {
        Debug.Log(distance);
    }

    private void FixedUpdate()
    {
        if (inTrigger)
        {
            distance = Vector2.Distance(tr.transform.localPosition, target.transform.localPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackBoard")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AttackBoard")
        {
            Debug.Log("C");
            inTrigger = false;
            distance = -1;
        }
    }
}
