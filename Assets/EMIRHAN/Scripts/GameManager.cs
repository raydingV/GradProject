using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager _playerManager;
    [SerializeField] public BossManager _bossManager;
    [SerializeField] SkillsData skillData;

    [HideInInspector] public AudioSource audioSource;

    [SerializeField] bool fpsLimit = false;
    [SerializeField] int FPS = 60;

    [Header("Boss")]
    public bool UpToBoss = false;
    public bool DownToBoss = false;
    public bool DashBoss = false;
    public bool DownDamage = false;
    public bool DashDamage = false;
    public bool DashStart = false;
    public bool InHitSequence = false;
    public bool BossDeath = false;

    bool GameOver = false;

    public bool FadeObjects = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetSkillsData();
    }

    private void Update()
    {

        if (fpsLimit)
        {
            Application.targetFrameRate = FPS;
        }

        //if (_bossManager != null)
        //{
        //    if (DownDamage == true)
        //    {
        //        BossHitBox();
        //    }
        //    else
        //    {
        //        BossHitBoxRegular();
        //    }
        //}


        if (playerIsDead() && GameOver == false)
        {
            GameOver = true;
            StartCoroutine(loadNewScene());
        }

        if(_bossManager != null && _bossManager.Health <= 0 && GameOver == false)
        {
            GameOver = true;
            //StartCoroutine(loadGame());
        }
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
        SceneManager.LoadScene("Assets/EMIRHAN/SCENES/BossLastImplement.unity");
    }

    IEnumerator loadGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Assets/EMIRHAN/SCENES/EnteranceImplement.unity");
    }

}
