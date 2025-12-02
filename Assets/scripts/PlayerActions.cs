using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject currentItem;
    public Transform itemHolderPoint;
    GameObject temp = null;
    public void SetItem(GameObject item)
    {
        currentItem = item;
        if (temp)
        {
            Destroy(temp.gameObject);
        }
        temp = Instantiate(item, itemHolderPoint);
        temp.transform.localPosition = Vector3.zero;
    }
}
