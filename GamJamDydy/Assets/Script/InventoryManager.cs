using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items;
    public int capacity;
    public UnityEvent OnInventoryChanged;

    private void Start()
    {
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



}
