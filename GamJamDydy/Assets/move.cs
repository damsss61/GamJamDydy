using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
        transform.parent.GetComponentInChildren<Animator>().SetBool("isMooving", true);
    }

   
}
