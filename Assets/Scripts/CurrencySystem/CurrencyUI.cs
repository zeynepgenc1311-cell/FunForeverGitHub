using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI rainbowCoinText;

    void Update()
    {
        coinText.text = CurrencyManager.Instance.coins.ToString();
        gemText.text = CurrencyManager.Instance.gems.ToString();
        rainbowCoinText.text = CurrencyManager.Instance.rainbowCoins.ToString();
    }
}
