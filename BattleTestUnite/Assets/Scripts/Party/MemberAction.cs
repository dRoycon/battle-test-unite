using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberAction 
{
    public int id { get; protected set; }
    public string fullName { get; protected set; }
    public string nickname { get; protected set; }
    public string description { get; protected set; }
    public string shortDescription { get; protected set; }

    public MemberAction (int _id, string _fullName, string _nickname, string description, string shortDescription)
    {
        id = _id;
        fullName = _fullName;
        nickname = _nickname;
        this.shortDescription = shortDescription;
        this.description = description;
    }
}
