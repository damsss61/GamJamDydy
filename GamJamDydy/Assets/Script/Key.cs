using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Key : MonoBehaviour, Item
{
    public Sprite sprite;
    public string name;
    bool isUsing = false;

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    
    void Update()
    {
        if (isUsing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LockDoor();
            }
        }
    }

    public void UseItem()
    {
        isUsing = true;

    }


    void LockDoor()
    {


        Debug.Log("coucou");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            Debug.Log(hitInfo.transform.tag);
            if (hitInfo.transform.CompareTag("Door"))
            {
                if (hitInfo.transform.GetComponent<NavMeshObstacle>().enabled == false)
                {
                    hitInfo.transform.GetComponent<NavMeshObstacle>().enabled = true;
                }
                else
                {
                    hitInfo.transform.GetComponent<NavMeshObstacle>().enabled = false;
                }

            }

        }
    }
}
