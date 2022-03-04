using UnityEngine;

public class Console : MonoBehaviour
{
    float displayTime = 4f;
    float timerDisplay = -1.0f;
    public GameObject keyCardDialog;
    PlayerController playerController;
    AudioSource audioSource;
    public AudioClip restrictedAudioClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(restrictedAudioClip);
        timerDisplay = displayTime;
        keyCardDialog.SetActive(true);
    }
}
