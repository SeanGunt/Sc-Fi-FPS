using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  CharacterController controller;
  float speed = 7.5f;
  Vector3 velocity;
  float gravity = -9.81f * 1.5f;
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  private bool isGrounded;
  public float jumpHeight = 1.5f;
  bool isSprinting;
  public AudioClip winSound;
  public AudioClip loseSound;
  public AudioClip firingSound;
  public AudioClip bgmusicSound;
  AudioSource audioSource;
  public Camera playerCamera;
  float interactionDistance = 3f;
  bool keyCardGrabbed = false;
  float maxStamina = 4f;
  float currentStamina;
  public Image staminaBarImage;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource= GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = bgmusicSound;
        audioSource.Play();
        currentStamina = maxStamina;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            currentStamina = Mathf.Clamp(currentStamina -= Time.deltaTime, 0.0f, maxStamina);
            Mathf.Clamp(staminaBarImage.fillAmount -= Time.deltaTime/4, 0.0f, 1.0f);
        }
        else
        {
            isSprinting = false;
            currentStamina = Mathf.Clamp(currentStamina += Time.deltaTime, 0.0f, maxStamina);
            Mathf.Clamp(staminaBarImage.fillAmount += Time.deltaTime/4, 0.0f, 1.0f);
        }

        if (currentStamina > 0.0f && isSprinting)
        {
            speed = 11f;
        }
        else
        {
            speed = 7.5f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            KeyCard keyCard = hit.transform.GetComponent<KeyCard>();
            if (keyCard != null)
            {
                keyCard.Remove();
                keyCardGrabbed = true;
            }

            Console console = hit.transform.GetComponent<Console>();
            if (console != null && !keyCardGrabbed)
            {
                console.DisplayDialog();
            }
            if (console != null && keyCardGrabbed)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
     void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                SceneManager.LoadScene(2);
            }
    }
}