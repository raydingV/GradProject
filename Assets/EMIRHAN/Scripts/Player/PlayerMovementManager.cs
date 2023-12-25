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
    float dashTime;
    float elapsedTime;

    public GameObject DashEffect;
    GameObject DashObject;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = characterController.isGrounded;

        jumpPlayer();
        transformPlayer();
        dashPlayer();
        Gravity();

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.23f , gameObject.transform.position.z);
    }

    IEnumerator DashMovement(Vector3 playerDirection)
    {
        dashTime = dashDistance / dashForce;
        elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            characterController.Move(VectorFixInput(playerDirection) * dashForce * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        DashObject = GameObject.Instantiate(DashEffect);
        DashObject.transform.position = gameObject.transform.position;
    }

    Vector3 VectorFixInput(Vector3 InputPlayer)
    {
       Vector3 correctedMoveDirection = Camera.main.transform.TransformDirection(InputPlayer);

        correctedMoveDirection.y = 0;

        return correctedMoveDirection;
    }

    void transformPlayer()
    {
        controlPlayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(VectorFixInput(controlPlayer) * Time.deltaTime * playerSpeed);

        //characterController.Move(playerVelocity * Time.deltaTime);
    }

    void jumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer == true)
        {
            playerVelocity.y = 0f;

            playerVelocity.y += Mathf.Sqrt(jumpHeight);
        }
    }

    void dashPlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashObject = GameObject.Instantiate(DashEffect);
            DashObject.transform.position = gameObject.transform.position;
            StartCoroutine(DashMovement(controlPlayer));
            Debug.Log("Pressed");
        }
    }

    void Gravity()
    {
        if (groundedPlayer == false)
        {
            playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        }
    }
}
