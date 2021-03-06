using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    Vector3 startPosition;
    public NavMeshAgent agent;
    public float lookRadius = 15f;
    bool isAngered;
    bool isPatrolling;
    public AudioClip activateSound;
    AudioSource audioSource;
    float stunnedTimer;
    float timeStunned = 2.5f;
    bool isStunned;
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public LayerMask whatIsGround;
    public SphereCollider sphereCollider;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource= GetComponent<AudioSource>();
        startPosition = this.transform.position;
        audioSource.clip = activateSound;
        stunnedTimer = timeStunned;
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance <= lookRadius)
        {
            isAngered = true;
            isPatrolling = false;
        }
        if (distance > lookRadius)
        {
            isAngered = false;
            isPatrolling = true;
        }

        if (isAngered && !isStunned)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            TreadsController.Instance.animator.speed = 2;
            agent.speed = 12;
            agent.SetDestination(player.transform.position);
        }
        if (isPatrolling && !isStunned)
        {
            TreadsController.Instance.animator.speed = 1;
            agent.speed = 7;
            Patroling();
        }
        if (isStunned)
        {
            sphereCollider.isTrigger = false;
            TreadsController.Instance.animator.speed = 0;
            agent.speed = 0;
            stunnedTimer -= Time.deltaTime;
            if (stunnedTimer < 0)
            {
                sphereCollider.isTrigger = true;
                stunnedTimer = timeStunned;
                isStunned = false;
            }
        }
    }
    void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 2.5f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    public void Stunned()
    {
        isStunned = true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, lookRadius);
        Gizmos.DrawWireSphere(this.transform.position, walkPointRange);
    }
}
