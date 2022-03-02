using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        Vector3 direction = (this.transform.position - playerController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
