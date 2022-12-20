using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int spot;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;
    [SerializeField] private TextMeshProUGUI txt;
    Party party;

    private void Start()
    {
        //party = GameObject.Find("Party").GetComponent<Party>;
        //Debug.Log(party.activePartyMembers[0].nickname);
        //totalhealthbar.fillAmount = party.activePartyMembers[spot].hp / party.activePartyMembers[spot].maxHp;
    }

    //private void Update()
    //{
    //    currenthealthbar.fillAmount = party.activePartyMembers[spot].hp / party.activePartyMembers[spot].maxHp;
    //    if (party.activePartyMembers[spot].hp > 9) txt.text = party.activePartyMembers[spot].hp + " / " + party.activePartyMembers[spot].maxHp;
    //    else txt.text = "  " + party.activePartyMembers[spot].hp + " / " + party.activePartyMembers[spot].maxHp;
    //}
}
