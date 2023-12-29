using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider slider;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] PlayerManager player;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public BoxCollider Box;

    [Header("Scripts")]
    [SerializeField] GameManager _gameManager;
    [SerializeField] BossAnimation _bossAnimation;

    [Header("Data")]
    [SerializeField] BossData BossData;
    [SerializeField] SkillsData SkillObjects;

    [Header("Variables")]
    [HideInInspector] public float Health;
    [HideInInspector] public float Damage;

    [HideInInspector] public bool InCombat = false;

    Vector3 locat;
    Vector3 Target;

    int skillTime;
    int lastRandomValue = 0;

    List<IEnumerator> allSkills = new List<IEnumerator>();

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        BossDataTake();
        SkillDataTake();
        slider.maxValue = Health;
        StartCoroutine(Skills());
    }

    void Update()
    {
        slider.value = Health;

        if (player != null)
        {
            locat = new Vector3(player.transform.position.x + GetRandomValue(), 0, player.transform.position.z + GetRandomValue());
        }
    }

    private void FixedUpdate()
    {
        if(InCombat == false && Health > 0)
        {
            agent.SetDestination(locat);
        }

        if (_gameManager.UpToBoss == true && player != null)
        {
            MoveUp();
        }

        if (_gameManager.DownToBoss == true && player != null)
        {
            MoveDown();
        }

        float speedBoss = Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z);
        _bossAnimation.floatParameter("Speed", speedBoss);
        _bossAnimation.floatParameter("Health", Health);
    }

    private void LateUpdate()
    {
        if(_gameManager.DashBoss == true && player != null)
        {
            MoveDash();
        }
        else
        {
            if (player != null && Health > 0)
            {
                rotationBoss();
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Magic")
        {
            Health -= (other.transform.localScale.x * 50);
            Debug.Log("HÝT!");
        }
    }

    IEnumerator Skills()
    {
        yield return new WaitForSeconds(Random.Range(6,10));

        InCombat = true;
        

        switch(GetRandomSkillValue())
        {
            case 1:
                agent.ResetPath();
                Debug.Log("Case 1");
                StartCoroutine(allSkills[0]);
                yield return new WaitForSeconds(skillTime);
                InCombat = false;
                break;
            case 2:
                agent.ResetPath();
                Debug.Log("Case 2");
                StartCoroutine(allSkills[1]);
                yield return new WaitForSeconds(skillTime);
                InCombat = false;
                break;
            case 3:
                agent.ResetPath();
                Debug.Log("Case 3");
                StartCoroutine(allSkills[2]);
                yield return new WaitForSeconds(skillTime);
                InCombat = false;
                break;
        }

        allSkills.Clear();
        SkillDataTake();
        StartCoroutine(Skills());
    }

    float GetRandomValue()
    {
        float randomResult = 0f;

        do
        {
            randomResult = Random.Range(-30f, 30f);
        } while (randomResult >= -15f && randomResult <= 15f);

        return randomResult;
    }

    int GetRandomSkillValue()
    {
        int randomResult = 1;

        do
        {
            randomResult = Random.Range(1, 4);
        } while (randomResult == lastRandomValue);

        lastRandomValue = randomResult;

        return randomResult;

    }

    void rotationBoss()
    {
        Vector3 Rotation = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);


        gameObject.transform.LookAt(Rotation);
    }

    void SkillDataTake()
    {
        SkillObjects._BossManager = this;

        if (BossData.RainOfAbundance == true)
        {
            allSkills.Add(SkillObjects.RainOfAbundanceSkill(player.gameObject));
        }

        if(BossData.JumpHigh == true)
        {
            allSkills.Add(SkillObjects.JumpHighSkill());
        }

        if(BossData.HungerDash == true)
        {
            allSkills.Add(SkillObjects.HungerDashing(player.gameObject));
        }
    }

    private void BossDataTake()
    {
        Health = BossData.Health;
        Damage = BossData.Damage;
        skillTime = BossData.SkillTime;
        agent.acceleration = BossData.Speed;
        agent.speed = BossData.Acceleration;
    }

    private void MoveUp()
    {
        Target = new Vector3(player.transform.position.x, transform.position.y + 70f, player.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, Target, 3f * Time.deltaTime);
    }

    private void MoveDown()
    {
        if(transform.position.y >= 4)
        {
            Target = new Vector3(player.transform.position.x, transform.position.y - 70f, player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Target, 3f * Time.deltaTime);
        }
    }

    private void MoveDash()
    {
        transform.Translate(transform.forward * 100f * Time.deltaTime, Space.World);
    }
}
