using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteranceLevelManager : MonoBehaviour
{
    public bool[] puzzleControl = new bool[7];
    int PutObjectValue = 0;
    bool checkOne = false;

    public bool puzzleDone = false;

    [SerializeField] Animator[] animatorController;
    [SerializeField] GameObject[] Rocks;
    [SerializeField] GameObject[] Collider;


    [Header("VFXObjects")]
    [SerializeField] GameObject[] vfxFire;

    //private void Awake()
    //{
    //    for (int i = 0; i < vfxFire.Length; i++)
    //    {
    //        vfxFire[i].Stop();
    //    }
    //}

    void Update()
    {
        if (PutObjectValue == 7 && checkOne == false && vfxFire != null && animatorController != null && animatorController != null && Collider != null)
        {
            EnterancePuzzleOver();
            SpawnRockPath();
            AnimationStart();
            ColliderEnterDisable();
            checkOne = true;
        }
    }

    void EnterancePuzzleOver()
    {
        for (int i = 0; i < vfxFire.Length; i++)
        {
            vfxFire[i].SetActive(true);
        }

        puzzleDone = true;
    }

    void AnimationStart()
    {
        for(int i = 0; i < animatorController.Length; i++)
        {
            animatorController[i].SetBool("StartAnim", true);
        }
    }

    void SpawnRockPath()
    {
        for(int i = 0; i < Rocks.Length; i++)
        {
            Rocks[i].SetActive(true);
        }
    }

    void ColliderEnterDisable()
    {
        for(int i = 0; i < Collider.Length; i++)
        {
            Collider[i].SetActive(false);
        }
    }

    public void checkPuzzle()
    {
        for(int i = 0;  i < puzzleControl.Length; i++)
        {
            PutObjectValue++;
            if (puzzleControl[i] == false)
            {
                PutObjectValue = 0;
                break;
            }
        }
    }
}
