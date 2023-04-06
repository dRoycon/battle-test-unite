using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMaster : MonoBehaviour
{
    [SerializeField] private GameObject chara;
    [SerializeField] private float distance;
    private PlayerParty party;
    private int pMemberAmt;

    private void OnEnable()
    {
        party = FindObjectOfType<PlayerParty>();
        Debug.Log(chara.GetComponent<RectTransform>().rect.width);
    }

    void Start()
    {
        pMemberAmt = party.CountActiveMembers();
        for (int i = 0; i < pMemberAmt; i++)
        {
            GameObject ch = Instantiate(chara, new Vector2((i-1) * chara.GetComponent<RectTransform>().rect.width * distance, -177.3558f), Quaternion.identity);
            ch.transform.SetParent(transform, false);
            ch.GetComponent<CharacterUi>().spot = i;
        }
    }


    void Update()
    {
        
    }
}
