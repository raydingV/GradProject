using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    void Start()
    {

    }

    void LateUpdate()
    {
        rotatePlayer();
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

}
