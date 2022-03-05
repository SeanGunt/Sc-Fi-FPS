using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationMultiplierX = 2;
    public float rotationMultiplierY;
    public float rotationMultiplierZ;
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + Time.deltaTime * rotationMultiplierX, 
                                            transform.rotation.eulerAngles.y + Time.deltaTime * rotationMultiplierY,
                                            transform.rotation.eulerAngles.z + Time.deltaTime * rotationMultiplierZ);
                                    
        Debug.Log(transform.eulerAngles);
    }
}
