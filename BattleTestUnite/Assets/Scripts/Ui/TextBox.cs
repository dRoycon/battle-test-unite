using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    private string content;
    private int maxCharPer; // 34-no portrait , ??-portrait
    private int charPer;
    private int pos;
    private bool willWrite;
    bool changeSpeed;
    private TextMeshProUGUI text;
    private int count;
    private int tickPerChar;
    private bool checkWord;
    public bool isWriting { get; private set; }
    public bool finishedWriting { get; private set; }
    public bool isDone { get; private set; }

    private bool isPressDone;
    private bool isPressHurry;

    void Start()
    {
        pos = 0;
        count = 0;
        willWrite = true;
        changeSpeed = true;
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
        checkWord = false;
        finishedWriting = false;
        isWriting = false;
        maxCharPer = 34;
        isPressDone = false;
        isPressHurry = false;
        isDone = true;
        //
        //Write(new Dialogue("<color=red>This sentence <color=white>number 1, how are <color=blue>yall doing ooga <color=yellow>booga<br>this is sentence 2!!", 1, 0, 0));
    }

    void Update()
    {
        if (!isDone)
            if (Input.GetKeyDown(Consts.keys["confirm"])) isPressDone = true;
        if (!finishedWriting && isWriting)
        {
            if (Input.GetKeyDown(Consts.keys["cancel"])) isPressHurry = true;
            if (changeSpeed)
            {
                if (count == tickPerChar)
                {
                    GetText(content);
                    count = 0;
                }
                if (changeSpeed) count++;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPressHurry)
        {
            Debug.Log("X");
            changeSpeed = false;
            isPressHurry = false;
            for (int i = pos; i < content.Length - 3; i++)
            {
                GetText(content);
            }
        }
        if (isPressDone)
        {
            Debug.Log("Z");
            isPressDone = false;
            if (finishedWriting)
            {
                isDone = true;
                text.text = "";
            }
        }
    }

    public void Write(Dialogue d)
    {
        pos = 0;
        count = 0;
        charPer = 0;
        willWrite = true;
        changeSpeed = true;
        text.text = "";
        checkWord = false;
        finishedWriting = false;
        isWriting = true;
        isDone = false;
        content = d.content;
        tickPerChar = 20;
        DisplayText("<color=white>* ");
        if (d.speaker == 0) maxCharPer = 34;
        else maxCharPer = 28;
    }

    public void Write(string str)
    {
        Write(new Dialogue(str, 0, 0, 0));
    }

    void GetText(string str)
    {
        string add = "";
        char ch = str[pos];
        if (willWrite)
        {
            if (ch == '<')
            {
                add += GetCommand(str);
                DisplayText(add);
                if (pos >= str.Length)
                {
                    finishedWriting = true;
                    isWriting = false;
                }
                else GetText(str);
            }
            else if (ch == '@')
            {
                int newSpeed = ChangeSpeed(str);
                if (changeSpeed) tickPerChar = newSpeed;
                if (pos >= str.Length)
                {
                    finishedWriting = true;
                    isWriting = false;
                }
                else GetText(str);
            }
            else if (checkWord || ch == ' ')
            {
                add += ch;
                DisplayText(add);
                if (ch == ' ')
                {
                    charPer++;
                    checkWord = false;
                }
                pos++;
            }
            else
            {
                if (!hasRoom(str))
                    add += "<br>  ";
                add += ch;
                checkWord = true;
                DisplayText(add);
                pos++;
            }
        }
        Debug.Log(charPer);
        if (pos >= str.Length)
        {
            finishedWriting = true;
            isWriting = false;
        }
    }

    void DisplayText(string add)
    {
        text.text += add;
    }

    /// <summary>
    /// check if there is enough room in a setntece for a word
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    bool hasRoom(string str)
    {
        int preCharPer = charPer;
        int cnt = pos;
        while (cnt < str.Length && str[cnt] != ' ' && str[cnt] != '<' && str[cnt] != '@')
        {
            charPer++;
            cnt++;
        }

        if (charPer > maxCharPer)
        {
            charPer -= preCharPer;
            return false;
        }
        return true;
    }

    string GetCommand(string str)
    {
        string res = "";
        while (pos < str.Length && str[pos] != '>')
        {
            res += str[pos];
            pos++;
        }
        res += '>';
        if (res == "<br>")
        {
            charPer = 0;
            res = "<color=white><br>* ";
        }
        pos++;
        checkWord = false;
        return res;
    }

    int ChangeSpeed(string str)
    {
        int speed = 0;
        pos++;
        while (pos < str.Length && str[pos] != '@')
        {
            int x = (int)str[pos]-48;
            pos++;
            if (changeSpeed) speed = (speed * 10) + x;
        }
        pos++;
        checkWord = false;
        return speed;
    }
}
