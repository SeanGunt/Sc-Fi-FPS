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
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource= GetComponent<AudioSource>();
        startPosition = this.transform.position;
        audioSource.clip = activateSound;
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

        if (isAngered)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            agent.speed = 10;
            agent.SetDestination(player.transform.position);
        }
        if (isPatrolling)
        {
            agent.speed = 5;
            agent.SetDestination(startPosition);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, lookRadius);
    }
}
