using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager playerManager;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DefineTile(GameObject _gameObject)
    {
        if(Vector3.Distance(playerManager.transform.position,_gameObject.transform.position) < 3f)
        {
            playerManager.clickedObject = _gameObject;
            MoveToTýle();
        }
    }

    public void GetPlayerCheckPoint()
    {
        playerManager.PlayerDead();
    }

    void MoveToTýle()
    {
        playerManager.PlayerTransform();
    }

}
