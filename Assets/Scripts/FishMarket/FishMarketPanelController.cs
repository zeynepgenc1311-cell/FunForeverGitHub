using UnityEngine;

public class FishMarketPanelController : MonoBehaviour
{
    public GameObject fishSellPanel;   // Balık satış paneli
    public GameObject rodShopPanel;    // Olta satın alma paneli

    public void OpenFishPanel()
    {
        fishSellPanel.SetActive(true);
        rodShopPanel.SetActive(false);
    }

    public void OpenRodPanel()
    {
        fishSellPanel.SetActive(false);
        rodShopPanel.SetActive(true);
    }
}
