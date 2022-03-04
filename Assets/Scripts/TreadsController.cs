using UnityEngine;

public class TreadsController : MonoBehaviour
{
    public static TreadsController Instance { get; private set; }
    public Animator animator;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
