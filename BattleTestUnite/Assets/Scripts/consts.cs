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
    public static Color KrisBlue = new Color(0, 255, 255);
    public static Color SusieMagenta = new Color(255, 0, 255);
    public static Color RalseiGreen = new Color(0, 255, 0);
    public static Color NoelleYellow = new Color(255, 255, 0);
    public static Color FaintRed = new Color(128f/255f, 0, 0);
    #endregion
}


