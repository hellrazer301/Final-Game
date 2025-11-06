using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();
    
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    public bool isFull;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        isFull = false;

        PopulateSlotList();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f; //"pauses game ticks"
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f; //continues "game tick"
            isOpen = false;
        }
    }


    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }

    }

    public void AddToInventory(string itemName)
    {
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);

    }


    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 20) //Total item slots (wonder how to do the pouch inventory expansion of re2)
        {
            return true;
        }
        else
        {
            return false;
        }




    }

    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {

            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }
}