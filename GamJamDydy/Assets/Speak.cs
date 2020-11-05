using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speak : MonoBehaviour
{
    [TextArea(3,10)]
    public string sentence;

    void Start()
    {
        ChatBubble.Create(transform, new Vector3(0.5f, 0.5f), sentence, 7f);
        CharacterSound audio = transform.parent.GetComponentInChildren<CharacterSound>();
        audio.PlayBlabla();
    }

}
