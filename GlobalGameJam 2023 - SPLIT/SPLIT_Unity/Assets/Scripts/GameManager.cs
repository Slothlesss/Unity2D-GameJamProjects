using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State { normal, zoom };

    
    public State currentState { get; set; }

    [Header("Scene")]
    public GameObject scenePool;
    public SceneViews[] sceneViews;

    public TextMeshProUGUI timelineText;
    public int currentViewIdx;
    int maxIdx = 3;

    [Header("InventorySlot")]
    public GameObject slotPool;
    public InventorySlot[] slots;
    public Item[] inventoryItems;

    public Toggle activeToggle;

    [Header("Camera")]
    public float zoomRatio = 0.3f;
    public Camera sceneCamera;
    public Vector3 orginalPos;


    public FarItem currentFarItem;
    public InteractablePlace currentPlace;

    static public GameManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        //Display Scene
        sceneViews = scenePool.GetComponentsInChildren<SceneViews>();
        foreach (SceneViews scene in sceneViews)
        {
            scene.gameObject.SetActive(false);
        }
        currentViewIdx = 1;
        DisplayTimeLine();
        sceneViews[currentViewIdx].gameObject.SetActive(true);
        //GetInventoryItems
        inventoryItems = slotPool.GetComponentsInChildren<Item>();
        foreach (Item item in inventoryItems)
        {
            item.gameObject.SetActive(false);
        }
        //GetSlot
        slots = slotPool.GetComponentsInChildren<InventorySlot>();
        //GetCameraPos
        orginalPos = sceneCamera.transform.position;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slotPool.GetComponent<ToggleGroup>().AnyTogglesOn() == true)
        {
            activeToggle = slotPool.GetComponent<ToggleGroup>().ActiveToggles().Single(i => i.isOn);
        }
    }

    public void DisplayTimeLine()
    {
        if (currentViewIdx == 0) timelineText.text = "Time Line: Past";
        else if (currentViewIdx == 1) timelineText.text = "Time Line: Present";
        else timelineText.text = "Time Line: Future";
    }
    public void ChangeScene()
    {
        ZoomOut();
        sceneViews[currentViewIdx].gameObject.SetActive(false);
        currentViewIdx += 1;
        if (currentViewIdx >= maxIdx) currentViewIdx = 0;
        DisplayTimeLine();
        sceneViews[currentViewIdx].gameObject.SetActive(true);
    }

    public void ZoomOut()
    {
        if (currentState == State.zoom)
        {
            if (currentFarItem != null)
            {
                currentFarItem.gameObject.SetActive(true);
                currentFarItem.item.gameObject.SetActive(false);
            }
            if(currentPlace != null)
            {
                currentPlace.GetComponent<Image>().raycastTarget = false;
            }
            sceneCamera.orthographicSize /= zoomRatio;
            sceneCamera.transform.position = orginalPos;
            currentState = State.normal;
        }
    }

    public void ZoomIn(ZoomableObject zo)
    {
        if (currentState == State.normal)
        {
            FarItem farItem = zo.GetComponent<FarItem>();
            if (farItem != null)
            {
                if (!farItem.item.hasCollected)
                {
                    currentFarItem = farItem;
                    farItem.item.gameObject.SetActive(true);
                    farItem.gameObject.SetActive(false);

                    sceneCamera.orthographicSize *= zoomRatio;
                    sceneCamera.transform.position = zo.transform.position + new Vector3(0, 0, -10);
                    currentState = State.zoom;
                }
            }
            InteractablePlace place = zo.GetComponent<InteractablePlace>();
            if(place != null)
            {
                if (!place.hasInteracted)
                {
                    currentPlace = place;
                    place.GetComponent<Image>().raycastTarget = true;
                    sceneCamera.orthographicSize *= zoomRatio;
                    sceneCamera.transform.position = zo.transform.position + new Vector3(0, 0, -10);
                    currentState = State.zoom;
                }
            }
        }
    }

    public void OnClickItem(Item item)
    {
        if(!item.hasCollected)
        {
            item.hasCollected = true;
            for(int i = 0; i < slots.Length; i++)
            {
                if(slots[i].isEmpty)
                {
                    inventoryItems[i].gameObject.SetActive(true);
                    inventoryItems[i].itemInfo = item.itemInfo;
                    slots[i].isEmpty = false;
                    break;
                }
            }
            if(item.specialItem == true)
            {
                item.removedObject.SetActive(false);
            }
            item.gameObject.SetActive(false);
        }
    }

    public void UseItem(InteractablePlace place)
    {
        if (currentState == State.zoom)
        {
            Debug.Log("Long");
            if (slotPool.GetComponent<ToggleGroup>().AnyTogglesOn() == true)
            {
                if (activeToggle.GetComponent<InventorySlot>().isEmpty == false)
                {
                    if (place.requiredItems != null)
                    {
                        if (activeToggle.GetComponentInChildren<Item>().itemInfo == place.requiredItems)
                        {
                            foreach (GameObject result in place.results)
                            {
                                result.SetActive(true);
                            }
                            foreach (GameObject removedObject in place.removedObjects)
                            {
                                removedObject.SetActive(false);
                            }
                            place.gameObject.SetActive(false);
                            place.hasInteracted = true;
                            activeToggle.GetComponent<InventorySlot>().isEmpty = true;
                            activeToggle.GetComponentInChildren<Item>().gameObject.SetActive(false);
                        }
                    }
                }
            }
            if (place.requiredItems == null)
            {
                foreach (GameObject result in place.results)
                {
                    result.SetActive(true);
                }
                foreach (GameObject removedObject in place.removedObjects)
                {
                    removedObject.SetActive(false);
                }
                place.gameObject.SetActive(false);
                place.hasInteracted = true;
            }
        }
    }
    public void UseItemOnTree(SceneViews tree)
    {
        if (currentState == State.normal)
        {
            if (slotPool.GetComponent<ToggleGroup>().AnyTogglesOn() == true)
            {
                if (activeToggle.GetComponent<InventorySlot>().isEmpty == false)
                {
                    if (tree.requiredItem[tree.currentViewIdx] != null)
                    {
                        if (activeToggle.GetComponentInChildren<Item>().itemInfo == tree.requiredItem[tree.currentViewIdx])
                        {
                            tree.smallSceneViews[tree.currentViewIdx].gameObject.SetActive(false);
                            tree.currentViewIdx += 1;
                            tree.smallSceneViews[tree.currentViewIdx].gameObject.SetActive(true);
                            activeToggle.GetComponent<InventorySlot>().isEmpty = true;
                            activeToggle.GetComponentInChildren<Item>().gameObject.SetActive(false);

                            //Add Item
                            for (int i = 0; i < slots.Length; i++)
                            {
                                if (slots[i].isEmpty)
                                {
                                    inventoryItems[i].gameObject.SetActive(true);
                                    inventoryItems[i].itemInfo = tree.getItem[tree.currentViewIdx];
                                    slots[i].isEmpty = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (activeToggle.GetComponentInChildren<Item>().itemInfo == tree.specialItem1)
                    {
                        activeToggle.GetComponent<InventorySlot>().isEmpty = true;
                        activeToggle.GetComponentInChildren<Item>().gameObject.SetActive(false);
                        activeToggle.GetComponent<Toggle>().isOn = false;
                        //Add Item
                        for (int i = 0; i < slots.Length; i++)
                        {
                            if (slots[i].isEmpty)
                            {
                                inventoryItems[i].gameObject.SetActive(true);
                                inventoryItems[i].itemInfo = tree.getSpecialItem1;
                                slots[i].isEmpty = false;
                                break;
                            }
                        }
                        
                    }
                    else if (activeToggle.GetComponentInChildren<Item>().itemInfo == tree.specialItem2)
                    {
                        SceneManager.LoadSceneAsync("Ending1");
                        MenuManager.Instance.data.finishEnding1 = true;
                    }
                    else if (activeToggle.GetComponentInChildren<Item>().itemInfo == tree.specialItem3)
                    {
                        SceneManager.LoadSceneAsync("Ending2");

                        MenuManager.Instance.data.finishEnding2 = true;
                    }
                }
            }
        }
    }

    public bool isInventoryContain(Item item)
    {
        foreach(Item inventoryItem in inventoryItems)
        {
            if(inventoryItem.itemInfo == item.itemInfo)
            {
                return true;
            }
        }
        return false;
    }
}
