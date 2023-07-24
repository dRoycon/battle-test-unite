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

    void Start()
    {
        pos = 0;
        count = 0;
        willWrite = true;
        changeSpeed = false;
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
        checkWord = false;
        finishedWriting = false;
        isWriting = false;
        maxCharPer = 34;
        //
        Write(new Dialogue("This sentence number 1, how are yall doing ooga booga<br>this is sentence 2!!", 1, 0, 0));
    }

    void Update()
    {
        if (!finishedWriting && isWriting)
        {
            if (count == tickPerChar)
            {
                GetText(content);
                count = 0;
            }
            count++;
        }
    }

    public void Write(Dialogue d)
    {
        pos = 0;
        count = 0;
        willWrite = true;
        changeSpeed = false;
        text.text = "";
        checkWord = false;
        finishedWriting = false;
        isWriting = true;
        content = d.content;
        tickPerChar = 20;
        DisplayText("* ");
        if (d.speaker == 0) maxCharPer = 34;
        else maxCharPer = 28;
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
            }
            else if (ch == '@')
            {
                tickPerChar = ChangeSpeed(str);
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
            }
            else
            {
                if (!hasRoom(str))
                    add += "<br>  ";
                add += ch;
                checkWord = true;
                DisplayText(add);
            }
        }
        Debug.Log(charPer);
        pos++;
        if (pos >= str.Length) finishedWriting = true;
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
            res = "<br>* ";
        }
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
            speed = (speed * 10) + x;
        }
        checkWord = false;
        return speed;
    }
}
