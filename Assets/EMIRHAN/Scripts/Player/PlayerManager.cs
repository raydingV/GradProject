using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.SearchService;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool InputEnable = true;

    public bool playerDeath = false;

    [SerializeField] GameObject DashStartObject;
    GameObject VFXSkull;

    [SerializeField] private GameObject[] bloodVFX;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip deathSFX;

    [SerializeField] public GameObject holdObject;

    [SerializeField] Slider playerHealthSlider;
    public float playerHealth = 100;
    float playerLastHealth;

    [Header("Components")]
    PlayerMovementManager _playerMovementManager;
    PlayerRotation _playerRotation;
    PlayerAttackManager _playerAttackManager;
    CharacterController _characterController;
    Rigidbody _characterRigidbody;
    public GameManager _gameManager;

 

    [HideInInspector] public Vector3 DashStartTransform;

    private void Awake()
    {
        _playerMovementManager = gameObject.GetComponent<PlayerMovementManager>();
        _playerRotation = gameObject.GetComponent<PlayerRotation>();
        _playerAttackManager = gameObject.GetComponent<PlayerAttackManager>();
        _characterController = gameObject.GetComponent<CharacterController>();
        _characterRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerInput();

        DashStartTransform = new Vector3(transform.position.x, 4.5f, transform.position.z);

        playerLastHealth = playerHealth;
    }

    void Update()
    {
        PlayerInput();
        if(playerHealthSlider != null)
        {
            playerHealthSlider.value = playerHealth;
        }

        SkullVFX();

        if(VFXSkull != null)
        {
            VFXSkull.transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);
        }

        if(bloodVFX != null)
        {
            DamageVFX();
        }

        Death();
    }

    void PlayerInput()
    {
        if(InputEnable == false)
        {
            _characterController.enabled = false;
            _playerMovementManager.enabled = false;
            _playerAttackManager.enabled = false;
            _characterRigidbody.useGravity = true;
        }
        else
        {
            _characterController.enabled = true;
            _playerMovementManager.enabled = true;
            _playerAttackManager.enabled = true;
            _characterRigidbody.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boss" && _gameManager.DashDamage == true)
        {
            playerHealth -= 10;
        }

        if (other.tag == "Boss" && _gameManager.DownDamage == true)
        {
            playerHealth -= 10;
        }

        if(other.tag == "GluttonyEnter")
        {
            SceneManager.LoadScene("Assets/EMIRHAN/SCENES/GluttonyPuzzle.unity");
        }

        if (other.tag == "GluttonyBossEnter")
        {
            SceneManager.LoadScene("Assets/EMIRHAN/SCENES/Boss.unity");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Boss" && _gameManager.DownDamage == true)
        //{
        //    playerHealth -= 10;
        //}
    }

    private void SkullVFX()
    {
        if(_gameManager.DashStart == true)
        {
            VFXSkull = Instantiate(DashStartObject, DashStartTransform, Quaternion.Euler(0, 0, 0));
            _gameManager.DashStart = false;
        }
    }

    private void DamageVFX()
    {
        if(playerHealth < playerLastHealth)
        {
            GameObject.Instantiate(bloodVFX[0], transform.position, Quaternion.identity);
            GameObject.Instantiate(bloodVFX[1], transform.position, Quaternion.identity);
            playerLastHealth = playerHealth;
        }
    }

    private void Death()
    {
        if(playerHealth <= 0 && playerDeath == false)
        {
            playerDeath = true;
            _gameManager.audioSource.PlayOneShot(deathSFX);
            GameObject.Instantiate(deathVFX, transform.position, Quaternion.identity);
            DisablePlayer();
        }
    }

    private void DisablePlayer()
    {
        _playerMovementManager.enabled = false;
        _playerAttackManager.enabled = false;
        _playerRotation.enabled = false;

        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
