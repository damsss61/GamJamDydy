using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public enum AIType { aware, unaware }
    public AIType awereness;
    GameManager gameManager;
    Transform targetTransform;
    NavMeshAgent agent;
    Vector3 previous;
    Vector3 velocity;
    float speed;
    Animator animator;
    bool hasReachedDestination = false;
    Vector3 destination;
    public int changeRoomTime=10000;


    private void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        if (awereness == AIType.unaware)
        {
            gameManager.AddUnawareAI(this);
        }

    }
    

    void FindRoom()
    {
        if (Random.Range(0,changeRoomTime)==0)
        {
            int index = Random.Range(0,gameManager.rooms.Count);
            targetTransform = gameManager.rooms[index];
            destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
            WalkToDestination();
        }
    }

    void FindTarget()
    {
        if (awereness == AIType.aware)
        {
            if (gameManager.unawarePersons.Count>0)
            {
                int index = Random.Range(0, gameManager.unawarePersons.Count);
                targetTransform = gameManager.unawarePersons[index].transform;
                destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
                WalkToDestination();
            }
            else
            {
                hasReachedDestination = true;
                agent.SetDestination(transform.position);
            }
            
        }

    }

    void WalkToDestination()
    {
        agent.SetDestination(destination);
    }

    private void Update()
    {
        if (targetTransform == null)
        {
            if (awereness == AIType.aware)
            {

                FindTarget();

            }
            
        }
        

        CheckDestination();

        WalkToDestination();

        if (awereness == AIType.aware)
        {

            if (hasReachedDestination)
            {
                ShareSecret();
                FindTarget();
            }
            destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
        }
        else
        {
            FindRoom();
        }

    }

    void CheckDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    hasReachedDestination = true;
                }
                else
                {
                    hasReachedDestination = false;
                }
            }
            else
            {
                hasReachedDestination = false;
            }
        }
        else
        {
            hasReachedDestination = false;
        }
    }

    void ShareSecret()
    {
        targetTransform.GetComponent<AI>().GetAware();

        gameManager.RemoveUnawareAI(targetTransform.GetComponent<AI>());
    }

    public void GetAware()
    {
        if (awereness == AIType.unaware)
        {
            awereness = AIType.aware;
            FindTarget();
        }
    }
    private void FixedUpdate()
    {
        float horizonal = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180);
        float vertical = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180);
        animator.SetBool("isMooving", !hasReachedDestination);
        animator.SetFloat("horizontal", horizonal);
        animator.SetFloat("vertical", vertical);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Floor"))
        {
            if (awereness == AIType.aware)
            {

                if (other.transform == targetTransform)
                {
                    hasReachedDestination = true;
                    agent.SetDestination(transform.position);


                }
            }

        }




    }

}
