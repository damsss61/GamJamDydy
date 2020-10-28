using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLoader : MonoBehaviour
{
    float timeSinceStart;
    TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        timeSinceStart = PlayerPrefs.GetFloat("Score");
        string minute = ((int)timeSinceStart / 60).ToString();
        string seconds = (timeSinceStart % 60).ToString("f0");
        string miliseconds = (((timeSinceStart % 60 - (int)timeSinceStart % 60) * 100) % 99).ToString("f0");
        text.text = minute + ":" + seconds + ":" + miliseconds;
    }

    
}
