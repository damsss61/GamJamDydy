using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    private bool selected = false;
    Item item;
    private bool onObject = false;



    private void OnMouseDown()
    {
        selected = true;
    }

    private void Start()
    {

        item = GetComponent<Item>();
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && selected)
        {
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            
        }
    }

    public void PickItem(InventoryManager inventory)
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }
}

    
