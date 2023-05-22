using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Item", menuName = "Item")]
public class ItemInfo : ScriptableObject
{
    public Sprite image; 
    public string itemName;
    [TextArea(3, 3)]
    public string itemDescription;
    public bool countable;
}
