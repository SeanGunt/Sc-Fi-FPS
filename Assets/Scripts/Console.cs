using UnityEngine;

public class Console : MonoBehaviour
{
    float displayTime = 4f;
    float timerDisplay = -1.0f;
    public GameObject keyCardDialog;
    PlayerController playerController;

    void Awake()
    {
        keyCardDialog.SetActive(false);
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                keyCardDialog.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        keyCardDialog.SetActive(true);
    }
}
