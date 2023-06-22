using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneViews : MonoBehaviour
{
    public SmallSceneViews[] smallSceneViews;

    public ItemInfo[] requiredItem;
    public ItemInfo[] getItem;

    public ItemInfo specialItem1;
    public ItemInfo specialItem2;
    public ItemInfo specialItem3;
    public ItemInfo getSpecialItem1;
    public int currentViewIdx;
    public void Awake()
    {
        smallSceneViews = gameObject.GetComponentsInChildren<SmallSceneViews>();
        foreach (SmallSceneViews scene in smallSceneViews)
        {
            scene.gameObject.SetActive(false);
        }
        currentViewIdx = 0;
        smallSceneViews[currentViewIdx].gameObject.SetActive(true);
    }
}
