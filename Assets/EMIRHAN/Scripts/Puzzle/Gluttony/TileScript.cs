using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool Touchable = true;
    [SerializeField] private bool FirstTouch = false;
    [SerializeField] private bool FinalTouch = false;
    [SerializeField] private GameObject _highLight;
    private bool Clicked = false;

    [SerializeField] private GameObject puzzleManagerObject;
    private GluttonyPuzzleManager puzzleManager;

    private float timer = 0.1f;
    void Start()
    {
        puzzleManagerObject = GameObject.Find("PuzzleManager");
        puzzleManager = puzzleManagerObject.GetComponent<GluttonyPuzzleManager>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            setHighlight(false);
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.collider.CompareTag("Player"))
    //    {
    //        Touching(collision.collider.gameObject);
    //    }
    //}
    
    private void OnTriggerExit(Collider other)
    {
        setHighlight(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Touching(other.gameObject);
        }
        
        if (other.CompareTag("Highlight"))
        {
            setHighlight(true);
            timer = 0.1f;
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
            Debug.Log("You D�ed");
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

    void setHighlight(bool light)
    {
        if (_highLight.gameObject != null)
        {
            _highLight.gameObject.SetActive(light);
        }
    }

    private void OnMouseDown()
    {
        Clicked = true;
        puzzleManager.DefineTile(gameObject, FirstTouch);
        Debug.Log(gameObject.name);
    }
}
