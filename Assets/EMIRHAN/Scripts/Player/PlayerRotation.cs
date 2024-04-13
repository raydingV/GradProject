using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotation : MonoBehaviour
{
    PlayerMovementManager _playerMovementManager;
    PlayerAttackManager _playerAttackManager;

    Quaternion targetRotation;

    [HideInInspector] public bool isRotating = false;

    [SerializeField] bool canFire = true;

    Ray _cameraRay;

    float InputValue;

    private void Awake()
    {
        _playerMovementManager = GetComponent<PlayerMovementManager>();
        _playerAttackManager = GetComponent<PlayerAttackManager>();
    }

    private void Update()
    {
        // if (Input.GetMouseButton(0) && canFire == true && _playerAttackManager.FireDelay <= 0)
        // {
        //     _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     isRotating = true;
        // }
        //
        // //Debug.Log("Distance: " + Quaternion.Angle(transform.rotation, targetRotation));
        //
        // if (isRotating == true)
        // {
        //     RotatePlayer(_cameraRay);
        // }
        //
        // if (_playerMovementManager != null)
        // {
        //     InputValue = (Mathf.Abs(_playerMovementManager.controlPlayer.x) + Mathf.Abs(_playerMovementManager.controlPlayer.z));
        //
        //     if (Quaternion.Angle(transform.rotation, targetRotation) <= 1f && InputValue > 0 && Input.GetMouseButton(0) == false)
        //     {
        //         isRotating = false;
        //     }
        // }

        if ((Input.GetMouseButton(0) && canFire == true && _playerAttackManager.FireDelay <= 0))
        {
            _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            isRotating = true;
        }

        if ((Input.inputString == "1" || Input.inputString == "2" || Input.inputString == "3"))
        {
            _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            isRotating = true;
        }
        
        if (isRotating == true)
         {
             rotatePlayerOld(_cameraRay);
         }
        
        if (_playerMovementManager != null)
        {
            InputValue = (Mathf.Abs(_playerMovementManager.controlPlayer.x) + Mathf.Abs(_playerMovementManager.controlPlayer.z));
        
            if (InputValue > 0 && Input.GetMouseButton(0) == false)
            {
                isRotating = false;
            }
        }
    }

    void RotatePlayer(Ray _cameraRay)
    {
        Ray cameraRay = _cameraRay;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlane.Raycast(_cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            Vector3 lookDirection = new Vector3(pointToLook.x - transform.position.x, 0f, pointToLook.z - transform.position.z);
            targetRotation = Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 80f * Time.deltaTime);
        }
    }
    
    void rotatePlayerOld(Ray _cameraRay)
    {

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(_cameraRay, out rayLength))
        {
            Vector3 pointToLook = _cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
