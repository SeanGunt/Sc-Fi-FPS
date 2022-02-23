using UnityEngine;
using UnityEngine.AI;

public class enemycontroller : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public bool isAngered;
    public NavMeshAgent agent;

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance <= 15f)
        {
            isAngered = true;
        }
        if (distance > 15f)
        {
            isAngered = false;
        }

        if (isAngered)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        if (!isAngered)
        {
            agent.isStopped = true;
        }
    }
}
