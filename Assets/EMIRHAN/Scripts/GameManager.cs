using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager _playerManager;
    [SerializeField] public BossManager _bossManager;
    [SerializeField] SkillsData skillData;

    [Header("Boss")]
    public bool UpToBoss = false;
    public bool DownToBoss = false;
    public bool DashBoss = false;
    public bool DownDamage = false;
    public bool DashDamage = false;
    public bool DashStart = false;

    private void Start()
    {
        GetSkillsData();
    }

    private void Update()
    {
        if(DownDamage == true)
        {
            BossHitBox();
        }
        else
        {
            BossHitBoxRegular();
        }
    }

    public void DefineTile(GameObject _gameObject)
    {
        if(Vector3.Distance(_playerManager.transform.position,_gameObject.transform.position) < 3f)
        {
            _playerManager.clickedObject = _gameObject;
            MoveToTýle();
        }
    }

    public void GetPlayerCheckPoint()
    {
        _playerManager.PlayerDead();
    }

    void MoveToTýle()
    {
        _playerManager.PlayerTransform();
    }

    void GetSkillsData()
    {
        skillData._gameManager = this;
    }

    void BossHitBox()
    {
        _bossManager.Box.size = new Vector3(3, 2, 3);
    }

    void BossHitBoxRegular()
    {
        _bossManager.Box.size = new Vector3(1, 2, 1);
    }

}
