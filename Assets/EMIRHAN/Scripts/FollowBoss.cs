using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoss : MonoBehaviour
{
    BossManager boss;
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossManager>();
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(boss.transform.position.x, 0.1f, boss.transform.position.z);
    }
}
