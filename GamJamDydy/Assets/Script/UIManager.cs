using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public InventoryManager playerInventory;
    public Button slot1;
    public Button slot2;

    int nbBille;

    

    public void UpdateInventoryUI()
    {
        TextMeshProUGUI textSlot1 = slot1.GetComponentInChildren<TextMeshProUGUI>();

        
        nbBille = 0;
        textSlot1.text = nbBille.ToString();
        slot1.image.sprite = null;
        slot2.image.sprite = null;

        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            if (playerInventory.items[i].GetName()=="Marble")
            {
                nbBille += 1;
                slot1.image.sprite = playerInventory.items[i].GetSprite();
            }

            if (playerInventory.items[i].GetName() == "Key")
            {
                slot2.image.sprite = playerInventory.items[i].GetSprite();
            }
        }
    }
}
