using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 move;
    public float forwardSpeed = 17f;
    public float maxSpeed;

    private int desiredLane = 1;
    public float laneDistance = 2.5f;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float gravity = -12f;
    public float jumpHeight = 2f;
    private Vector3 velocity;

    public Animator animator;
    private bool isSliding = false;
    public float slideDuration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        if (isGrounded)
        {
            Debug.Log("helelo");
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        move.z = forwardSpeed;
        controller.Move(move * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("isGameStarted", true);

    }
}
