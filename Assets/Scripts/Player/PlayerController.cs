using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 move;
    public float forwardSpeed = 17f;
    private float maxSpeed = 70f; //Tốc độ tăng tốc tối đa

    private int desiredLane = 1;
    private float laneDistance = 2.5f;

    private bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private float gravity = -15f;
    private float jumpHeight = 1.4f;
    private Vector3 velocity;

    public Animator animator;
    private bool isSliding = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void FixedUpdate(){
        if(!PlayerManager.isGameStarted||PlayerManager.gameOver){
            return;
        }
        if (forwardSpeed < maxSpeed) //Nhân vật chạy nhanh dần theo thời gian
        {
            forwardSpeed += Time.deltaTime * 0.4f;
        }

    }
    void Update()
    {


        if (!PlayerManager.isGameStarted || PlayerManager.gameOver) return;

        animator.SetBool("isGameStarted", true);

        move.z = forwardSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, .3f, groundLayer); //Check tiếp xúc mặt đất

        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (isGrounded)
        {
          
            if (SwipeManager.swipeUp)
            {
                Jump(); //Nhảy
            }
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide()); //Trượt

            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            if (SwipeManager.swipeDown && !isSliding)
            {           
                StartCoroutine(Slide());
                velocity.y = -20; //Nhân vật rơi nhanh hơn khi vuốt xuống
            }
        }


        controller.Move(velocity * Time.deltaTime); 

        if (SwipeManager.swipeRight)
        {
            desiredLane++; //Chuyển sang làn bên phải
            if (desiredLane >= 2)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--; //Chuyển sang làn bên trái
            if (desiredLane <=0)
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
    //Nhân vật va chạm chướng ngại vật
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
        isSliding = false;
        StopCoroutine(Slide());
        animator.SetBool("isSliding", false);
        animator.SetTrigger("jump");
        velocity.y = Mathf.Sqrt(jumpHeight * 3 * -gravity);

    }
    private IEnumerator Slide()
    {
        isSliding = true;
        controller.center = new Vector3(0, -.8f, 0);
        controller.height = .5F;
        animator.SetBool("isSliding", true);

        yield return new WaitForSeconds(1f);
        animator.SetBool("isSliding", false);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2f;
        isSliding = false;

    }
}
