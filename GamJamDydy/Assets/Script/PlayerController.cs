using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    NavMeshAgent agent;
    Vector3 target;
    Vector3 destination;
    public bool blockMouvement;
    bool hasReachedDestination;
    bool followTransform;
    Transform clickedTransform;
    InventoryManager inventory;
    public Item itemUsed;
    RaycastHit hitInfo;
    public Animator animator;
    Rigidbody rb;
    Vector3 previous;
    Vector3 velocity;
    List<Vector3> velocities;
    float speed;
    Vector3 velocityMoy;

    public enum playerAction
    {
        idle,walk, talk, pick, useItem, interact, prepareItem
    }
    public playerAction currentAction;
    

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<InventoryManager>();
        rb = GetComponent<Rigidbody>();
        previous = transform.position;
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
            
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                clickedTransform = hitInfo.transform;
                followTransform = false;
                //register target
                target = new Vector3(hitInfo.point.x, 1, hitInfo.point.z);


                if (currentAction==playerAction.prepareItem)
                {
                    destination = target;
                    WalkToDestination();
                    agent.stoppingDistance = 2f;
                    currentAction = playerAction.useItem;
                    hasReachedDestination = false;
                    UseItem(hitInfo);
                    return;
                }
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
                        agent.stoppingDistance = 1f;
                        followTransform = true;
                        // follow NPC and talk;
                        break;

                    case "Item":
                        currentAction = playerAction.pick;
                        agent.stoppingDistance = 1f;
                        followTransform = true;
                        break;

                    case "Door":
                        currentAction = playerAction.interact;
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

                case playerAction.interact:
                    clickedTransform.GetComponent<InteractableObject>().Interact(this);
                    followTransform = false;
                    Debug.Log("Item Activated");
                    currentAction = playerAction.idle;
                    break;

                case playerAction.prepareItem:
                    Debug.Log("Prepare Item");
                    break;

                case playerAction.useItem:
                    UseItem(hitInfo);
                    break;
                default:
                    break;
            }

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

    public void UseItem(RaycastHit hitInfo)
    {
        if (hasReachedDestination)
        {
            if (itemUsed.UseItem(hitInfo))
            {
                currentAction = playerAction.idle;
                inventory.RemoveItem(itemUsed);
                itemUsed = null;
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
        if (other.transform == hitInfo.transform)
        {
            if (!other.CompareTag("Floor"))
            {
                hasReachedDestination = true;
                agent.SetDestination(transform.position);
                followTransform = false;
            }

        }

    }
}
