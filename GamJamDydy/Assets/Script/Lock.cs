using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour,InteractableObject
{
    public Transform openDoor;
    public Transform closeDoor;
    AudioSource audio;

    private void Start()
    {
        closeDoor.gameObject.SetActive(false);
        openDoor.gameObject.SetActive(true);
        audio = transform.parent.GetComponent<AudioSource>();
    }

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
        closeDoor.gameObject.SetActive(true);
        openDoor.gameObject.SetActive(false);
        audio.Play();
    }

    public void ReleaseDoor()
    {
        Destroy(GetComponent<Key>());
        closeDoor.gameObject.SetActive(false);
        openDoor.gameObject.SetActive(true);
    }



}
