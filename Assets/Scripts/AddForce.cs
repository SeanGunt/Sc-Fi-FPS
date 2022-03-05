using UnityEngine;

public class AddForce : MonoBehaviour
{   
    Rigidbody rb;
    float forceAmount = 3f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.up * forceAmount);
    }
}
