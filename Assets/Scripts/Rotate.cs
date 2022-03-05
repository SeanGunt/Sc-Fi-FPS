using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationMultiplierX;
    public float rotationMultiplierY;
    public float rotationMultiplierZ;
    public void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + Time.deltaTime * rotationMultiplierX, 
                                            transform.rotation.eulerAngles.y + Time.deltaTime * rotationMultiplierY,
                                            transform.rotation.eulerAngles.z + Time.deltaTime * rotationMultiplierZ);
    }
}
