using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberAction 
{
    public int id { get; protected set; }
    public string fullName { get; protected set; }
    public string nickname { get; protected set; }
    public string description { get; protected set; }

    public MemberAction (int _id, string _fullName, string _nickname)
    {
        id = _id;
        fullName = fullName;
        nickname = _nickname;
    }
}