using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, InteractableObject
{
    public bool openDoor;
    public void Interact(PlayerController player)
    {
        if (!openDoor)
        {
            GetComponentInParent<Lock>().Interact(player);
        }
    }

    public void LockDoor(Key key)
    {
        GetComponentInParent<Lock>().LockDoor(key);
    }

}
