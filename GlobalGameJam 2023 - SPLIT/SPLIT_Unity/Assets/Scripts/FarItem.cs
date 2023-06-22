using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarItem : MonoBehaviour
{
    public Item item;
    public Image img;

    public void Start()
    {
        if (!item.hasCollected)
        {
            if (item.itemInfo != null)
            {
                img.sprite = item.itemInfo.image;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Update()
    {
        Start();
    }
}
