using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool Touchable = true;

    [SerializeField] private GameObject gameManagerObject;
    private GameManager gameManager;

    void Start()
    {
        if(Touchable == false)
        {
            gameManagerObject = GameObject.Find("GameManager");
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Touching(collision.collider.gameObject);
        }
    }

    void Touching(GameObject PlayerObject)
    {
        if(Touchable == false)
        {
            Debug.Log("You Dýed");
            Debug.Log(PlayerObject.name);
            gameManager.GetPlayerCheckPoint();
        }
    }
}
