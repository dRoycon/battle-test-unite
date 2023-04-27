using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Consts
{

    #region controls
    static public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>
    {
        {"up" , KeyCode.UpArrow},
        {"left" , KeyCode.LeftArrow},
        {"down" , KeyCode.DownArrow},
        {"right" , KeyCode.RightArrow},
        {"confirm" , KeyCode.Z},
        {"cancel" , KeyCode.X},
        {"dash", KeyCode.Z},
        {"sneak", KeyCode.X },
        {"pause", KeyCode.Escape}
    };
    #endregion

    #region colors
    public static Color SoulRed = new Color(255, 0, 0);
    public static Color CourageOrange = new Color(255f/255f, 160f/255f, 64f/255f);
    public static Color StaticOrange = new Color(255f/255f, 127f/255f, 39f/255f);
    public static Color FaintRed = new Color(128f / 255f, 0, 0);

    public static Color KrisBlue = new Color(0, 255, 255);
    public static Color KrisAccent1 = new Color(0, 0, 255f / 255f);
    public static Color KrisAccent2 = new Color(0, 162f/255f, 232f/255f);

    public static Color SusieMagenta = new Color(255, 0, 255);
    public static Color SusieAccent1 = new Color(128f/255f, 0, 128f/255f);
    public static Color SusieAccent2 = new Color(234f/255f, 121f/255f, 200f/255f);

    public static Color RalseiGreen = new Color(0, 255, 0);
    public static Color RalseiAccent1 = new Color(0, 128f / 255f, 0);
    public static Color RalseiAccent2 = new Color(181f/255f, 230f/255f, 29f/255f);

    public static Color NoelleYellow = new Color(255, 255, 0);
    public static Color NoelleAccent1 = new Color(255f/255f, 255f/255f, 0);
    public static Color NoelleAccent2 = new Color(254f/255f, 254f/255f, 254f/255f);


    #endregion

    #region bools
    public static bool finishedAttackingTurn = true;

    #endregion

    #region items
    public static Item bcPieSlice = new Item(1, 90, "ButterscotchCinnamon Pie Slice", "bcSlice", "Homemade specialty, HEALS 90", false);
    public static Item bcPieFull = new Item(1, 90, "ButterscotchCinnamon Pie", "bcPie", "Feels all with LOVE, HEALS PARTY 90", true);
    public static Item SnailPieSlice = new Item(1, 90, "Snail Pie Slice", "sPie","Only a treat for some, HEALS ??", false);
    public static Item SnailPieFull = new Item(1, 90, "Snail Pie Slice", "sPie", "Only a treat for some, HEALS PARTY ??", true);
    #endregion

    #region player/party
    public static Party playerParty;

    public static PlayerPartyMember kris = new PlayerPartyMember(0, "Kris", 270, 2, 18, 0, false, KrisBlue, KrisAccent1, KrisAccent2);
    public static PlayerPartyMember susie = new PlayerPartyMember(1, "Susie", 340, 2, 23, 8, true, SusieMagenta, SusieAccent1, SusieAccent2);
    public static PlayerPartyMember ralsei = new PlayerPartyMember(2, "Ralsei", 250, 2, 16, 16, true, RalseiGreen, RalseiAccent1, RalseiAccent2);
    public static PlayerPartyMember noelle = new PlayerPartyMember(3, "Noelle", 160, 1, 7, 14, true, NoelleYellow, NoelleAccent1, NoelleAccent2);
    #endregion
}


