using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed=5f;
    public NavMeshAgent agent;
    Vector3 target;

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
            

        }
        //transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

    }
}
