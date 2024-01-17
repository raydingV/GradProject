using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("Referances")]
    private CharacterController characterController;
    private Vector3 playerVelocity;

    PlayerManager playerManager;

    private bool groundedPlayer = false;
    [SerializeField] float angleSpeed;
    float yPosition;

    public Vector3 controlPlayer;

    [SerializeField] AudioClip DashSound;

    [Header("Walk")]
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] bool slowDownInFire = true;
    private float playerDefaultSpeed;
    private float InputValue;
    private bool bugFýx = false;

    [Header("Dash")]
    [SerializeField] float dashForce;
    [SerializeField] float dashDistance;
    float dashTime;
    float elapsedTime;
    [SerializeField] float DashDelaySecond = 1;
    float DashDelay = 0;

    public GameObject DashEffect;
    GameObject DashObject;

    [SerializeField] public bool InDashing = false;
    [SerializeField] bool GravityEnable = false;
    [SerializeField] bool DashEnable = true;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    private void Start()
    {
        yPosition = transform.position.y;
        playerDefaultSpeed = playerSpeed;
        playerVelocity.y = gameObject.transform.position.y;
    }

    void Update()
    {
        transformInput();

        slowDownSpeed();

        InputValue = (Mathf.Abs(controlPlayer.x) + Mathf.Abs(controlPlayer.z));

        DashDelay -= Time.deltaTime;

        if(DashEnable == true && DashDelay <= 0)
        {
            dashPlayer();
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, playerVelocity.y, gameObject.transform.position.z);
    }

    private void FixedUpdate()
    {
        transformPlayer();

        rotatePlayer();

        if (GravityEnable == true)
        {
            Gravity();
        }
    }

    IEnumerator DashMovement(Vector3 playerDirection)
    {
        dashTime = dashDistance / dashForce;
        elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            InDashing = true;
            characterController.Move(VectorFixInput(playerDirection) * dashForce * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        DashObject = GameObject.Instantiate(DashEffect);
        DashObject.transform.position = gameObject.transform.position;
        InDashing = false;
        DashDelay = DashDelaySecond;
    }

    Vector3 VectorFixInput(Vector3 InputPlayer)
    {
       Vector3 correctedMoveDirection = Camera.main.transform.TransformDirection(InputPlayer);

        correctedMoveDirection.y = 0;

        return correctedMoveDirection;
    }

    void transformInput()
    {
        controlPlayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void transformPlayer()
    {
        characterController.Move(VectorFixInput(controlPlayer) * Time.deltaTime * playerSpeed);
    }

    void rotatePlayer()
    {
        if(Input.GetMouseButton(0) == false && InputValue > 0)
        {
             Quaternion toRotation = Quaternion.LookRotation(VectorFixInput(controlPlayer), Vector3.up);
             transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 5 * Time.deltaTime);

             //characterController.Move(playerVelocity * Time.deltaTime);
        }
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
            playerManager._gameManager.audioSource.PlayOneShot(DashSound);
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

    void slowDownSpeed()
    {
        if (Input.GetMouseButtonDown(0) && slowDownInFire == true && bugFýx == false) 
        {
            playerSpeed = 6;
            bugFýx = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            playerSpeed = playerDefaultSpeed;
            bugFýx = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Plane")
        {
            groundedPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Plane")
        {
            groundedPlayer = false;
        }
    }
}
