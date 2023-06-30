using TMPro;
using UnityEngine;

public class Description : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = new Vector2(16.97655f, -10.87598f);
        text = GetComponent<TextMeshProUGUI>();
        text.color = new Color(128f / 255f, 128f / 255f, 128f / 255f);
        text.margin = new Vector4(-7.2615f, 0, -33.303f, 0);
    }

    public void SetText(string x)
    {
        text.text = x;
    }



}
