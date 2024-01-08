using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyPuzzleManager : MonoBehaviour
{
    [SerializeField] public PlayerGlutonyPuzzle _playerManager;

    public bool InPuzzle = false;

    [SerializeField] float distanceTile = 5.2f;

    [SerializeField] GameObject PortalVfx;

    public void DefineTile(GameObject _gameObject, bool IsF�rst)
    {
        float dist = Vector3.Distance(_playerManager.transform.position, _gameObject.transform.position);
        Debug.Log(dist);

        if ((Vector3.Distance(_playerManager.transform.position, _gameObject.transform.position) < distanceTile || (IsF�rst == true && InPuzzle == false)) && _playerManager.tileInput == true)
        {
            _playerManager.clickedObject = _gameObject;
            _playerManager.tileInput = false;
        }
    }

    public void GetPlayerCheckPoint()
    {
        _playerManager.PlayerDead();
    }

    public void GetPlayerFinishPoint()
    {
        _playerManager.FinishPuzzle();
        PortalVfx.SetActive(true);
    }
}
