using UnityEngine;

public class GunBob : MonoBehaviour
{
    float xIntensity = 0.015f;
    float yIntensity = 0.035f;
    public Transform gunHolder;
    private Vector3 gunHolderStartPos;
    float amplitude = 0.75f;
    
    void Awake()
    {
        gunHolderStartPos = gunHolder.localPosition;
    }
    void Update()
    {
        StartBob();
    }

    public void StartBob()
    {
        gunHolder.localPosition = new Vector3(Mathf.Cos(gunHolderStartPos.x += amplitude * Time.deltaTime) * xIntensity,
            Mathf.Sin(gunHolderStartPos.y += amplitude * Time.deltaTime) * yIntensity,
                gunHolderStartPos.z);
    }
}
