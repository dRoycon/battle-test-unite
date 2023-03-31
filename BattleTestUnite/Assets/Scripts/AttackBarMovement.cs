using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarMovement : MonoBehaviour
{
    public bool isFinished;
    private Transform tr;
    [SerializeField] private float space = 0.01f;
    [SerializeField] private float speed = 1;
    bool trig; // debug
    private void Start()
    {
        isFinished = false;
        tr = GetComponent<Transform>();
        trig = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            trig = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            speed = 0;
            isFinished = true;
        }
    }

    private void FixedUpdate()
    {
        if (trig)
        {
            tr.localPosition += new Vector3(-space, 0, 0) * speed;
            trig = false;
        }
    }
}
