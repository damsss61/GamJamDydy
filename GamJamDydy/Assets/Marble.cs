using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour,Item
{
    public Sprite sprite;
    public string name;
    public GameObject marblePrefab;

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
