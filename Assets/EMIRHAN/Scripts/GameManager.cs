using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GetPlayerCheckPoint()
    {
        playerManager.PlayerDead();
    }
}
