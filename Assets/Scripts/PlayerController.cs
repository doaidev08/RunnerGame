using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 move;
    public float forwardSpeed = 17f;
    public float maxSpeed = 70f;

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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;
    }
    void Update()
    {
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += Time.deltaTime * 0.4f;
        }


        if (PlayerManager.isGameStarted || PlayerManager.gameOver) return;

        animator.SetBool("isGameStarted", true);

        move.z = forwardSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, .3f, groundLayer);

        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (isGrounded)
        {
          
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());

            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            if (SwipeManager.swipeDown && !isSliding)
            {           
                StartCoroutine(Slide());
                velocity.y = -10;
            }
        }


        controller.Move(velocity * Time.deltaTime);

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if(transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if(moveDir.sqrMagnitude < diff.magnitude)
            {
                controller.Move(moveDir);
            }
            else
            {
                controller.Move(diff);
            }
        }


        controller.Move(move * Time.deltaTime);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }


    private void Jump()
    {
        StopCoroutine(Slide());
        animator.SetBool("isSliding", false);
        animator.SetTrigger("jump");
      /*  controller.center = Vector3.zero;*/
       /* controller.height = 2;*/
        isSliding = false;
   
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
        
        
        
        
    }
    private IEnumerator Slide()
    {
        isSliding = true;
        controller.center = new Vector3(0, 0.1f, 0);
        controller.height = .5F;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(.7f);
        animator.SetBool("isSliding", false);
        controller.center = new Vector3(0, 0.7f, 0);
        controller.height = 1.49f;
        isSliding = false;

    }
}
