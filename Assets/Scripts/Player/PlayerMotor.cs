using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool IsGrounded;
    private bool Crouching=false;
    private bool LerpCrouch = false;
    private bool Sprinting =false;

    
    public float gravity = -9.8f;
    public float speed = 5f;
    public float JumpHeight = 3f;
    public float CrouchTimer = 1f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = controller.isGrounded;

        if (LerpCrouch)
        {
            CrouchTimer += Time.deltaTime;
            float p = CrouchTimer / 1;
            p *= p;
            if (Crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                LerpCrouch = false;
                CrouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 Input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = Input.x;
        moveDirection.z = Input.y;  
        controller.Move(transform.TransformDirection(moveDirection)* speed * Time.deltaTime);
        playerVelocity.y += gravity* Time.deltaTime;
        if (IsGrounded && playerVelocity.y <0 )
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity* Time.deltaTime);
        Debug.Log(playerVelocity.y);

    }

    public void jump()
    {
        if(IsGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        Crouching = !Crouching;
        CrouchTimer = 0;
        LerpCrouch = true;

    }

    public void Sprint()
    {
        Sprinting = !Sprinting;
        if (Sprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }
}
