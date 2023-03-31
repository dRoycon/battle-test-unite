using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private int spot;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI currentHp;
    [SerializeField] private TextMeshProUGUI maxHp;
    PlayerParty party;

    private void Start()
    {
        spot = GetComponentInParent<CharacterUi>().spot;
        party = GetComponentInParent<CharacterUi>().party;
        healthBar.fillAmount = party.activePartyMembers[spot].hp / party.activePartyMembers[spot].maxHp;
        maxHp.text = party.activePartyMembers[spot].maxHp + "";
        healthBar.color = ((PlayerPartyMember)party.activePartyMembers[spot]).color;
    }

    private void Update()
    {
        healthBar.fillAmount = (float)party.activePartyMembers[spot].hp / (float)party.activePartyMembers[spot].maxHp;
        currentHp.text = party.activePartyMembers[spot].hp+"";
    }
}
