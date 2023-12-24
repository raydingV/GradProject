using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool InputEnable = true;

    public GameObject clickedObject;

    [Header("Components")]
    PlayerMovementManager _playerMovementManager;
    PlayerRotation _playerRotation;
    PlayerAttackManager _playerAttackManager;
    CharacterController _characterController;
    Rigidbody _characterRigidbody;

    void Start()
    {
        _playerMovementManager = gameObject.GetComponent<PlayerMovementManager>();
        _playerRotation = gameObject.GetComponent<PlayerRotation>();
        _playerAttackManager = gameObject.GetComponent<PlayerAttackManager>();
        _characterController = gameObject.GetComponent<CharacterController>();
        _characterRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        if (clickedObject != null)
        {
            PlayerTransform();
        }
    }

    public void PlayerDead()
    {
        clickedObject = null;
        StartCoroutine(GetCheckPoint());
    }

    public void PlayerTransform()
    {
        transform.position = Vector3.MoveTowards(transform.position, clickedObject.transform.position, 5f * Time.deltaTime);

        if(Vector3.Distance(clickedObject.transform.position, transform.position) < 0.5f)
        {
            clickedObject = null;
        }    
    }

    private IEnumerator GetCheckPoint()
    {
        transform.position = new Vector3(-4.53000021f, 1.37800002f, 2.08999991f);
        yield return new WaitForSeconds(0.01f);
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
}
