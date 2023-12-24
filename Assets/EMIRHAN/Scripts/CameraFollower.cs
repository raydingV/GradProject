using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        gameObject.transform.position = player.transform.position;
    }
}
