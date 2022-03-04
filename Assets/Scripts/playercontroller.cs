using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  private CharacterController controller;
  public float groundDistance = 0.4f;
  public float jumpHeight = 1.75f;
  float speed = 7.5f;
  float gravity = -9.81f * 1.5f;
  float interactionDistance = 3f;
  float shootDistance = 100.0f;
  float maxStamina = 4f;
  float currentStamina;
  float chargeMax = 5f;
  float currentCharge;
  private AudioSource audioSource;
  public AudioClip firingSound;
  public AudioClip pickupSound;
  private bool isGrounded;
  private bool isSprinting;
  private bool canShoot;
  private bool keyCardGrabbed = false;
  private Vector3 velocity;
  public Transform groundCheck;
  public LayerMask groundMask;
  public Camera playerCamera;
  public Image staminaBarImage;
  public Image gunChargeImage;
  public GameObject impactEffect;
  public GameObject shell;
  public ParticleSystem muzzleFlash;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource= GetComponent<AudioSource>();
        currentStamina = maxStamina;
        currentCharge = chargeMax;
    }
    
    void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                SceneManager.LoadScene(2);
            }
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

        if (Input.GetButtonDown("Jump") && isGrounded && currentStamina > 0f && !PauseMenu.GameIsPaused)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            currentStamina = currentStamina - 0.25f;
            staminaBarImage.fillAmount = staminaBarImage.fillAmount - (0.25f/4f);
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
            speed = 9f;
        }
        else
        {
            speed = 7.5f;
        }

        if (!canShoot)
        {
            currentCharge = Mathf.Clamp(currentCharge += Time.deltaTime, 0.0f, chargeMax);
            Mathf.Clamp(gunChargeImage.fillAmount += Time.deltaTime/5, 0.0f, 1.0f);
        }

        if (currentCharge == chargeMax)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot && !PauseMenu.GameIsPaused)
            {
                Shoot();
                Recoil.Instance.AddRecoil();
            }
            else
            {
                Recoil.Instance.StopRecoil();
                return;
            }
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
                audioSource.volume = 1f;
                audioSource.PlayOneShot(pickupSound);
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

    void Shoot()
    {
        audioSource.volume = 0.25f;
        muzzleFlash.Play();
        gunChargeImage.fillAmount = 0.0f;
        audioSource.PlayOneShot(firingSound);
        currentCharge = 0.0f;
        RaycastHit shoot;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out shoot, shootDistance))
        {
            EnemyController enemyController = shoot.transform.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.Stunned();
            }
            
            GameObject impactGO = Instantiate(impactEffect, shoot.point, Quaternion.LookRotation(shoot.normal));
            GameObject shellGO = Instantiate(shell, shoot.point, Quaternion.LookRotation(Vector3.one, -shoot.normal));
            Destroy(impactGO, 2f);
            Destroy(shellGO, 10f);
        }
    }
}