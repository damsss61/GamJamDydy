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
        Debug.Log(item.GetName());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!onObject) 
            {
                MouseDownOutside();
            }
                
        }
    }

    void OnMouseEnter()
    {
        onObject = true;
    }

    private void OnMouseExit()
    {
        onObject = false;
    }
   
    void MouseDownOutside()
    {
        selected = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && selected)
        {
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            inventory.AddItem(item);

            Destroy(gameObject);
        }
    }
}

    
