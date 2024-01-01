using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteranceLevelManager : MonoBehaviour
{
    public bool[] puzzleControl = new bool[9];
    int PutObjectValue = 0;
    bool checkOne = false;

    public bool puzzleDone = false;


    [Header("VFXObjects")]
    [SerializeField] ParticleSystem[] vfxFire;

    private void Start()
    {
        for (int i = 0; i < vfxFire.Length; i++)
        {
            vfxFire[i].Stop();
        }
    }

    void Update()
    {
        if (PutObjectValue == 9 && checkOne == false)
        {
            EnterancePuzzleOver();
            checkOne = true;
        }
    }

    void EnterancePuzzleOver()
    {
        for (int i = 0; i < vfxFire.Length; i++)
        {
            vfxFire[i].Play();
        }

        puzzleDone = true;
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
