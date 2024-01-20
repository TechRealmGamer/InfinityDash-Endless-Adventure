using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float dodgeSpeed = 10f;

    [Header("Lane Changes")]
    [SerializeField] private float laneDistance = 3.25f;
    private int targetLane = 1;
    private float targetXPosition;
    private float currentXPosition;

    [Header("Jump")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float jumpForce = 10f;
    private float currentYPosition;

    [Header("Slide")]
    [SerializeField] private float slideTime = 1f;
    private float slideTimer = 0f;
    private bool isSliding = false;
    private float colliderCenterY;
    private float colliderHeight;

    // Components
    private CharacterController controller;
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        colliderCenterY = controller.center.y;
        colliderHeight = controller.height;
    }
    private void Update()
    {
        isGrounded = controller.isGrounded;
        if(!isSliding)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                ChangeLane(-1);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                ChangeLane(1);
        }

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentYPosition = jumpForce;
                animator.SetTrigger("Jump");
            }
        }
        else
            currentYPosition -= jumpForce * 2 * Time.deltaTime;
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
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            slideTimer = slideTime;
            currentYPosition = -10f;
            isSliding = true;
            controller.height = colliderHeight/2;
            controller.center = new Vector3(0, colliderCenterY/2, 0);
            animator.SetTrigger("Slide");
        }
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
    }
}
