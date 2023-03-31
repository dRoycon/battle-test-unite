using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeSequence
{
    public string name { get; private set; }
    public Queue<Dialouge> texts { get; private set; }
    private bool finishedAdding;
    private bool nextFinishScence;
    private bool ScenceDone;
    public DialougeSequence()
    {
        texts = new Queue<Dialouge>();
        finishedAdding = false;
        nextFinishScence = false;
        ScenceDone = false;
    }
    public DialougeSequence(int speed)
    {
        texts = new Queue<Dialouge>();
        finishedAdding = false;
    }

    public void AddText(string txt, string name, int expression)
    {
        Dialouge add = new Dialouge(txt, name, expression);
        texts.Enqueue(add);
    }

    public void AddText(string txt, string name, int expression, int speed)
    {
        Dialouge add = new Dialouge(txt, name, expression, speed);
        texts.Enqueue(add);
    }

    public string NextText()
    {
        string res = "-1";
        if (!finishedAdding) AddText("-1", "-1", 0);
        finishedAdding = true;

        if (ScenceDone)
        {
            nextFinishScence = false;
            ScenceDone = false;
        }
        if (nextFinishScence)
        {
            DialougeOver();
            ScenceDone = true;
        }
        else
        {
            res = texts.Peek().name + ": " + texts.Peek().text;
            texts.Enqueue(texts.Dequeue());
        }
        if (!(texts.Peek().text != "-1")) nextFinishScence = true;
        return res;
    }

    public void DialougeOver()
    {
        texts.Enqueue(texts.Dequeue());
        Debug.Log("Over");
    }
}
