using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [Header("Values")]
    public float Speed = 20f;
    float Timer = 6;
    float scaleSize = 1f;
    float scalePosition = 1f;

    [Header("Scripts")]
    GameManager _gameManager;

    [Header("Objects")]
    [SerializeField] private GameObject BoomEffect;
    [SerializeField] private GameObject AttackObject;
    GameObject BoomObject;

    [Header("Sound")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip triggerSound;

    Rigidbody rb;

    bool InýtializeFire = false;
    public bool StartFunc = false;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        Fire();

        ObjectDestroy();
    }

    void LateUpdate()
    {
        if(StartFunc == true && rb != null)
        {
            rb.AddForce(transform.forward * Speed * Time.deltaTime);
        }

        scalePosition = (gameObject.transform.localScale.x * 3 * Time.deltaTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, scalePosition, gameObject.transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boss" && StartFunc == true)
        {
            BoomObject = GameObject.Instantiate(BoomEffect);
            BoomObject.transform.position = gameObject.transform.position;
            _gameManager.audioSource.PlayOneShot(triggerSound);
            scaleSize = (gameObject.transform.localScale.x * 10/2);
            BoomObject.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);

            Destroy(gameObject);
        }
    }

    void Fire()
    {
        if (StartFunc == true && InýtializeFire == false)
        {
            _gameManager.audioSource.PlayOneShot(fireSound);
            AttackObject.SetActive(true);
            InýtializeFire = true;
        }
    }

    void ObjectDestroy()
    {
        if (StartFunc == true)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
