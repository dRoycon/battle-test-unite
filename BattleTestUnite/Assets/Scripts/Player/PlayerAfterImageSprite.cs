using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField] private float alphaSet = 0.8f;
    [SerializeField] private float alphaMultiplier = 0.85f;

    private Transform player;
    private PlayerMovement _playerMovement;
    private int r;
    private int g;
    private int b;

    private SpriteRenderer sr;
    [SerializeField] private Sprite sp;
    [SerializeField] private Sprite spSpeciel;

    private Color color;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        alpha = alphaSet;
        transform.position = player.position;
        transform.rotation = player.rotation;
        _playerMovement =  player.GetComponent<PlayerMovement>();
        if (_playerMovement.dashAmount == _playerMovement.DASH_LIMIT) // color for special dash
        {
            sr.sprite = spSpeciel;
            r = 255;
            g = 255;
            b = 255;
        }
        else // color for regular dash
        {
            sr.sprite = sp;
            r = 0;
            g = 223;
            b = 225;
        }
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(r/255f, g/255f, b/255f, alpha);
        sr.color = color;

        if (Time.time >= timeActivated + activeTime)
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
