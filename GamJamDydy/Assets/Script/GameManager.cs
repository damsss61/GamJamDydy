using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<NpcController> persons;
    public List<NpcController> unawarePersons;
    public List<Transform> rooms;
    public bool onPause;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI timer;
    float timeSinceStart=0f;
    public UnityEvent onDeathEvent;
    public List<int> spawnTimes;
    float eps = 0.1f;
    public Spawner spawner;
    

    private void FixedUpdate()
    {
        //Spawn NPC
        UpdateCounter();
        UpdateTimer();
        if (spawnTimes.Count != 0)
        {
            if (Mathf.Abs(timeSinceStart - spawnTimes[0]) < eps)
            {
                spawner.SpawnRandomNPC();
                spawnTimes.RemoveAt(0);
            }
        }
        


        if (unawarePersons.Count==0)
        {
            GameOver();
        }




    }

    void GameOver()
    {
        Debug.Log("Game Over");
        Pause();
        PlayerPrefs.SetFloat("Score", timeSinceStart);
        if (onDeathEvent!=null)
        {
            onDeathEvent.Invoke();
        }
        
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


    public void AddAI(NpcController ai)
    {
        persons.Add(ai);
    }

    public void AddUnawareAI(NpcController ai)
    {
        unawarePersons.Add(ai);
    }

    public void RemoveUnawareAI(NpcController ai)
    {
        unawarePersons.Remove(ai);
    }
}
