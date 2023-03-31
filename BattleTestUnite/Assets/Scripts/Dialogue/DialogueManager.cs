using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager: MonoBehaviour
{
    private List<DialougeSequence> dialogue;

    void Awake()
    {
        dialogue = new List<DialougeSequence>();
        dialogue.Add(new DialougeSequence());
        dialogue[0].AddText("I'm a Goofy Goober #1", "Goober", 1);
        dialogue[0].AddText("Goober Mode Activated #2", "Gooby", 1);
        dialogue[0].AddText("Ingaging in GOOBERING ACTIVITIES #3", "Goobla", 1);
        dialogue.Add(new DialougeSequence());
        dialogue[1].AddText("WIll YOU KIDDos ShuT It!! I HATER GOBBER!!", "GooberHater", 1);
        dialogue[1].AddText("no", "Goober", 1);
        dialogue[1].AddText("brueh, I gueess i was wrogg", "FormerGooberHater", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(dialogue[0].NextText());
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(dialogue[1].NextText());
        }
    }
}
