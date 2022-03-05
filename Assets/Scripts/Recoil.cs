using UnityEngine;

public class Recoil : MonoBehaviour
{
    public static Recoil Instance { get; private set; }
    public Vector3 upRecoil;
    Vector3 originalRotation;
    float smooth;
    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        originalRotation = this.transform.eulerAngles;
    }
    public void AddRecoil()
    {
        transform.eulerAngles += upRecoil;
    }

    public void StopRecoil()
    {
        Vector3.Lerp(upRecoil, originalRotation, smooth * Time.deltaTime);
    }
}
