using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items;
    public int capacity;
    public UnityEvent OnInventoryChanged;
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        items = new List<Item>(capacity);
        if (OnInventoryChanged == null)
        {
            OnInventoryChanged = new UnityEvent();
        }
            
    }

    public void AddItem(Item item)
    {
        if (items.Count<capacity)
        {
            items.Add(item);
        }

        OnInventoryChanged.Invoke();
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetName() == item.GetName())
            {
                items.RemoveAt(i);
                OnInventoryChanged.Invoke();
                return;
            }
        }
    }



    public void UseItem(string item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetName()==item)
            {
                playerController.currentAction= PlayerController.playerAction.prepareItem;
                playerController.itemUsed = items[i];
                
            }
        }
    }



}
