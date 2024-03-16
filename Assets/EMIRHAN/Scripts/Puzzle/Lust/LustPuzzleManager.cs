using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustPuzzleManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject[] Rooms;
    [SerializeField] private int RoomValue = 0;
    [SerializeField] Vector3 CheckPoint;
    private bool fix = false;

    public void ChangeRoom(bool nextLevel)
    {
        if (nextLevel == true && fix == false)
        {
            RoomValue++;
        }
        else
        {
            RoomValue = 0;
        }

        roomManagement(RoomValue);
    }

    void roomManagement(int _RoomValue)
    {
        if (player != null)
        {
            StartCoroutine(GetCheckPoint());
        }
        
        for (int i = 0; i < Rooms.Length; i++)
        {
            Rooms[i].SetActive(false);
        }
        
        Rooms[_RoomValue].SetActive(true);
    }

    private IEnumerator GetCheckPoint()
    {
        player.InputEnable = false;
        player.transform.position = CheckPoint;
        yield return new WaitForSeconds(0.1f);
        player.InputEnable = true;
    }
}
