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
    public static Color CourageOrange = new Color(255f / 255f, 160f / 255f, 64f / 255f);
    public static Color StaticOrange = new Color(255f / 255f, 127f / 255f, 39f / 255f);
    public static Color FaintRed = new Color(128f / 255f, 0, 0);
    public static Color UnusableGray = new Color(128f / 255f, 128f / 255f, 128f / 255f);

    public static Color KrisBlue = new Color(0, 255, 255);
    public static Color KrisAccent1 = new Color(0, 0, 255f / 255f);
    public static Color KrisAccent2 = new Color(0, 162f / 255f, 232f / 255f);

    public static Color SusieMagenta = new Color(255, 0, 255);
    public static Color SusieAccent1 = new Color(128f / 255f, 0, 128f / 255f);
    public static Color SusieAccent2 = new Color(234f / 255f, 121f / 255f, 200f / 255f);

    public static Color RalseiGreen = new Color(0, 255, 0);
    public static Color RalseiAccent1 = new Color(0, 128f / 255f, 0);
    public static Color RalseiAccent2 = new Color(181f / 255f, 230f / 255f, 29f / 255f);

    public static Color NoelleYellow = new Color(255, 255, 0);
    public static Color NoelleAccent1 = new Color(255f / 255f, 255f / 255f, 0);
    public static Color NoelleAccent2 = new Color(254f / 255f, 254f / 255f, 254f / 255f);


    #endregion

    #region bools
    public static bool finishedAttackingTurn = true;

    #endregion


    #region spells
    static public Dictionary<string, Magic> spells = new Dictionary<string, Magic>
    {
        {"healPrayer" ,  new Magic
            (1, 2, 32, "Heal Prayer", "Heavenly light restores a little HP to one party member. Depends on Magic.", "Heal <br>Ally", false)},
       {"pacify" , new Magic
            (2, 3, 16, "Pacify", "SPARE a tired enemy by putting them to sleep.", "Spare <br>TIRED foe", false)},
        {"rudeBuster" , new Magic
            (3, 1, 50, "Rude Buster", "Deals moderate Rude-elemental damage to one foe. Depends on Attack & Magic.", "Rude <br>Damage", false) },
        {"ultimateHeal", new Magic
            (4, 2, 85, "UltimateHeal", "Heals one ally to the best of Susie's ability.", "Best <br>healing", false) },
        {"sleepMist", new Magic
            (5, 3, 32, "Sleep Mist", "A cold mist sweeps through, sparing all TIRED enemies.", "Spare <br>TIRED foes ", true) },
        {"iceShock", new Magic
            (6, 1, 16, "IceShock", "Deal magical ICE damage to one enemy.", "Damage <br>w/ ICE", false) },
        {"snowGrave", new Magic
            (7, 1, 200, "SnowGrave", "Deals fatal damage for enemies of NUGGET element.", "Fatal", true)}
    };
    #endregion

    #region player/party
    public static PlayerParty playerParty;

    public static PlayerPartyMember kris = new PlayerPartyMember(Kris.krisId, "Kris", 270, 2, 18, 0, false, KrisBlue, KrisAccent1, KrisAccent2);
    public static Susie susie = new Susie(Susie.susieId, 340, 2, 23, 9);
    public static Ralsei ralsei = new Ralsei(Ralsei.ralseiId, 250, 2, 16, 16);
    public static Noelle noelle = new Noelle(Noelle.noelleId, 160, 1, 7, 18);
    #endregion

    #region items
    public static Item[] items = new Item[] {
    new Item(0, 200, "ButterscotchCinnamon Pie Slice", "bcSlice", "Homemade specialty, Heals 200HP", "Heals <br>200HP", false),
    new Item(1, 200, "ButterscotchCinnamon Pie", "bcPie", "Feels all with love, Heals Party 200HP", "Heals <br>Party <br>200HP", true),
    new Item(2, new int[]{ 220, 260, 110, 200}, "Snail Pie Slice", "sPieSlice", "Only a treat for some, Heals ??HP", "Heals <br>??HP", false),
    new Item(3, new int[]{ 220, 260, 110, 200}, "Snail Pie", "sPie", "Only a treat for some, Heals Party ??HP", "Heals <br>Party <br>??HP", true),
    new Item(4, 40, "Dummy Item", "DummyItem", "Yummy Cloth, Heals 40HP", "Heals <br>40HP", false),
    new Item(5, 100000, "ReviveMint", "ReviveMint", "Heals a downed ally to full HP.", "Heal <br>Downed <br>Ally", false)
    };
    #endregion
}


