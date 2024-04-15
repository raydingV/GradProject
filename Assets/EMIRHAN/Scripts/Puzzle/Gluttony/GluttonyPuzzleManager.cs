using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyPuzzleManager : MonoBehaviour
{
    [SerializeField] public PlayerGlutonyPuzzle _playerManager;

    public bool InPuzzle = false;

    [SerializeField] float distanceTile = 5.2f;
    int lastRandomValue = -1;

    [SerializeField] GameObject PortalVFX;
    [SerializeField] GameObject FireWorkVFX;
    [SerializeField] GameObject[] Level;

    private void Start()
    {
        getNewLevel();
    }

    public void DefineTile(GameObject _gameObject, bool IsFýrst)
    {
        float dist = Vector3.Distance(_playerManager.transform.position, _gameObject.transform.position);
        Debug.Log(dist);

        if ((Vector3.Distance(_playerManager.transform.position, _gameObject.transform.position) < distanceTile || (IsFýrst == true && InPuzzle == false)) && _playerManager.tileInput == true)
        {
            _playerManager.clickedObject = _gameObject;
            _playerManager.tileInput = false;
        }
    }

    public void GetPlayerCheckPoint()
    {
        _playerManager.PlayerDead();
        getNewLevel();
    }

    public void GetPlayerFinishPoint()
    {
        _playerManager.FinishPuzzle();
        PortalVFX.SetActive(true);
        FireWorkVFX.SetActive(true);
    }

    void getNewLevel()
    {
        for (int i = 0; i < Level.Length; i++)
        {
            Level[i].SetActive(false);
        }

        Level[GetRandomValue(0, Level.Length)].SetActive(true);
    }

    int GetRandomValue(int Min, int Max)
    {
        int randomResult = 0;

        do
        {
            randomResult = Random.Range(Min, Max);
        } while (randomResult == lastRandomValue);

        lastRandomValue = randomResult;

        return randomResult;
    }
}
