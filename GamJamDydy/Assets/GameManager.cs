using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<AI> persons;
    public List<AI> unawarePersons;
    public List<Transform> rooms;
    public bool onPause;
    public TextMeshProUGUI counter;

    

    private void FixedUpdate()
    {
        //Spawn NPC
        UpdateCounter();

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
