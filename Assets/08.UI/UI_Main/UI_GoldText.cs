using UnityEngine;
using TMPro;

public class UI_GoldText : MonoBehaviour
{
    TextMeshProUGUI goldText;

    private void Start()
    {
        if (TryGetComponent(out goldText))
            RenewalGoldText();
    }

    public void RenewalGoldText()
    {
        goldText.text = AccountManager.Instance.Gold.ToString();
    }
}
