using UnityEngine;
using TMPro; // TextMeshPro kullanıyorsan

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [Header("Para Miktarları")]
    public int coins;
    public int gems;

    [Header("UI")]
    public TMP_Text coinText;
    public TMP_Text gemText;

    void Awake()
{
    Instance = this;

    if (!PlayerPrefs.HasKey("coins"))
        PlayerPrefs.SetInt("coins", coins); // inspector değerini kaydet

    if (!PlayerPrefs.HasKey("gems"))
        PlayerPrefs.SetInt("gems", gems);

    coins = PlayerPrefs.GetInt("coins");
    gems = PlayerPrefs.GetInt("gems");
}


    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("coins", coins);
        UpdateUI();
    }

    public void AddGems(int amount)
    {
        gems += amount;
        PlayerPrefs.SetInt("gems", gems);
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt("coins", coins);
            UpdateUI();
            return true;
        }
        return false;
    }

    public bool SpendGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            PlayerPrefs.SetInt("gems", gems);
            UpdateUI();
            return true;
        }
        return false;
    }

    public void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coins.ToString();

        if (gemText != null)
            gemText.text = gems.ToString();
    }
}
