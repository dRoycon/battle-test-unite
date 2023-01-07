using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTp : MonoBehaviour
{
    public const int MAX_TP = 250;
    private static int TP_DEFEND = 40;
    [SerializeField] public int tp { get; private set; }
    [SerializeField] SpriteRenderer sr;
    [SerializeField] PlayerHealth p;
    [SerializeField] TpBar ui;
    public bool canGainTp;
    private int tpPercent;
    private int timer;

    // Start is called before the first frame update
    void Start()
    {
        canGainTp = true;
        tp = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static int Defend(int tp)
    {
        return Mathf.Clamp(tp + TP_DEFEND, 0, MAX_TP);
    }

    public void AddTp(float distance)
    {

        if (!p.InvisFrames() && canGainTp)
        {
            timer++;
            float speed = Mathf.Clamp(Mathf.Round(distance * 2.2f), 0, 100);
            if (Input.GetKey(KeyCode.T)) tp = 0;
            if (timer % (int)speed == 0)
            {
                tp = Mathf.Clamp(tp + 1, 0, MAX_TP);
                UpdtateTpPercent();
                timer = 0;
                sr.enabled = true;
                ui.CheckTp();
            }
        }
        else
        {
            sr.enabled = false;
        }
    }

    public void TpRadius(bool istrue)
    {
        sr.enabled = istrue;
    }

    public void SetTp(int _tp)
    {
        tp = Mathf.Clamp(_tp, 0, MAX_TP);
    }

    public void UpdtateTpPercent()
    {
        tpPercent = (int)Math.Ceiling(100 * (float)((float)tp / (float)MAX_TP));
    }

    public float TpPercent() { return tpPercent; }
}
