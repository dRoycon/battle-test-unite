using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string content { get; private set; }
    public int speaker {  get; private set; } // 0-default, 1-susie, 2-ralsei, 3-noelle, 4-villian, 5-kris
    public int expression { get; private set; }
    public int voice { get; private set; }

    public Dialogue(string content, int speaker, int expression, int voice)
    {
        this.content = content;
        this.speaker = speaker;
        this.expression = expression;
        this.voice = voice;
    }
}
