using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed=5f;
    public NavMeshAgent agent;
    Vector3 target;
    Vector3 destination;
    public bool blockMouvement;
    bool hasReachedDestination;
    bool followTransform;
    Transform clickedTransform;
    InventoryManager inventory;
    public enum playerAction
    {
        idle,walk, talk, pick, useItem
    }
    public playerAction currentAction;
    

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<InventoryManager>();
        
    }

    public void ToogleMouvement()
    {
        blockMouvement = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                clickedTransform = hitInfo.transform;
                followTransform = false;
                //register target
                target = new Vector3(hitInfo.point.x, 1, hitInfo.point.z);
                Debug.Log(target);

                switch (hitInfo.transform.tag)
                {
                    case "Floor":
                        currentAction = playerAction.walk;
                        agent.stoppingDistance = 0f;
                        destination = target;
                        WalkToDestination();
                        break;

                    case "NPC":
                        currentAction = playerAction.talk;
                        agent.stoppingDistance = 2f;
                        followTransform = true;
                        // follow NPC and talk;
                        break;

                    case "Item":
                        currentAction = playerAction.pick;
                        agent.stoppingDistance = 2f;
                        followTransform = true;
                        break;

                    default:
                        break;
                }
            }
        }

        if (followTransform)
        {
            destination = new Vector3(clickedTransform.position.x, 1, clickedTransform.position.z);
            WalkToDestination();
        }

        CheckDestination();

        if (hasReachedDestination)
        {
            switch (currentAction)
            {
                case playerAction.walk:
                    currentAction = playerAction.idle;
                    break;
                case playerAction.talk:
                    Debug.Log("Talk");
                    currentAction = playerAction.idle;
                    followTransform = false;
                    break;
                case playerAction.pick:
                    clickedTransform.GetComponent<ClickableObject>().PickItem(inventory);
                    followTransform = false;
                    Debug.Log("Pick Item");
                    currentAction = playerAction.idle;
                    break;
                case playerAction.useItem:
                    break;
                default:
                    break;
            }

        }


    }


    void WalkToDestination()
    {
        agent.SetDestination(destination);
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (currentAction == playerAction.talk)
            {
                hasReachedDestination = true;
                agent.SetDestination(transform.position);
            }
        }
        
    }
}
