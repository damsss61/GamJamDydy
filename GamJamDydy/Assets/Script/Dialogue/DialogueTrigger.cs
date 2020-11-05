using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue awareDialogue;
    public Dialogue unawareDialogue;

    public void TriggerDialogue()
    {
        if (GetComponent<NpcController>().awereness==NpcController.AIType.aware)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(awareDialogue, gameObject);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(unawareDialogue, gameObject);
        }
        
    }
}
