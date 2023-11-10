using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("Referances")]
    private CharacterController characterController;
    private Vector3 playerVelocity;


    private bool groundedPlayer;
    private float playerAngle;
    public float angleSpeed;

    private Vector3 controlPlayer;

   [Header("Walk")]
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    [Header("Dash")]
    public float dashForce;
    public float dashDistance;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = characterController.isGrounded;

        movePlayer();
        rotatePlayer();
        jumpPlayer();
        dashPlayer();
    }

    IEnumerator DashMovement(Vector3 playerDirection)
    {
        float dashTime = dashDistance / dashForce;
        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            characterController.Move(playerDirection * dashForce * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void movePlayer()
    {

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        controlPlayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(controlPlayer * Time.deltaTime * playerSpeed);

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void rotatePlayer()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void jumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && groundedPlayer!)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
    }

    void dashPlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(DashMovement(controlPlayer));
            Debug.Log("Pressed");
        }
    }
}
