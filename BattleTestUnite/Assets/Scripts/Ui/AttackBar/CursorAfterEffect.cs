using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAfterEffect : MonoBehaviour
{
    [SerializeField] private int tickTillTimer;
    [SerializeField] private float startAlpha;
    [SerializeField] private float alphaMultiplayer;
    private SpriteRenderer sr;
    private int timer;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        timer = 0;
        transform.localScale = new Vector3(1, 1, 1);
        sr.color = new Color(255, 255, 255, startAlpha);
    }
    private void Update()
    {
        if (timer == tickTillTimer)
        {
            sr.color = new Color(255, 255, 255, sr.color.a*alphaMultiplayer);
            timer = 0;
            if (sr.color.a <= 0.05) Destroy(gameObject);
        }
        timer++;
    }
}
