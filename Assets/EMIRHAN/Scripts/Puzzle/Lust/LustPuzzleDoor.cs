using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustPuzzleDoor : MonoBehaviour
{
    [SerializeField] private LustPuzzleManager _puzzleManager;
    [SerializeField] private bool CorrectDoor;

    private void Awake()
    {
        _puzzleManager = GameObject.Find("PuzzleManager").GetComponent<LustPuzzleManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"));
        {
            RoomControl();
            Debug.Log("Enter");
        }
    }

    private void RoomControl()
    {
        if (_puzzleManager != null)
        {
            _puzzleManager.ChangeRoom(CorrectDoor);
        }
    }
}
