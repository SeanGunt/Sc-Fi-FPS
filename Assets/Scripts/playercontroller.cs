using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
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
    void Start()
    {
        controller = GetComponent<CharacterController>();

        audioSource= GetComponent<AudioSource>();
        audioSource.loop = true;
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
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
            speed = 11f;
        }
        else
        {
            speed = 7.5f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            {
                SceneManager.LoadScene(2);
            }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}