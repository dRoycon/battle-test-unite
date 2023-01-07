//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables
    [SerializeField] Party party;
    private bool enemyTurnActivated;
    private bool enemyTurnDeactivated;
    private bool enemyTurn;
    private PlayerHealth health;
    private PlayerTp tp;

    [SerializeField] float MAX_SPEED;
    float speed;

    [SerializeField] bool DASH;
    [SerializeField] int DASH_FORCE;
    [SerializeField] int DASH_COOLDOWN;
    [SerializeField] public int DASH_LIMIT;
    [SerializeField] float dashLast;
    [SerializeField] float dashStreangth;
    int dashCooldown;
    public int dashAmount;


    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite PlayerSp;
    [SerializeField] Sprite PlayerDash;
    [SerializeField] Sprite PlayerNoDash;
    [SerializeField] Sprite PlayerDashing;
    [SerializeField] PlayerHealth _health;
    [SerializeField] PlayerTp _tp;

    Rigidbody2D rb;

    bool up, left, down, right, slowKey, dashkey;
    bool isUp, isLeft, isRight, isDown;
    bool canDash, isDashing;
    bool canMove, canDetectInput;
    int horInpt, verInpt, lastInpt;

    private float lastImageX;
    private float lastImageY;
    private float distanceBetweenImages = 0.4f;

    private int timer;
    private int dashRechargeTimer;
    #endregion


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        horInpt = 0; verInpt = 0;
        canMove = true;
        canDash = true;
        isDashing = false;
        lastInpt = 1;
        canDetectInput = true;
        timer = 0;
        dashAmount = DASH_LIMIT;
        dashRechargeTimer = 0;
        dashCooldown = DASH_COOLDOWN;
        health = GetComponent<PlayerHealth>();
        tp = GetComponent<PlayerTp>();
    }

    private void Start() // Checks if its the player's turn at the start
    {
        enemyTurn = party.isPlayerTurn;
        StartTurn();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        KeyOutput();
        rb.velocity = new Vector2(horInpt, verInpt).normalized * speed;
        Dash();
        StartTurn();
    }

    /// <summary>
    /// this method is called when converting player input into output. should be in FixedUpdate.
    /// </summary>
    private void KeyOutput()
    {
        BasicMovement();
        if (slowKey) speed = 0.45f * MAX_SPEED;
        else speed = MAX_SPEED;
    }

    /// <summary>
    /// This method detects player input
    /// </summary>
    private void PlayerInput()
    {
        if (canDetectInput)
        {
            if (canMove)
            {
                if (Input.GetKey(Consts.keys["up"])) up = true;
                else
                {
                    up = false;
                    isUp = true;
                }
                if (Input.GetKey(Consts.keys["down"])) down = true;
                else
                {
                    down = false;
                    isDown = true;
                }
                if (Input.GetKey(Consts.keys["right"])) right = true;
                else
                {
                    right = false;
                    isRight = true;
                }
                if (Input.GetKey(Consts.keys["left"])) left = true;
                else
                {
                    left = false;
                    isLeft = true;
                }
            }

            if (Input.GetKey(Consts.keys["sneak"])) slowKey = true;
            else slowKey = false;
            if (Input.GetKeyDown(Consts.keys["dash"])) dashkey = true;
        }
    }

    /// <summary>
    /// This method is converts only the regular movement into output. basically just WASD or arrows or whateves
    /// </summary>
    private void BasicMovement()
    {
        if (up)
        {
            if (isUp)
            {
                verInpt = 1;
                lastInpt = 1;
                isUp = false;
            }
            if (!down)
            {
                isUp = up;
            }
        }
        if (down)
        {
            if (isDown)
            {
                verInpt = -1;
                lastInpt = -1;
                isDown = false;
            }
            if (!isUp && !up)
            {
                isDown = down;
                isUp = true;
            }
            else if (!up) verInpt = -1;
        }
        if (!up && !down && !isDashing) verInpt = 0;
        if (right)
        {
            if (isRight)
            {
                horInpt = 1;
                lastInpt = 4;
                isRight = false;
            }
            if (!left)
            {
                isRight = right;
            }
        }
        if (left)
        {
            if (isLeft)
            {
                horInpt = -1;
                lastInpt = 2;
                isLeft = false;
            }
            if (!isRight && !right)
            {
                isLeft = left;
                isRight = true;
            }
            else if (!right) horInpt = -1;
        }
        if (!right && !left && !isDashing) horInpt = 0;
    }

    /// <summary>
    /// Player Dash Output, for dashing shenanigans
    /// </summary>
    private void Dash()
    {
        if (DASH) // if player has the ability to dash
        {
            if (canDash) // if player has recharged and not dashing, allow them to dash
            {
                if (dashkey && dashAmount > 0) // if player presses dash key
                {
                    if (dashAmount >= DASH_LIMIT) sr.sprite = PlayerDashing;
                    else
                    {
                        sr.sprite = PlayerNoDash;
                        dashCooldown = DASH_COOLDOWN / 3 * 2;
                    }
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageX = transform.position.x;
                    lastImageY = transform.position.y;
                    isDashing = true;
                    canDash = false;
                    dashkey = false;
                    dashRechargeTimer = 0;
                    if (dashAmount >= DASH_LIMIT) SpecialDash(true);
                    if (lastInpt > 1) horInpt = lastInpt - 3;
                    else verInpt = lastInpt;
                }
                else if (dashAmount < DASH_LIMIT) DashRecharge(dashCooldown); // recharges dash if player isnt dashing
            }
            else if (isDashing) // while player is dashing
            {
                rb.velocity = new Vector2(horInpt, verInpt).normalized * DASH_FORCE * dashStreangth;
                timer++;
                DashAfterEffect();
                if (timer >= DASH_FORCE/dashLast) // forces player to finish dashing
                {
                    timer = 0;
                    isDashing = false;
                    canMove = true;
                    if (dashAmount >= 2) sr.sprite = PlayerDash;
                    dashAmount = Mathf.Clamp(dashAmount - 1, 0, DASH_LIMIT);
                    canDash = true;
                    dashkey = false;
                }
                else if (timer >= 3) canMove = false;
            }
        }
    }

    /// <summary>
    /// recharge timer for when a player gains a dash back
    /// </summary>
    /// <param name="cooldown"></param>
    private void DashRecharge(int cooldown)
    {
        if (dashRechargeTimer >= cooldown)
        {
            dashAmount = Mathf.Clamp(dashAmount + 1, 0, DASH_LIMIT);
            dashRechargeTimer = 0;
            dashCooldown = DASH_COOLDOWN;
            dashkey = false;
            if (dashAmount == 1) sr.sprite = PlayerDash;
            else sr.sprite = PlayerSp;
        }
        // disables the invis of a special dash, gives a few invis frames after a speciel dash
        else if (dashRechargeTimer == 5 && dashAmount < DASH_LIMIT && dashAmount > DASH_LIMIT-2) SpecialDash(false);

        dashRechargeTimer++;
    }

    /// <summary>
    /// Controls if the game detects player input or not
    /// </summary>
    /// <param name="trigger"></param>
    public void CanMove(bool trigger)
    {
        if (trigger)
        {
            canMove = true;
            canDetectInput = true;
            health.canGetHit = true;
            tp.canGainTp = true;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            Awake();
            dashAmount = DASH_LIMIT;
            canDetectInput = false;
            canMove = false;
            up = false;
            down = false;
            right = false;
            left = false;
            health.SetInvisFrames();
            health.canGetHit = false;
            tp.canGainTp = false;
            sr.sprite = PlayerSp;
        }
    }

    /// <summary>
    /// methos summons the rad after effect
    /// </summary>
    private void DashAfterEffect()
    {
        if (Mathf.Abs(transform.position.x - lastImageX) > distanceBetweenImages || Mathf.Abs(transform.position.y - lastImageY) > distanceBetweenImages)
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageX = transform.position.x;
            lastImageY = transform.position.y;
        }
    }

    /// <summary>
    /// method is used to make a regular dash a [SPECIL] dash or disable one
    /// </summary>
    /// <param name="trigger"></param>
    private void SpecialDash(bool trigger)
    {
        _health.canGetHit = !trigger;
        _tp.canGainTp = !trigger;
    }

    private void StartTurn() // Checks if its the player's turn
    {
        if (party.isPlayerTurn) enemyTurnDeactivated = true;
        else enemyTurnActivated = true;

        if (enemyTurnActivated)
        {
            if (enemyTurn == false)
            {
                transform.position = new Vector2(0, 4.8f); // TEMPORARY. enemy attacks should set player pos
                enemyTurn = true;
                CanMove(true);
                enemyTurnActivated = false;
                enemyTurnDeactivated = false;
            }
        }
        if (enemyTurnDeactivated)
        {
            if (enemyTurn == true)
            {
                enemyTurn = false;
                CanMove(false);
                transform.position = new Vector2(0, 19);
                enemyTurnDeactivated = false;
                enemyTurnActivated = false;
            }
        }
    }
}
