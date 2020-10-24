using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Item 
{
    bool UseItem(RaycastHit hitInfo);
    Sprite GetSprite();
    string GetName();

    
}
