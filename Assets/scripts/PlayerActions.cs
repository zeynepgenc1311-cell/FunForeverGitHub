using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public SCItem currentItem;
    public Transform itemHolderPoint;
    GameObject temp = null;

    public void SetItem(SCItem item)
    {
        currentItem = item;

        if (temp)
        {
            Destroy(temp.gameObject);
        }

        if (item.itemPrefab != null)
        {
            temp = Instantiate(item.itemPrefab, itemHolderPoint);
            temp.transform.localPosition = Vector3.zero;
        }
    }
}
