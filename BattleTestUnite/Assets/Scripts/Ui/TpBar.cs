using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TpBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private PlayerTp tp;
    [SerializeField] private Image currentTp;
    [SerializeField] private Image removeTp;
    [SerializeField] private Image addTp;
    [SerializeField] private Image fullTp;
    [SerializeField] private Image tpText;

    private void Awake()
    {
        CheckTp();
    }

    private void Update()
    {
    }

    public void CheckTp()
    {
        currentTp.fillAmount = tp.TpPercent() / 100;
        if (tp.TpPercent() == 100)
        {
            txt.text = "M \r\n A \r\n  X";
            tpText.fillAmount = 0.5f;
            fullTp.enabled = true;
        }
        else
        {
            txt.text = tp.TpPercent()+"";
            fullTp.enabled = false;
            tpText.fillAmount = 1;
            if (tp.TpPercent() < 5) addTp.fillAmount = 0;
            else
            {
                addTp.fillAmount = Mathf.Clamp(tp.TpPercent() / 100 + 0.01f, 0, 1);
            }
        }
    }

}
