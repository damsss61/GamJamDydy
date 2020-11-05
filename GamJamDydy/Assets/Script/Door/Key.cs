using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Key : MonoBehaviour, Item
{
    public Sprite sprite;
    public string name;

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public bool UseItem(RaycastHit hitInfo)
    {
        if (hitInfo.transform.CompareTag("Door"))
        {
            
            hitInfo.transform.GetComponent<Door>().LockDoor(this);
            return true;
        }
        else
        {
            return false;
        }
    }



}
