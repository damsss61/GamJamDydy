using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<AI> persons;
    public List<AI> unawarePersons;
    public List<Transform> rooms;
    public bool onPause;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI timer;
    float timeSinceStart=0f;

    

    private void FixedUpdate()
    {
        //Spawn NPC
        UpdateCounter();
        UpdateTimer();
        if (unawarePersons.Count==0)
        {
            GameOver();
        }



    }

    void GameOver()
    {
        Debug.Log("Game Over");
        Pause();
        SceneManager.LoadScene(2);
    }
    void UpdateTimer()
    {
        if (!onPause)
        {
            timeSinceStart += Time.fixedDeltaTime;
            string minute = ((int)timeSinceStart / 60).ToString();
            string seconds = (timeSinceStart % 60).ToString("f0");
            string miliseconds = (((timeSinceStart % 60-(int)timeSinceStart % 60)*100)%99).ToString("f0");
            timer.text = minute + ":" + seconds + ":" + miliseconds;
        }
        

    }

    void UpdateCounter()
    {
        counter.text = (persons.Count - unawarePersons.Count) + " / " + persons.Count;
    }

    public void Restart()
    {
        onPause = false;
    }

    public void Pause()
    {
        onPause = true;
    }


    public void AddAI(AI ai)
    {
        persons.Add(ai);
    }

    public void AddUnawareAI(AI ai)
    {
        unawarePersons.Add(ai);
    }

    public void RemoveUnawareAI(AI ai)
    {
        unawarePersons.Remove(ai);
    }
}
