using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Transform AnswerBox;
    public Transform ContinueBox;
    public Animator animator;
    public GameObject caller;
    int dialogueResponse;
    GameManager gameManager;

    Queue<Sentence> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
        gameManager= GetComponent<GameManager>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject _caller)
    {
        dialogueResponse = 0;
        caller = _caller;
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.calledName;
        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        gameManager.Pause();
        DisplayNextSentence();
    }
   
    public void SetDialogueResponse(int response)
    {
        dialogueResponse = response;
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));
        if (caller.GetComponentInChildren<CharacterSound>()!=null)
        {
            caller.GetComponentInChildren<CharacterSound>().PlayBlabla();
        }
        if (sentence.isQuestion)
        {
            AnswerBox.gameObject.SetActive(true);
            ContinueBox.gameObject.SetActive(false);
        }
        else
        {
            AnswerBox.gameObject.SetActive(false);
            ContinueBox.gameObject.SetActive(true);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public  void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        if (dialogueResponse!=0)
        {
            caller.GetComponent<NpcController>().DialogueResponse(dialogueResponse);
        }
        gameManager.Restart();

    }
}
