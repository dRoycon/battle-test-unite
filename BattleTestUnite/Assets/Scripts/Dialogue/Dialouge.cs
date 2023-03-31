using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge
{
    public string text { get; private set; }
    public string name { get; private set; }
    public int expression { get; private set; }
    public int speed { get; private set; }

    public Dialouge(string text, string name, int expression)
    {
        this.text = text;
        this.name = name;
        this.expression = expression;
        speed = 5;
    }
    public Dialouge(string text, string name, int expression, int speed)
    {
        this.text = text;
        this.name = name;
        this.expression = expression;
        this.speed = speed;
    }
}
