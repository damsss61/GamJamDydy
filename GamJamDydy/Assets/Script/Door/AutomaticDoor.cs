using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Transform open;
    public Transform close;
    private void Start()
    {
        open.gameObject.SetActive(false);
        close.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            open.gameObject.SetActive(true);
            close.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            open.gameObject.SetActive(false);
            close.gameObject.SetActive(true);
        }
    }
}
