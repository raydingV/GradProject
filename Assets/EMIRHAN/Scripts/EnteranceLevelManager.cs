using System.Collections;
using TMPro;
using UnityEngine;

public class EnteranceLevelManager : MonoBehaviour
{
    public bool[] puzzleControl = new bool[7];
    int PutObjectValue = 0;
    bool checkOne = false;

    public bool tookGem = false;

    public bool puzzleDone = false;

    [HideInInspector] public AudioSource audioSource;
    [SerializeField] AudioClip fireOnSound;

    [SerializeField] GameObject RiddleUI;
    [SerializeField] GameObject BossNameUI;

    public TextMeshProUGUI textRiddle;
    public TextMeshProUGUI textBossName;
    [SerializeField] float typingSpeedRiddle = 0.03f;
    [SerializeField] float typingSpeedBossName = 0.3f;
    [SerializeField] bool TypeEffect = false;

    [SerializeField] Animator[] animatorController;
    [SerializeField] GameObject[] Rocks;
    [SerializeField] GameObject[] Collider;


    [Header("VFXObjects")]
    [SerializeField] GameObject[] vfxFire;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        RiddleOffScreen();
    }

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

        audioSource.PlayOneShot(fireOnSound);
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

    public void RiddleOnScreen(string riddle)
    {
        if(RiddleUI != null)
        {
            RiddleUI.SetActive(true);
        }

        if(TypeEffect == true)
        {
            StartCoroutine(TypeEffectRiddle(riddle));
        }
        else
        {
            textRiddle.text = riddle;
        }
    }

    public void RiddleOffScreen()
    {
        if (RiddleUI != null)
        {
            RiddleUI.SetActive(false);
        }
        textRiddle.text = null;
    }

    public void BossNameOnScreen(string bossName)
    {
        if (BossNameUI != null)
        {
            BossNameUI.SetActive(true);
        }

        textBossName.text = bossName;

        //if (TypeEffect == true)
        //{
        //    StartCoroutine(TypeEffectBossName(bossName));
        //}
        //else
        //{
        //    textBossName.text = bossName;
        //}
    }

    public void BossNameOffScreen()
    {
        if (BossNameUI != null)
        {
            BossNameUI.SetActive(false);
        }
        textBossName.text = null;
    }

    IEnumerator TypeEffectRiddle(string UIText)
    {
        foreach (char c in UIText)
        {
            textRiddle.text += c;
            yield return new WaitForSeconds(typingSpeedRiddle);

            if(textRiddle.text == null)
            {
                break;
            }
        }
    }
    IEnumerator TypeEffectBossName(string UIText)
    {
        foreach (char c in UIText)
        {
            textBossName.text += c;
            yield return new WaitForSeconds(typingSpeedBossName);
        }
    }
}
