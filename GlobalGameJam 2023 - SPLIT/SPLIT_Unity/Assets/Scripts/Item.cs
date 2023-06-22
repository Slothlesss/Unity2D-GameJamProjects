using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    public bool hasCollected;
    public bool specialItem;
    public GameObject removedObject;
    public Image img;
    public void Start()
    {

        if (itemInfo != null)
        {
            img.sprite = itemInfo.image;
        }
    }
    public void Update()
    {
        Start();
    }
}
