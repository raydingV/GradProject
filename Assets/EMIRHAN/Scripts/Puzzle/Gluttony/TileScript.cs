using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool Touchable = true;
    [SerializeField] private bool FirstTouch = false;
    [SerializeField] private bool FinalTouch = false;
    private bool Clicked = false;

    [SerializeField] private GameObject puzzleManagerObject;
    private GluttonyPuzzleManager puzzleManager;

    void Start()
    {
        puzzleManagerObject = GameObject.Find("PuzzleManager");
        puzzleManager = puzzleManagerObject.GetComponent<GluttonyPuzzleManager>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleManager.InPuzzle = true;
        }
    }

    void Touching(GameObject PlayerObject)
    {
        if(Touchable == false && Clicked == true && puzzleManager._playerManager.tileInput == true)
        {
            Debug.Log("You Dýed");
            Debug.Log(PlayerObject.name);
            puzzleManager.GetPlayerCheckPoint();
            puzzleManager.InPuzzle = false;
            Clicked = false;
        }

        if (FinalTouch == true && puzzleManager._playerManager.tileInput == true)
        {
            puzzleManager.GetPlayerFinishPoint();
            Clicked = false;
        }
    }

    private void OnMouseDown()
    {
        Clicked = true;
        puzzleManager.DefineTile(gameObject, FirstTouch);
        Debug.Log(gameObject.name);
    }
}
