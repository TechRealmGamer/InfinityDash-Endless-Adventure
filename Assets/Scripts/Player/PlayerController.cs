using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float dodgeSpeed = 10f;

    [Header("Lane Changes")]
    [SerializeField] private float laneDistance = 3.25f;
    private int targetLane = 1;
    private float targetXPosition;
    private float currentXPosition;
    [SerializeField] private AudioClip laneChangeSound;

    [Header("Jump")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float jumpForce = 10f;
    private float currentYPosition;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;

    [Header("Slide")]
    [SerializeField] private float slideTime = 1f;
    private float slideTimer = 0f;
    public bool isSliding = false;
    private float colliderCenterY;
    private float colliderHeight;
    [SerializeField] private AudioClip slideSound;

    [Header("Collision Audio Clips")]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip deathSound;

    // Components
    private PlayerInputManager playerInputManager;
    private CharacterController controller;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        Instance = this;

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInputManager = GetComponent<PlayerInputManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        colliderCenterY = controller.center.y;
        colliderHeight = controller.height;
    }
    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (playerInputManager.switchLane != 0)
            ChangeLane(playerInputManager.switchLane);

        currentXPosition = Mathf.Lerp(currentXPosition, targetXPosition, Time.deltaTime * dodgeSpeed);
        Vector3 moveVector = new Vector3(currentXPosition - transform.position.x, currentYPosition * Time.deltaTime, forwardSpeed*Time.deltaTime);
        controller.Move(moveVector);
        Jump();
        Slide();
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", controller.velocity.magnitude);
    }

    private void Jump()
    {
        if (controller.isGrounded && !isSliding)
        {
            if (playerInputManager.jump)
            {
                currentYPosition = jumpForce;
                animator.CrossFadeInFixedTime("Jump", 0.1f);
                audioSource.PlayOneShot(jumpSound);
            }
        }
        else
            currentYPosition -= jumpForce * 2 * Time.deltaTime;
        playerInputManager.jump = false;
    }

    private void Slide()
    {
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0)
        {
            slideTimer = 0f;
            isSliding = false;
            controller.height = colliderHeight;
            controller.center = new Vector3(0, colliderCenterY, 0);
        }
        if(playerInputManager.slide)
        {
            slideTimer = slideTime;
            currentYPosition = -10f;
            isSliding = true;
            controller.height = colliderHeight/2;
            controller.center = new Vector3(0, colliderCenterY/2, 0);
            animator.SetTrigger("Slide");
            AudioSource.PlayClipAtPoint(slideSound, transform.position);
        }
        playerInputManager.slide = false;
    }

    private void ChangeLane(int direction)
    {
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, 0, 2);

        if (targetLane == 0)
            targetXPosition = -laneDistance;
        else if (targetLane == 2)
            targetXPosition = laneDistance;
        else
            targetXPosition = 0;

        playerInputManager.switchLane = 0;
        audioSource.PlayOneShot(laneChangeSound);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Obstacle")
        {
            audioSource.PlayOneShot(deathSound);
            controller.enabled = false;
            animator.enabled = false;
            GetComponentInChildren<Rigidbody>().AddForce(hit.point * 100f);
            FindObjectOfType<LevelManager>().Invoke("RestartLevel", 4f);
            enabled = false;
        }
    }

    public void LandingSound()
    {
        audioSource.PlayOneShot(landSound);
    }

    public void Footstep()
    {
        audioSource.PlayOneShot(landSound);
    }
}
