using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    public float JumpForce;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;

    public CharacterController characterController;
    public float playerSpeed;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    public Animator playerAnimatorController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorController.SetFloat("VelocityWalkPlayer", playerInput.magnitude * playerSpeed);

        camDirection();
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * playerSpeed;

        characterController.transform.LookAt(characterController.transform.position + movePlayer);

        gravedad();
        Jump();
        characterController.Move(movePlayer * Time.deltaTime);

        Debug.Log(characterController.velocity.magnitude);
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void gravedad()
    {
        if (characterController.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetFloat("PlayerVerticalVelocity", characterController.velocity.y);
        }
        playerAnimatorController.SetBool("IsGrounded", characterController.isGrounded);
        SlideDown();
    }

    public void Jump()
    {
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = JumpForce;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetTrigger("SaltoPlayer");
        }
        SlideDown();
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= characterController.slopeLimit;
        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            movePlayer.y += slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
