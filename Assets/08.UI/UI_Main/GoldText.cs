using UnityEngine;
using TMPro;

public class GoldText : MonoBehaviour
{
    TextMeshProUGUI goldText;

    private void Start()
    {
        if(TryGetComponent(out goldText))
            goldText.text = AccountManager.Instance.Gold.ToString();
    }
}
