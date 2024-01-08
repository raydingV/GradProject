using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private float Speed = 30f;

    private void Start()
    {
        gameObject.transform.position = player.transform.position;
    }

    private void LateUpdate()
    {
        if(player != null && player.playerDeath == false)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        gameObject.transform.position = player.transform.position * Speed *Time.fixedDeltaTime;
    }
}
