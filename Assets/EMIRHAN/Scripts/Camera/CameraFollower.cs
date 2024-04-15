using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private float Speed = 1f;

    [SerializeField] bool Latency = true;

    private void Start()
    {
        gameObject.transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        if(player != null && player.playerDeath == false)
        {
            if(Latency == true)
            {
                FollowPlayerLate();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void FollowPlayerLate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position ,player.transform.position, Speed * Time.deltaTime);
    }

    void FollowPlayer()
    {
        gameObject.transform.position = player.transform.position;
    }
}
