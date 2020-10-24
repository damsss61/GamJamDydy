using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lock : MonoBehaviour,InteractableObject
{
    
    public void Interact(PlayerController player)
    {
        if (GetComponent<Item>() !=null)
        {
            player.GetComponent<InventoryManager>().AddItem(GetComponent<Item>());
            ReleaseDoor();
        }
        
    }

    public void LockDoor(Key key)
    {
        Key _key =gameObject.AddComponent<Key>();
        _key.name = key.name;
        _key.sprite = key.sprite;
        GetComponent<NavMeshObstacle>().enabled = true;
    }

    public void ReleaseDoor()
    {
        Destroy(GetComponent<Key>());
        GetComponent<NavMeshObstacle>().enabled = false;
    }



}
