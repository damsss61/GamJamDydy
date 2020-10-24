using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed=5f;
    public NavMeshAgent agent;
    Vector3 target;
    public delegate void OnMouseClickedDelegate();
    public event OnMouseClickedDelegate clickEvent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            Debug.Log(mousePoint);
            target = new Vector3(mousePoint.x, 0, mousePoint.z);
            agent.SetDestination(target);
            if (clickEvent!=null)
            {
                clickEvent();
            }

        }

    }

}
