using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Marble : MonoBehaviour,Item
{
    public Sprite sprite;
    public string name;
    public GameObject marblePrefab;
    List<AI> SlowedNPC;
    public float speedFactor;

    public void Start()
    {
        SlowedNPC = new List<AI>();
    }
    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            AI NPCAI = other.GetComponent<AI>();
            NPCAI.GetComponent<NavMeshAgent>().speed *= speedFactor;
            SlowedNPC.Add(NPCAI);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            AI NPCAI = other.GetComponent<AI>();
            NPCAI.GetComponent<NavMeshAgent>().speed /= speedFactor;
            SlowedNPC.Remove(NPCAI);
        }
    }

    private void OnDestroy()
    {
        foreach (AI npc in SlowedNPC)
        {
            npc.GetComponent<NavMeshAgent>().speed /= speedFactor;
        }
    }

    public bool UseItem(RaycastHit hitInfo)
    {
        if (hitInfo.transform.CompareTag("Floor"))
        {
            
            Instantiate(marblePrefab, new Vector3(hitInfo.point.x, 1, hitInfo.point.z), Quaternion.identity);
            Debug.Log("Item utilisé");
            return true;
        }
        else
        {
            return false;
        }
    }
}
