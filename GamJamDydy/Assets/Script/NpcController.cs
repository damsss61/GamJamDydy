using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public enum AIType { aware, unaware }
    public AIType awereness;
    public string npcName;
    GameManager gameManager;
    Transform targetTransform;
    NavMeshAgent agent;
    Vector3 previous;
    Vector3 velocity;
    float speed;
    Animator animator;
    bool hasReachedDestination = false;
    Vector3 destination;
    public int changeRoomTime = 10000;
    public int changeTargetTime = 10000;
    bool isMooving;
    bool isSlowed;


    private void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        gameManager.AddAI(this);
        if (awereness == AIType.unaware)
        {
            gameManager.AddUnawareAI(this);
            GetRoom();
        }
        

    }


    private void Update()
    {


        if (targetTransform == null)
        {
            FindTarget();
        }

        RandomlyChangeTarget();

        WalkToDestination();

        CheckDestination();

        if (hasReachedDestination)
        {
            targetTransform = null;
        }


        UpdateAnimator();
    }

    void ExecuteAction()
    {
        if (awereness == AIType.aware)
        {
            ShareSecret();
            FindTarget();
        }
        else
        {
            FindRoom();
        }
    }

    void RandomlyChangeTarget()
    {
        if (awereness == AIType.aware)
        {
            if (Random.Range(0, changeTargetTime) == 0)
            {
                targetTransform = ClosestTarget();
                ShareThought();
            }
        }
    }


    public Transform ClosestTarget()
    {
        float sqrDist = float.MaxValue;
        Transform target = targetTransform;
        foreach (NpcController unawareNPC in gameManager.unawarePersons)
        {
            if (Vector3.SqrMagnitude(unawareNPC.transform.position-transform.position)<sqrDist)
            {
                sqrDist = Vector3.SqrMagnitude(unawareNPC.transform.position - transform.position);
                target = unawareNPC.transform;
            }
        }

        return target;
    }


    public void DialogueResponse(int roomIndex)
    {
        if (awereness == AIType.unaware)
        {
            targetTransform = gameManager.rooms[roomIndex - 1];
            destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
            WalkToDestination();
        }
    }

    public void GetRoom()
    {
        int index = Random.Range(0, gameManager.rooms.Count);
        targetTransform = gameManager.rooms[index];
        destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
    }
    void FindRoom()
    {
        if (Random.Range(0, changeRoomTime) == 0)
        {
            int index = Random.Range(0, gameManager.rooms.Count);
            targetTransform = gameManager.rooms[index];
            destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
            ShareThought();
        }
        else
        {
            targetTransform = null;
            destination = transform.position;
        }
    }

    void FindTarget()
    {
        if (awereness == AIType.aware)
        {
            if (gameManager.unawarePersons.Count > 0)
            {
                int index = Random.Range(0, gameManager.unawarePersons.Count);
                targetTransform = gameManager.unawarePersons[index].transform;
                ShareThought();
                destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
            }
            else
            {
                hasReachedDestination = true;
                agent.SetDestination(transform.position);
                targetTransform = null;
            }

        }
        else
        {
            FindRoom();
        }

    }

    void WalkToDestination()
    {
        if (!gameManager.onPause)
        {
            if (targetTransform != null)
            {
                destination = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
                agent.SetDestination(destination);
                isMooving = true;
            }
            else
            {
                destination = transform.position;
                isMooving = false;
            }
            agent.isStopped = false;
            agent.SetDestination(destination);

        }
        else
        {
            agent.isStopped = true;
            isMooving = false;
        }

    }


    void UpdateAnimator()
    {
        float horizonal = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180);
        float vertical = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180);
        animator.SetBool("isMooving", isMooving);
        animator.SetFloat("horizontal", horizonal);
        animator.SetFloat("vertical", vertical);
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
        targetTransform.GetComponent<NpcController>().GetAware();

        gameManager.RemoveUnawareAI(targetTransform.GetComponent<NpcController>());
    }

    void ShareThought()
    {
        if (awereness == AIType.aware)
        {
            string sentence = "I'm gonna tell " + targetTransform.GetComponent<NpcController>().npcName + " !";
            ChatBubble.Create(transform.GetChild(0), new Vector3(0.5f, 0.5f), sentence, 4f);
        }
        else
        {
            string sentence = "I'm going to the " + targetTransform.name + " !";
            ChatBubble.Create(transform.GetChild(0), new Vector3(0.5f, 0.5f), sentence, 4f);
        }
    }

    public void GetAware()
    {
        if (awereness == AIType.unaware)
        {
            awereness = AIType.aware;
            GetRoom();
            ChatBubble.Create(transform.GetChild(0), new Vector3(0.5f, 0.5f), "I can't believe it !", 4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("NPC"))
        {
            if (awereness == AIType.aware)
            {

                if (other.transform == targetTransform)
                {
                    ExecuteAction();


                }
            }

        }


    }


}
