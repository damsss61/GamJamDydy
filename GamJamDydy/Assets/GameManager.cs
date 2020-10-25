using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<AI> persons;
    public List<AI> unawarePersons;



    void Start()
    {
        
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
