using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopPanel;

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
