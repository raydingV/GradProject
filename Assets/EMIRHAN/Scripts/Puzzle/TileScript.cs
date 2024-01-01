using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool Touchable = true;
    private bool Clicked = false;

    [SerializeField] private GameObject gameManagerObject;
    private GameManager gameManager;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.collider.CompareTag("Player"))
    //    {
    //        Touching(collision.collider.gameObject);
    //    }
    //}


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Touching(other.gameObject);
        }
    }

    void Touching(GameObject PlayerObject)
    {
        if(Touchable == false && Clicked == true)
        {
            Debug.Log("You Dýed");
            Debug.Log(PlayerObject.name);
            gameManager.GetPlayerCheckPoint();
            Clicked = false;
        }
    }

    private void OnMouseDown()
    {
        Clicked = true;
        gameManager.DefineTile(gameObject);
        Debug.Log(gameObject.name);
    }
}
