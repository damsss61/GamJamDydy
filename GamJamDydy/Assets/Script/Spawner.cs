using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> npcs;
    

    public void SpawnRandomNPC()
    {
        GameObject npc = npcs[Random.Range(0, npcs.Count)];
        GameObject instatiatedNPC = Instantiate(npc, transform.position, transform.rotation, transform);
        npcs.Remove(npc);

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnRandomNPC();
        }
    }
}
