using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager _playerManager;
    [SerializeField] public BossManager _bossManager;
    [SerializeField] SkillsData skillData;

    [HideInInspector] public AudioSource audioSource;

    [Header("Boss")]
    public bool UpToBoss = false;
    public bool DownToBoss = false;
    public bool DashBoss = false;
    public bool DownDamage = false;
    public bool DashDamage = false;
    public bool DashStart = false;

    bool GameOver = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetSkillsData();
    }

    private void Update()
    {
        if (_bossManager != null)
        {
            if (DownDamage == true)
            {
                BossHitBox();
            }
            else
            {
                BossHitBoxRegular();
            }
        }


        if(playerIsDead() && GameOver == false)
        {
            GameOver = true;
            StartCoroutine(loadNewScene());
        }
    }

    public void DefineTile(GameObject _gameObject)
    {
        float kaka = Vector3.Distance(_playerManager.transform.position, _gameObject.transform.position);
        Debug.Log(kaka);
        if (Vector3.Distance(_playerManager.transform.position,_gameObject.transform.position) < 4f)
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

    bool playerIsDead()
    {
        bool dead;
        if (_playerManager.playerDeath == false)
        {
            dead = false;
        }
        else
        {
            dead = true;
        }

        return dead;
    }

    IEnumerator loadNewScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Assets/EMIRHAN/SCENES/Boss.unity");
    }

}
