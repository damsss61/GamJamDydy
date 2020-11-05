using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro text;
    [SerializeField]
    public GameObject chatBubblePrefab;

    public static void Create(Transform parent, Vector3 localPosition, string text, float duration)
    {
        GameObject chatBubblePrefab = (GameObject)Resources.Load("chatBubble");
        GameObject chatBubbleTransform = Instantiate(chatBubblePrefab, parent);
        chatBubbleTransform.transform.localPosition = localPosition;
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);


        Destroy(chatBubbleTransform, duration);
    }

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        text = transform.Find("Text").GetComponent<TextMeshPro>();

    }

    private void Start()
    {
        
    }

    private void Setup(string _text)
    {
        text.text = _text;
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues();
        Vector2 offset = new Vector2(0.1f, 0.1f);
        backgroundSpriteRenderer.size = textSize + offset;
        backgroundSpriteRenderer.transform.localPosition = new Vector3(textSize.x / 2f, 0);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_text));
        text.ForceMeshUpdate();

    }


    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
