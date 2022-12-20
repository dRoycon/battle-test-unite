using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoxSize : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] float x;
    [SerializeField] float y;
    private const float thickness = 3;
    private const float borders = 0.239f;
    private const float cenetrOffset = 9;
    #region faces
    // HitBox
    [SerializeField] Transform _01;
    [SerializeField] Transform _10;
    [SerializeField] Transform _21;
    [SerializeField] Transform _12;
    [SerializeField] Transform _00;
    [SerializeField] Transform _02;
    [SerializeField] Transform _20;
    [SerializeField] Transform _22;
    [SerializeField] Transform back;
    // Visual
    [SerializeField] Transform _01B;
    [SerializeField] Transform _10B;
    [SerializeField] Transform _21B;
    [SerializeField] Transform _12B;

    #endregion

    void Awake()
    {
        updateScale(height, width, x, y, thickness);
    }

    public void updateScale(float height, float width, float posX, float posY, float thickness)
    {
        float centerX = (-width + cenetrOffset) / 2 + posX;
        float centerY = (-height + cenetrOffset) / -2 + posY;

        #region scale
        //Hitbox

        // Scale of | |
        _01.transform.localScale = new Vector2(thickness, height+_00.transform.localScale.x);
        _21.transform.localScale = _01.transform.localScale;
        // Scale of =
        _10.transform.localScale = new Vector2(width+_00.transform.localScale.x, thickness);
        _12.transform.localScale = _10.transform.localScale;

        // Visual

        back.transform.localScale = new Vector2(width , height);
        // unparenting
        _01B.transform.parent = _01.transform.parent;
        _21B.transform.parent = _21.transform.parent;
        _10B.transform.parent = _10.transform.parent;
        _12B.transform.parent = _12.transform.parent;
        // scaling
        _01B.transform.localScale = new Vector2(borders, height);
        _21B.transform.localScale = new Vector2(borders, height);
        _10B.transform.localScale = new Vector2(width, borders);
        _12B.transform.localScale = new Vector2(width, borders);
        // reparenting
        _01B.transform.parent = _01;
        _21B.transform.parent = _21;
        _10B.transform.parent = _10;
        _12B.transform.parent = _12;
        #endregion

        #region position
        // Hitbox
        _00.transform.localPosition = new Vector2(centerX, centerY);
        _10.transform.localPosition = new Vector2(_10.transform.localScale.x / 2 + centerX, 0 + centerY);
        _20.transform.localPosition = new Vector2(_10.transform.localScale.x + centerX, 0 + centerY);
        _01.transform.localPosition = new Vector2(centerX, -_01.transform.localScale.y / 2 + centerY);
        _02.transform.localPosition = new Vector2(centerX, -_01.transform.localScale.y + centerY);
        _12.transform.localPosition = new Vector2(_10.transform.localScale.x / 2 + centerX, -_01.transform.localScale.y + centerY);
        _22.transform.localPosition = new Vector2(_10.transform.localScale.x + centerX, -_01.transform.localScale.y + centerY);
        _21.transform.localPosition = new Vector2(_10.transform.localScale.x + centerX, -_01.transform.localScale.y / 2 + centerY);

        //Visual
        back.transform.localPosition = new Vector2(_10.transform.localScale.x / 2 + centerX, -_01.transform.localScale.y / 2 + centerY);
        #endregion
    }
}
