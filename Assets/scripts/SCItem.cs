using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class SCItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public Sprite itemSprite;
    public int price;
    public bool canStackable;
    public int itemPrice = 1;

    // Envantar ve world için:
    public GameObject itemPrefab;
}
