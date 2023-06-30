using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowHover : MonoBehaviour
{
    private float y;
    private float startY;
    private const float X = 11.28f;
    private const float BOTTOM_Y = -15.31f;
    private const float TOP_Y = -10.82f;
    private bool isFlipped;
    [SerializeField] private float heightAmp;
    [SerializeField] private float widthAmp;
    private void Start()
    {
        y = 0;
        transform.position = new Vector2(X, BOTTOM_Y);
        startY = transform.position.y;
        isFlipped = false;
    }
    void Update()
    {
        y += 0.01f;
        if (y >= Mathf.PI * 2 * (1/widthAmp)) y = 0; 
        transform.position = new Vector2(transform.position.x, startY + (heightAmp*Mathf.Sin(widthAmp*y)));
    }

    public void Flip()
    {
        transform.Rotate(0, 0, 180);
        if (isFlipped) startY = BOTTOM_Y;
        else startY = TOP_Y;
        isFlipped = !isFlipped;
    }
}
