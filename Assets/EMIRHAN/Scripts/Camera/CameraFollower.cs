using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private PlayerManager player;

    private void LateUpdate()
    {
        if(player.playerDeath == false)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        gameObject.transform.position = player.transform.position;
    }
}
