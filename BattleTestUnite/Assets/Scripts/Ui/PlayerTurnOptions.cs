//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class PlayerTurnOptions : MonoBehaviour
//{
//    #region variables

//    [SerializeField]
//    private bool hasMagic;
//    [SerializeField]
//    private Sprite magic;
//    Image[] options;
//    [SerializeField]
//    private Image fight;
//    [SerializeField]
//    private Image ability;
//    [SerializeField]
//    private Image item;
//    [SerializeField]
//    private Image spare;
//    [SerializeField]
//    private Image defend;

//    private int opt;
//    private int prevOpt;
//    private int dif;
//    private const int amt = 5;

//    #endregion
//    void Start()
//    {
//        opt = 1;
//        options = new Image[amt]
//        {
//            fight,ability,item,spare,defend
//        };
//        options[opt-1].color = Consts.NoelleYellow;
//        if (hasMagic) ability.sprite = magic;
//    }

//    void Update()
//    {
//        dif = select.options(amt, opt);
//        if (dif != 0)
//        {
//            prevOpt = opt;
//            opt += dif;
//            options[opt-1].color = Consts.NoelleYellow;
//            options[prevOpt-1].color = Consts.StaticOrange;
//        }

//    }

//    private void FixedUpdate()
//    {
        
//    }
//}
